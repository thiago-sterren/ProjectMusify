using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaProjectMusify
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public List<Playlist>? Playlists { get; set; }
        public List<Album>? FavoritedAlbums { get; set; }
        public User(string name, string username, string password)
        {
            this.name = name;
            this.username = username;
            this.password = password;
            this.Playlists = new List<Playlist>();
            this.FavoritedAlbums = new List<Album>();
        }
    }
}
