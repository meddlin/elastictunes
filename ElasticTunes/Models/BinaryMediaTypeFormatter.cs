﻿using System;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ElasticTunes.Models
{
    public class BinaryMediaTypeFormatter : OutputFormatter
    {

        private static Type _supportedType = typeof(byte[]);
        private bool _isAsync = false;

        public BinaryMediaTypeFormatter() : this(false)
        {
        }

        public BinaryMediaTypeFormatter(bool isAsync)
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            IsAsync = isAsync;
        }

        public bool IsAsync
        {
            get { return _isAsync; }
            set { _isAsync = value; }
        }

        private Task GetWriteTask(Stream stream, byte[] data)
        {
            return new Task(() =>
            {
                var ms = new MemoryStream(data);
                ms.CopyTo(stream);
            });
        }


        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var value = context.Object;
            if (value == null) value = new byte[0];

            Task writeTask = GetWriteTask((FileStream)context.Object, (byte[])value);
            if (_isAsync)
            {
                writeTask.Start();
            }
            else
            {
                writeTask.RunSynchronously();
            }

            return writeTask;
        }
    }
}
