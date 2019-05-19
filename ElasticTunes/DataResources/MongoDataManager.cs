using ElasticTunes.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticTunes.DataResources
{
    public class MongoDataManager
    {
        private readonly IMongoCollection<MusicDocument> _musicDocs;
        public MongoConnection Connection { get; set; }

        public MongoDataManager()
        {
            Connection = new MongoConnection();

            _musicDocs = Connection.Database.GetCollection<MusicDocument>("library");
        }

        public void Insert(MusicDocument musicDoc)
        {
            if (musicDoc != null) _musicDocs.InsertOne(musicDoc);
        }

        public void InsertMany(List<MusicDocument> collection)
        {
            if (collection.Count() > 0)
            {
                _musicDocs.InsertMany(collection);
            }
        }
    }
}
