using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticTunes.Models
{
    public class MusicDocument
    {
        public Guid Id { get; set; }

        public string SongName { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public DateTime DateReleased { get; set; }
        public string Genre { get; set; }

        public string FileName { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
