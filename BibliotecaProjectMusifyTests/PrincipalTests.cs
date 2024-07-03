using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibliotecaProjectMusify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaProjectMusify.Tests
{
    [TestClass()]
    public class PrincipalTests
    {
        ApplicationDbContext context = new ApplicationDbContext();
        Principal principal = new Principal();
        [TestMethod()]
        public void AddRegularUserTest()
        {
            User user = new User("Thiago Sterren", "thiago.sterren", "ThiagoMusify");
            principal.AddRegularUser(user);
            var search = context.Users.Find(user.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsTrue(searchTrueOrFalse);
        }
        [TestMethod()]
        public void ModRegularUserTest()
        {
            User user = new User("Matias Maretto", "matim", "marettoprofe");
            principal.AddRegularUser(user);
            int idBuscado = user.id;
            User userMod = new User("Matias Maretto", "matim", "programacion");
            userMod.id = user.id;
            principal.ModRegularUser(userMod);
            var search = context.Users.Find(idBuscado);
            Assert.AreEqual(search.password, "programacion");
        }
        [TestMethod()]
        public void DeleteRegularUserTest()
        {
            User user = new User("Lionel Messi", "messilionel", "campeondelmundo");
            principal.AddRegularUser(user);
            principal.DeleteRegularUser(user);
            var search = context.Users.Find(user.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsFalse(searchTrueOrFalse);
        }
        [TestMethod()]
        public void AddArtistTest()
        {
            Artist artist = new Artist("Andres Calamaro", "AndresCalamaro", "gintonic");
            principal.AddArtist(artist);
            var search = context.Artists.Find(artist.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsTrue(searchTrueOrFalse);
        }
        [TestMethod()]
        public void ModArtistTest()
        {
            Artist artist = new Artist("Gustavo Ceratti", "GustavoCeratti", "crimen");
            principal.AddArtist(artist);
            int idBuscado = artist.id;
            Artist artistMod = new Artist("Benito Antonio Martinez Ocasio", "Benito", "badbo");
            artistMod.id = idBuscado;
            principal.ModRegularUser(artistMod);
            var search = context.Artists.Find(idBuscado);
            Assert.AreEqual(search.password, "badbo");
        }
        [TestMethod()]
        public void DeleteArtistTest()
        {
            Artist artist = new Artist("Ricardo Arjona", "RArjona", "fuistetu");
            principal.AddArtist(artist);
            principal.DeleteArtist(artist);
            var search = context.Artists.Find(artist.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsFalse(searchTrueOrFalse);
        }
        [TestMethod()]
        public void AddAlbumTest()
        {
            Artist artist = new Artist("Gabriel Armando Mora Quintero", "MoraQuintero", "pddc");
            principal.AddArtist(artist);
            int id = artist.id;
            Album album = principal.AddAlbum("Estrella", id);
            var search = context.Albums.Find(album.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsTrue(searchTrueOrFalse);
        }
        [TestMethod()]
        public void ModAlbumTest()
        {
            Artist artist = new Artist("Benito Antonio Martinez Ocasio", "BadBunny", "badbo");
            principal.AddArtist(artist);
            Album album = principal.AddAlbum("Un Verano Sin Ti", artist.id);
            Assert.IsTrue(album.id > 0, "El ID del álbum debe ser mayor que 0 después de agregarlo");
            principal.ModAlbum(album.id, "X 100PRE", artist.id);
            var search = context.Albums.Include(a => a.Songs).FirstOrDefault(a => a.id == album.id);
            Assert.IsNotNull(search, "La búsqueda del álbum dio nula");
            Assert.AreEqual(search.name, "X 100PRE");
        }
        [TestMethod()]
        public void DeleteAlbumTest()
        {
            Artist artist = new Artist("Benito Antonio Martinez Ocasio", "BadBo94", "badbo");
            principal.AddArtist(artist);
            Album album = principal.AddAlbum("OASIS", artist.id);
            principal.DeleteAlbum(album);
            var search = context.Albums.Find(album.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsFalse(searchTrueOrFalse);
        }
        [TestMethod()]
        public void FavAlbumTest()
        {
            Artist artist = new Artist("Gabriel Armando Mora Quintero", "MoraQuinteroMusify", "pddc");
            principal.AddArtist(artist);
            Album album = principal.AddAlbum("OASIS", artist.id);
            var search = context.Albums.Find(album.id);
            Assert.IsNotNull(search);
            User user = new User("Thiago Sterren", "thiago.sterren.musify", "ThiagoMusify");
            principal.AddRegularUser(user);
            var search2 = context.Users.Find(user.id);
            Assert.IsNotNull(search2);
            principal.FavAlbum(user.id, album.id);
            Assert.AreEqual("OASIS", user.FavoritedAlbums[0].name, "No se encontró el resultado esperado");
        }
        [TestMethod()]
        public void UnfavAlbumTest()
        {
            Artist artist = new Artist("Gabriel Armando Mora Quintero", "MoraQuinteroMusifyy", "pddc");
            principal.AddArtist(artist);
            Album album = principal.AddAlbum("EUTDM", artist.id);
            var search = context.Albums.Find(album.id);
            Assert.IsNotNull(search);
            User user = new User("Thiago Sterren", "thiago.sterren.mf", "ThiagoMusify");
            principal.AddRegularUser(user);
            var search2 = context.Users.Find(user.id);
            Assert.IsNotNull(search2);
            principal.FavAlbum(user.id, album.id);
            Assert.AreEqual("EUTDM", user.FavoritedAlbums[0].name, "No se encontró el resultado esperado");
            principal.UnfavAlbum(user.id, album.id);
            bool search3 = false;
            foreach (Album a in user.FavoritedAlbums)
            {
                if (a == album)
                {
                    search3 = true; break;
                }
            }
            Assert.IsFalse(search3, "Se encontró el álbum dentro de la lista de álbums favoritos del usuario, se esperaba que se quitara de la misma");
        }
        [TestMethod()]
        public void AddSongTest()
        {
            Artist artist = new Artist("Gabriel Armando Mora Quintero", "GabrielMora", "pddc");
            principal.AddArtist(artist);
            Song song = principal.AddSong("Tuyo", 269, artist.id);
            var search = context.Songs.Find(song.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsTrue(searchTrueOrFalse);
        }
        [TestMethod()]
        public void ModSongTest()
        {
            Artist artist = new Artist("Benito Antonio Martinez Ocasio", "BBunny", "badbo");
            Artist artist2 = new Artist("Jhay Cortez", "Jhayco", "jclp");
            principal.AddArtist(artist);
            principal.AddArtist(artist2);
            var search = context.Artists.Find(artist.id);
            var search2 = context.Artists.Find(artist2.id);
            bool result = false;
            if (search != null && search2 != null)
            {
                result = true;
            }
            Assert.IsTrue(result);
            Song song = principal.AddSong("HIBIKI", 208, artist.id);
            Assert.IsTrue(song.id > 0, "El ID de la canción debe ser mayor que 0 después de agregarla");
            Assert.IsNotNull(song, "La canción es nula");
            principal.ModSong(song.id, "HIBIKI", 208, artist2.id);
            var search3 = context.Songs.Find(song.id);
            var search4 = context.Artists.Find(artist2.id);
            Assert.IsNotNull(search3, "La modificación de la canción no fue encontrada");
            Assert.AreEqual(search3.artist, search4, "El artista no es el esperado");
        }
        [TestMethod()]
        public void DeleteSongTest()
        {
            Artist artist = new Artist("Jhay Cortez", "JhayCortez", "jclp");
            principal.AddArtist(artist);
            Song song = principal.AddSong("Tokyo", 202, artist.id);
            principal.DeleteSong(song);
            var search = context.Songs.Find(song.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsFalse(searchTrueOrFalse);
        }
        [TestMethod()]
        public void AddPlaylistTest()
        {
            User user = new User("Lionel Messi", "leomessi", "campeon");
            principal.AddRegularUser(user);
            Playlist playlist = principal.AddPlaylist("Reggaeton", user.id);
            var search = context.Playlists.Find(playlist.id);
            bool searchTrueOrFalse = false;
            if (search != null)
            {
                searchTrueOrFalse = true;
            }
            Assert.IsTrue(searchTrueOrFalse);
        }
        [TestMethod()]
        public void ModPlaylistTest()
        {
            User user = new User("Cristiano Ronaldo", "cr7", "madrid");
            principal.AddRegularUser(user);
            Playlist playlist = principal.AddPlaylist("Indie", user.id);
            Assert.IsTrue(playlist.id > 0, "El ID de la playlist debe ser mayor que 0 después de agregarla");
            principal.ModPlaylist(playlist.id, "Trap y rap", user.id);
            var search = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == playlist.id);
            Assert.IsNotNull(search, "La playlist no fue encontrada");
            Assert.AreEqual("Trap y rap", search.name, "El nombre de la playlist no es el esperado");
        }
        [TestMethod()]
        public void DeletePlaylistTest()
        {
            User user = new User("Neymar", "ney", "brasil");
            principal.AddRegularUser(user);
            Playlist playlist = principal.AddPlaylist("Bachata", user.id);
            var search = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == playlist.id);
            Assert.IsNotNull(search, "La playlist no fue encontrada");
            principal.DeletePlaylist(playlist);
            var search2 = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == playlist.id);
            Assert.IsNull(search2, "La playlist fue encontrada, se esperaba que se borre");
        }
        [TestMethod()]
        public void AddSongToPlaylistTest()
        {
            User user = new User("Juan Gonzalez", "jgonz", "jgjg");
            principal.AddRegularUser(user);
            Playlist playlist = principal.AddPlaylist("Rock", user.id);
            Artist artist = new Artist("Gabriel Armando Mora Quintero", "MoraPR", "pddc");
            principal.AddArtist(artist);
            Song song = principal.AddSong("Tuyo", 269, artist.id);
            Song song2 = principal.AddSong("EN LA ORILLA", 235, artist.id);
            principal.AddSongToPlaylist(playlist.id, song.id);
            principal.AddSongToPlaylist(playlist.id, song2.id);
            Assert.AreEqual(playlist.Songs[0].name, "Tuyo", "El nombre de la canción no coincide con el esperado");
            Assert.AreEqual(playlist.Songs[1].duration, 235, "La duración de la canción no coincide con la esperada");
        }
        [TestMethod()]
        public void RemoveSongFromPlaylistTest()
        {
            User user = new User("Martin Gomez", "mgomez", "mgmg");
            principal.AddRegularUser(user);
            Playlist playlist = principal.AddPlaylist("Metal", user.id);
            Artist artist = new Artist("Gabriel Armando Mora Quintero", "PRMora", "pddc");
            principal.AddArtist(artist);
            Song song = principal.AddSong("Tuyo", 269, artist.id);
            principal.AddSongToPlaylist(playlist.id, song.id);
            Assert.AreEqual(playlist.Songs[0].name, "Tuyo", "El nombre de la canción no coincide con el esperado");
            principal.RemoveSongFromPlaylist(playlist.id, song.id);
            bool search = false;
            foreach (Song s in playlist.Songs)
            {
                if (s.id == song.id)
                {
                    search = true; break;
                }
            }
            Assert.IsFalse(search, "Se encontró la canción dentro de la playlist, se esperaba que se quitara de la misma");
        }
        [TestMethod()]
        public void AddSongToAlbumTest()
        {
            Artist artist = new Artist("Salomón", "feid", "ferxxo");
            principal.AddArtist(artist);
            Album album = principal.AddAlbum("FERXXOCALIPSIS", artist.id);
            Song song = principal.AddSong("LUNA", 360, artist.id);
            principal.AddSongToAlbum(album.id, song.id);
            var search = context.Albums.Include(a => a.Songs).FirstOrDefault(a => a.id == album.id);
            Assert.AreEqual(search.Songs[0].duration, 360, "La duración de la canción no coincide con la esperada");
        }
        [TestMethod()]
        public void RemoveSongFromAlbumTest()
        {
            Artist artist = new Artist("Salomón", "feid19", "ferxxo");
            principal.AddArtist(artist);
            Album album = principal.AddAlbum("FERXXOCALIPSIS", artist.id);
            Song song = principal.AddSong("Classy 101 - ft.(Young Miko)", 400, artist.id);
            principal.AddSongToAlbum(album.id, song.id);
            Assert.AreEqual(album.Songs[0].name, "Classy 101 - ft.(Young Miko)", "El nombre de la canción no coincide con el esperado");
            principal.RemoveSongFromAlbum(album.id, song.id);
            bool search = false;
            foreach (Song s in album.Songs)
            {
                if (s.id == song.id)
                {
                    search = true; break;
                }
            }
            Assert.IsFalse(search, "Se encontró la canción dentro del álbum, se esperaba que se quitara del mismo");
        }
    }
}