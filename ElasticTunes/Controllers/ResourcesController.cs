using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ElasticTunes.DataResources;
using Microsoft.AspNetCore.Mvc;

namespace ElasticTunes.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ResourcesController : Controller
    {
        internal DataManager GetDataManager() =>  new DataManager("http://192.168.1.160:9200/");
        internal MongoDataManager GetMongoDataManager() => new MongoDataManager();

        public List<string> BigListOfFiles { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/133660/how-can-i-access-a-mapped-network-drive-with-system-io-directoryinfo</remarks>
        [HttpGet]
        public void CrawlMappedDrive()
        {
            string path = @"Z:\0_MEDIA\Music";
            var dm = GetDataManager();
            var mdm = GetMongoDataManager();

            DirectoryInfo targetDir = new DirectoryInfo(path);

            if (targetDir != null)
                dm.WalkDirectory(targetDir);

            // Get music docs
            var musicDocs = dm.BuildDocuments(dm.GetListOfFiles());

            // Insert one into Mongo (a random one for now)
            //mdm.Insert(musicDocs.Count() > 20 ? musicDocs[10] : null);

            mdm.InsertMany(musicDocs);

            // Index into Elastic
            dm.InsertToElastic(musicDocs);
        }
    }
}
