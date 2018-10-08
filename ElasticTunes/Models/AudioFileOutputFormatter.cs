using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ElasticTunes.Models
{
    //public class AudioFileOutputFormatter : OutputFormatter
    //{
    //    //public AudioFileOutputFormatter()
    //    //{
    //    //    SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/octet-stream"));
    //    //}

    //    //protected override bool CanWriteType(Type type)
    //    //{
    //    //    return base.CanWriteType(type);
    //    //}

    //    //public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    //    //{
    //    //    IServiceProvider serviceProvider = context.HttpContext.RequestServices;
    //    //    var logger = serviceProvider.GetService(typeof(ILogger<AudioFileOutputFormatter>)) as ILogger;

    //    //    var response = context.HttpContext.Response;

    //    //    var buffer = new MemoryStream();
    //    //    var audioFile = context.Object;
    //    //}
    //}
}
