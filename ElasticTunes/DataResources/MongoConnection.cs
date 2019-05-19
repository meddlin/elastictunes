using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticTunes.DataResources
{
    public class MongoConnection
    {
        public MongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public MongoConnection()
        {
            var client = new MongoClient("mongodb://192.168.1.160:27017/music_store");
            var database = client.GetDatabase("music_store");

            Client = client;
            Database = database;
        }
    }
}
