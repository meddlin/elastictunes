using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticTunes.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<MusicDocument> Music { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
