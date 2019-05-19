using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Elasticsearch.Net;
using ElasticTunes.Models;

namespace ElasticTunes.DataResources
{
    public class DataManager
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        private ElasticConnection _elasticConn;
        public List<string> rawList = new List<string>();

        public DataManager(string connection)
        {
            _elasticConn = _elasticConn ?? ElasticConnection.Instance(new Uri(connection), "music");

            //_elasticConn = _elasticConn ?? ElasticConnection.Instance(
            //    resource: settings.ElasticConnectionUri,
            //    index: !string.IsNullOrWhiteSpace(NewIndexName) ? NewIndexName : settings.ElasticIndexAlias);

            //NewIndexName = !string.IsNullOrWhiteSpace(settings.ElasticIndexAlias) ? settings.ElasticIndexAlias : "";
        }

        public List<string> GetListOfFiles() => rawList;

        public List<string> GetFiles()
        {
            var resources = new List<string>();

            string[] drives = System.Environment.GetLogicalDrives();
            foreach (string dr in drives)
            {
                DriveInfo di = new DriveInfo(dr);
                if (!di.IsReady)
                {
                    Console.WriteLine($"The drive {di.Name} could not be read");
                    continue;
                }
                DirectoryInfo rootDir = di.RootDirectory;
                WalkDirectory(rootDir);
            }

            return resources;
        }

        public void WalkDirectory(DirectoryInfo root)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    Console.WriteLine(fi.FullName);
                    rawList.Add(fi.FullName);
                }

                subDirs = root.GetDirectories();
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectory(dirInfo);
                }
            }
        }

        public List<MusicDocument> BuildDocuments(List<string> collection)
        {
            // TODO : DataManager.BuildDocuments -- Experiment with turning this into a generator
            var result = new List<MusicDocument>();

            if (collection.Count() > 0)
            {
                foreach(string c in collection)
                {
                    var parsed = ParseFileName(c);

                    parsed.Id = Guid.NewGuid();
                    parsed.FileName = c;
                    parsed.DateAdded = DateTime.Now;

                    //var tmp = new MusicDocument()
                    //{
                    //    Id = Guid.NewGuid(),
                    //    FileName = c,
                    //    DateAdded = DateTime.Now
                    //};

                    result.Add(parsed);
                }
            }

            return result;
        }

        public MusicDocument ParseFileName(string fileName)
        {
            // "Z:\\0_MEDIA\\Music\\Led Zeppelin\\01 - Studio albums\\CD remastered\\1971 - Led Zeppelin IV [1994, Atlantic, 82638-2]\\06. Four Sticks.mp3"
            var tmp = new MusicDocument();

            string songName = fileName.Split("\\").Last();
            string number = songName.Split(" ").FirstOrDefault();

            tmp.SongName = songName;
            tmp.SongNumber = number;

            return tmp;
        }

        public void InsertToElastic(List<MusicDocument> collection)
        {
            IBulkResponse bulkIdx = _elasticConn.Client
                .Bulk(b => b
                    .Index("music")
                    .IndexMany(collection)
                );
        }
    }
}
