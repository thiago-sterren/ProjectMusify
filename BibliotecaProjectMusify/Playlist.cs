using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaProjectMusify
{
    public class Playlist
    {
        public int id { get; set; }
        public string name { get; set; }
        public User user { get; set; }
        public List<Song>? Songs { get; set; }
        /*public int duration { get; set; } = 0;
        public Playlist() { }
        public Playlist(string name)
        {
            this.name = name;
            this.Songs = new List<Song>();
        }
        public int CalculateDuration()
        {
            int duration = 0;
            foreach (Song item in Songs)
            {
                duration += item.duration;
            }
            return duration;
        }*/
        public static Playlist CreatePlaylist(string name, User user)
        {
            Playlist playlist = new Playlist();
            playlist.name = name;
            playlist.user = user;
            playlist.Songs = new List<Song>();
            return playlist;
        }
    }
}
