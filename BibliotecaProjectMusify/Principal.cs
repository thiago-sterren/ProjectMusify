using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BibliotecaProjectMusify
{
    public class Principal
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void AddRegularUser(User user)
        {
            if (context.Users.Any(u => u.username == user.username) == false)
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ocurrió un error al intentar agregar este usuario: {ex.Message}");
                }
            }
            else
            {
                throw new Exception("Ya existe un usuario con el mismo username.");
            }
        }
        public void ModRegularUser(User user)
        {
            if (context.Users.Any(u => u.username == user.username && u.id != user.id) == false)
            {
                try
                {
                    var search = context.Users.Find(user.id);
                    if (search != null)
                    {
                        search.name = user.name;
                        search.username = user.username;
                        search.password = user.password;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("No se encontró ningún usuario con este id");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ocurrió un error al intentar modificar este usuario: {ex.Message}");
                }
            }
            else
            {
                throw new Exception("Ya existe un usuario con el mismo username.");
            }
        }
        public void DeleteRegularUser(User user)
        {
            try
            {
                var search = context.Users.Find(user.id);
                if (search != null)
                {
                    context.Users.Remove(search);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró ningún usuario con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar borrar este usuario: {ex.Message}");
            }
        }
        public void AddArtist(Artist artist)
        {
            if (context.Artists.Any(a => a.username == artist.username) == false)
            {
                try
                {
                    context.Artists.Add(artist);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ocurrió un error al intentar agregar este artista: {ex.Message}");
                }
            }   
            else
            {
                throw new Exception("Ya existe un artista con el mismo username.");
            }
        }
        public void ModArtist(Artist artist)
        {
            if (context.Artists.Any(a => a.username == artist.username && a.id != artist.id) == false)
            {
                try
                {
                    var search = context.Artists.Find(artist.id);
                    if (search != null)
                    {
                        search.name = artist.name;
                        search.username = artist.username;
                        search.password = artist.password;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("No se encontró ningún artista con este id");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ocurrió un error al intentar modificar este artista: {ex.Message}");
                }
            }
            else
            {
                throw new Exception("Ya existe un artista con el mismo username.");
            }
        }
        public void DeleteArtist(Artist artist)
        {
            try
            {
                var search = context.Artists.Find(artist.id);
                if (search != null)
                {
                    context.Artists.Remove(search);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró ningún artista con este id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar borrar este artista: {ex.Message}");
            }
        }
        public Album AddAlbum(string name, int idArtist)
        {
            try
            {
                var artist = context.Artists.FirstOrDefault(a => a.id == idArtist);
                if (artist != null)
                {
                    Album album = Album.CreateAlbum(name, artist);
                    context.Albums.Add(album);
                    context.SaveChanges();
                    return album;
                }
                else
                {
                    throw new Exception("No se encontró ningún artista con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar agregar este álbum: {ex.Message}");
            }
        }
        public Album ModAlbum(int id, string name, int idArtist)
        {
            try
            {
                var artist = context.Artists.FirstOrDefault(a => a.id == idArtist);
                if (artist != null)
                {
                    Album albumMod = Album.CreateAlbum(name, artist);
                    albumMod.id = id;
                    var search = context.Albums.Include(a => a.Songs).FirstOrDefault(a => a.id == albumMod.id);
                    if (search != null)
                    {
                        search.name = albumMod.name;
                        search.artist = albumMod.artist;
                        context.SaveChanges();
                        return search;
                    }
                    else
                    {
                        throw new Exception("No se encontró ningún álbum con ese id");
                    }
                }
                else
                {
                    throw new Exception("No se encontró ningún artista con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar modificar este álbum: {ex.Message}");
            }
        }
        public Album DeleteAlbum(Album album)
        {
            try
            {
                var search = context.Albums.Find(album.id);
                if (search != null)
                {
                    context.Albums.Remove(search);
                    context.SaveChanges();
                    return search;
                }
                else
                {
                    throw new Exception("No se encontró ningún álbum con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar borrar este álbum: {ex.Message}");
            }
        }
        public void AddSongToAlbum(int idAlbum, int idSong)
        {
            try
            {
                var searchAlbum = context.Albums.Include(p => p.Songs).FirstOrDefault(p => p.id == idAlbum);
                if (searchAlbum == null)
                {
                    throw new Exception("No existe un álbum con ese id");
                }
                var searchSong = context.Songs.FirstOrDefault(s => s.id == idSong);
                if (searchSong == null)
                {
                    throw new Exception("La canción que estás intentando agregar a este álbum no fue encontrada en la base de datos");
                }
                foreach (var item in searchAlbum.Songs)
                {
                    if (searchSong.id == item.id)
                    {
                        throw new Exception("Esta canción ya está en este álbum");
                    }
                }
                searchAlbum.Songs.Add(searchSong);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoveSongFromAlbum(int idAlbum, int idSong)
        {
            try
            {
                var searchAlbum = context.Albums.Include(p => p.Songs).FirstOrDefault(p => p.id == idAlbum);
                if (searchAlbum == null)
                {
                    throw new Exception("No existe un álbum con ese id");
                }
                var searchSong = context.Songs.FirstOrDefault(s => s.id == idSong);
                if (searchSong == null)
                {
                    throw new Exception("La canción que estás intentando eliminar de este álbum no fue encontrada en la base de datos");
                }
                bool search = false;
                foreach (Song item in searchAlbum.Songs)
                {
                    if (item.id == searchSong.id)
                    {
                        search = true;
                        break;
                    }
                }
                if (search == true)
                {
                    searchAlbum.Songs.Remove(searchSong);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró la canción en el álbum");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Song AddSong(string name, int duration, int idArtist)
        {
            try
            {
                var artist = context.Artists.FirstOrDefault(a => a.id == idArtist);
                if (artist != null)
                {
                    Song song = Song.CreateSong(name, duration, artist);
                    context.Songs.Add(song);
                    context.SaveChanges();
                    return song;
                }
                else
                {
                    throw new Exception("No se encontró ningún artista con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar agregar este álbum: {ex.Message}");
            }
        }
        public Song ModSong(int id, string name, int duration, int idArtist)
        {
            try
            {
                var artist = context.Artists.FirstOrDefault(a => a.id == idArtist);
                if (artist != null)
                {
                    Song songMod = Song.CreateSong(name, duration, artist);
                    songMod.id = id;
                    var search = context.Songs.Include(s => s.artist).FirstOrDefault(s => s.id == songMod.id);
                    if (search != null)
                    {
                        search.name = songMod.name;
                        search.duration = songMod.duration;
                        search.artist = songMod.artist;
                        context.SaveChanges();
                        return search;
                    }
                    else
                    {
                        throw new Exception("No se encontró ninguna canción con ese id");
                    }
                }
                else
                {
                    throw new Exception("No se encontró ningún artista con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo un error al intentar modificar esta canción: {ex.Message}");
            }
        }
        public Song DeleteSong(Song song)
        {
            try
            {
                var search = context.Songs.Find(song.id);
                if (search != null)
                {
                    context.Songs.Remove(search);
                    context.SaveChanges();
                    return search;
                }
                else
                {
                    throw new Exception("No se encontró ninguna canción con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar borrar esta canción: {ex.Message}");
            }
        }
        public Playlist AddPlaylist(string name, int idUser)
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => u.id == idUser);
                if (user != null)
                {
                    Playlist playlist = Playlist.CreatePlaylist(name, user);
                    context.Playlists.Add(playlist);
                    context.SaveChanges();
                    return playlist;
                }
                else
                {
                    throw new Exception("No se encontró ningún usuario con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar agregar esta playlist: {ex.Message}");
            }
        }
        public Playlist ModPlaylist(int id, string name, int idUser) 
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => u.id == idUser);
                if (user != null)
                {
                    Playlist playlistMod = Playlist.CreatePlaylist(name, user);
                    playlistMod.id = id;
                    var search = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == playlistMod.id);
                    if (search != null)
                    {
                        search.name = playlistMod.name;
                        search.user = playlistMod.user;
                        context.SaveChanges();
                        return search;
                    }
                    else
                    {
                        throw new Exception("No se encontró ninguna playlist con ese id");
                    }
                }
                else
                {
                    throw new Exception("No se encontró ningún usuario con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar modificar esta playlist: {ex.Message}");
            }
        }
        public Playlist DeletePlaylist(Playlist playlist)
        {
            try
            {
                var search = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == playlist.id);
                if (search != null)
                {
                    context.Playlists.Remove(search);
                    context.SaveChanges();
                    return playlist;
                }
                else
                {
                    throw new Exception("No se encontró ninguna playlist con ese id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al intentar borrar esta playlist: {ex.Message}");
            }
        }
        public void AddSongToPlaylist(int idPlaylist, int idSong)
        {
            try
            {
                var searchPlaylist = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == idPlaylist);
                if (searchPlaylist == null)
                {
                    throw new Exception("No existe una playlist con ese id");
                }
                var searchSong = context.Songs.FirstOrDefault(s => s.id == idSong);
                if (searchSong == null)
                {
                    throw new Exception("La canción que estás intentando agregar a esta playlist no fue encontrada en la base de datos");
                }
                foreach (var item in searchPlaylist.Songs)
                {
                    if (searchSong.id == item.id)
                    {
                        throw new Exception("Esta canción ya está en esta playlist");
                    }
                }
                searchPlaylist.Songs.Add(searchSong);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoveSongFromPlaylist(int idPlaylist, int idSong)
        {
            try
            {
                var searchPlaylist = context.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == idPlaylist);
                if (searchPlaylist == null)
                {
                    throw new Exception("No existe una playlist con ese id");
                }
                var searchSong = context.Songs.FirstOrDefault(s => s.id == idSong);
                if (searchSong == null)
                {
                    throw new Exception("La canción que estás intentando eliminar de esta playlist no fue encontrada en la base de datos");
                }
                bool search = false;
                foreach (Song item in searchPlaylist.Songs)
                {
                    if (item.id == searchSong.id)
                    {
                        search = true;
                        break;
                    }
                }
                if (search == true)
                {
                    searchPlaylist.Songs.Remove(searchSong);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró la canción en la playlist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void FavAlbum(int idUser, int idAlbum)
        {
            try
            {
                var searchUser = context.Users.FirstOrDefault(u => u.id == idUser);
                if (searchUser == null)
                {
                    throw new Exception("No existe un usuario con ese id");
                }
                var searchAlbum = context.Albums.Include(p => p.Songs).FirstOrDefault(p => p.id == idAlbum);
                if (searchAlbum == null)
                {
                    throw new Exception("No existe un álbum con ese id");
                }
                foreach (Album item in searchUser.FavoritedAlbums)
                {
                    if (item.id == searchAlbum.id)
                    {
                        throw new Exception("Este álbum ya estaba agregado a su lista de sus álbums favoritos");
                    }
                }
                searchUser.FavoritedAlbums.Add(searchAlbum);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UnfavAlbum(int idUser, int idAlbum)
        {
            try
            {
                var searchUser = context.Users.FirstOrDefault(u => u.id == idUser);
                if (searchUser == null)
                {
                    throw new Exception("No existe un usuario con ese id");
                }
                var searchAlbum = context.Albums.Include(p => p.Songs).FirstOrDefault(p => p.id == idAlbum);
                if (searchAlbum == null)
                {
                    throw new Exception("No existe un álbum con ese id");
                }
                bool search = false;
                foreach (Album item in searchUser.FavoritedAlbums)
                {
                    if (item.id == searchAlbum.id)
                    {
                        search = true;
                        break;
                    }
                }
                if (search == true)
                {
                    searchUser.FavoritedAlbums.Remove(searchAlbum);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Este álbum no estaba en su lista de sus álbums favoritos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
