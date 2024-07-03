using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BackMusify
{
    public class Playlist
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Song> Songs { get; set; }
        public Playlist(string name)
        {
            this.name = name;
        }
    }
}
