using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackMusify
{
    public class Album
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Song> Songs { get; set; }
        public Album(string name, List<Song> Songs)
        {
            this.name = name;
            this.Songs = Songs;
        }
    }
}
