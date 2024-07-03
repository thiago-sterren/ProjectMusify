using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaProjectMusify
{
    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public int duration { get; set; } // en segundos
        public Artist artist { get; set; }
        /*public Song() { }
        public Song(string name, int duration, Artist artist)
        {
            this.name = name;
            this.duration = duration;
            this.artist = artist;
        }*/
        public static Song CreateSong(string name, int duration, Artist artist)
        {
            Song song = new Song();
            song.name = name;
            song.duration = duration;
            song.artist = artist;
            return song;
        }
    }
}
