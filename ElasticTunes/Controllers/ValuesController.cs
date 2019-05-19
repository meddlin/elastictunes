using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ElasticTunes.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public void LoadMusic()
        {
            Console.WriteLine("starting to play music...");
        }

        [HttpGet]
        public FileStream FileDownload()
        {
            var path = @"C:\Users\meddlin\Documents\Junkyard\git\ElasticTunes\ElasticTunes\Models\263_full_resurgence_0160_preview.mp3";
            //var path = @"C:\Users\meddlin\Documents\Junkyard\git\ElasticTunes\ElasticTunes\Models\bensound-happyrock.mp3";
            //var path = @"C:\Users\meddlin\Documents\Junkyard\git\ElasticTunes\ElasticTunes\Models\01 Kryptonite.mp3";
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = path
            //};

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return stream;
        }

        [HttpGet]
        public HttpResponseMessage StreamSong()
        {
            var path = @"C:\Users\meddlin\Documents\Junkyard\git\ElasticTunes\ElasticTunes\Models\01 Kryptonite.mp3";
            var dataBytes = System.IO.File.ReadAllBytes(path);
            var dataStream = new MemoryStream(dataBytes);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(dataStream);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = "Kyptonite.mp3";
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return httpResponseMessage;
        }


        [HttpGet]
        public HttpResponseMessage AudioFileProvider()
        {
            var stream = new MemoryStream();
            // processing the stream.
            var path = @"C:\Users\meddlin\Documents\Junkyard\git\ElasticTunes\ElasticTunes\Models\01 Kryptonite.mp3";

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            response.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = path
                };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return response;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
