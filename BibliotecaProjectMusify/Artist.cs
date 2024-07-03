using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaProjectMusify
{
    public class Artist : User
    {
        public List<Song>? Songs { get; set; }
        public List<Album>? Albums { get; set; }
        public Artist(string name, string username, string password) : base(name, username, password)
        {
            this.Songs = new List<Song>();
            this.Albums = new List<Album>();
        }
    }
}
