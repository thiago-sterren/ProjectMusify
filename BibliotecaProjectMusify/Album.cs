using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaProjectMusify
{
    public class Album
    {
        public int id { get; set; }
        public string name { get; set; }
        public Artist artist { get; set; }
        public List<Song>? Songs { get; set; }
        /*public Album() { }
        public Album(string name, Artist artist)
        {
            this.name = name;
            this.artist = artist;
            this.Songs = new List<Song>();
        }*/
        public static Album CreateAlbum(string name, Artist artist)
        {
            Album album = new Album();
            album.name = name;
            album.artist = artist;
            album.Songs = new List<Song>();
            return album;
        }
    }
}
