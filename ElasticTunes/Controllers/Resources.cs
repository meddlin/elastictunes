using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElasticTunes.DataResources;
using Microsoft.AspNetCore.Mvc;

namespace ElasticTunes.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Resources : Controller
    {
        [HttpGet]
        public void CrawlDirectories()
        {
            var dm = new DataManager();
            var result = dm.GetFiles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/133660/how-can-i-access-a-mapped-network-drive-with-system-io-directoryinfo</remarks>
        [HttpGet]
        public void CrawlDirectories2()
        {
            DirectoryInfo targetDir = new DirectoryInfo(@"\\192.168.1.100\TheKove\0_MEDIA\Music");
            if (targetDir != null)
            {
                Console.Write("stuff");
                var dirList = targetDir.GetDirectories();
                if (dirList.Count() > 0)
                {
                    var fileList = dirList[0].GetFiles();
                    if (fileList.Count() > 0)
                    {
                        Console.WriteLine(fileList[0].FullName);
                    }
                }
            }
        }

        public List<string> BigListOfFiles { get; set; } = new List<string>();

        public void CrawlDirectory(string path)
        {
            var targetDir = new DirectoryInfo(path);
            DirectoryInfo[] dirList = targetDir.GetDirectories();
            for (int i = 0; i < dirList.Count(); i++)
            {
                if (dirList[i].EnumerateFiles().Count() > 0) BigListOfFiles.AddRange( dirList[i].GetFiles().Select(f => f.FullName).ToArray() );

                if (dirList[i].EnumerateDirectories().Count() > 0) CrawlDirectory(dirList[i].FullName);
                else break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/133660/how-can-i-access-a-mapped-network-drive-with-system-io-directoryinfo</remarks>
        [HttpGet]
        public List<string> CrawlMappedDrive()
        {
            string path = @"Z:\0_MEDIA\Music";
            DirectoryInfo targetDir = new DirectoryInfo(path);
            if (targetDir != null)
            {
                var dm = new DataManager();
                return dm.WalkDirectory(targetDir);
            }

            return new List<string>();
        }
    }
}
