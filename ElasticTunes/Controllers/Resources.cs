using System;
using System.Collections.Generic;
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
    }
}
