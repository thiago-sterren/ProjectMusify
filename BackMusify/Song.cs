using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMusify
{
    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public string duration { get; set; }
        public List<Artist> SongArtists { get; set; }
        public Song(string name, string duration, List<Artist> SongArtists)
        {
            this.name = name;
            this.duration = duration;
            this.SongArtists = SongArtists;
        }
    }
}
