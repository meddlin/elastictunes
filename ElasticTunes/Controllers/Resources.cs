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

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/133660/how-can-i-access-a-mapped-network-drive-with-system-io-directoryinfo</remarks>
        [HttpGet]
        public void CrawlMappedDrive()
        {
            DirectoryInfo targetDir = new DirectoryInfo(@"Z:\0_MEDIA\Music");
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
    }
}
