using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticTunes.DataResources
{
    public class DataManager
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

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
                }

                subDirs = root.GetDirectories();
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectory(dirInfo);
                }
            }
        }
    }
}
