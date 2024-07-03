using BibliotecaProjectMusify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/playlist")]
    public class PlaylistController : ControllerBase
    {
        public Principal principal = new Principal();
        [HttpGet(Name = "GetPlaylists")]
        public List<Playlist> GetPlaylists()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Playlists.ToList();
            }
        }
        [HttpGet("{IdPlaylist}")]
        public ActionResult GetPlaylist(int IdPlaylist)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var playlist = db.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == IdPlaylist);
                if (playlist == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(playlist);
                }
            }
        }
        [HttpPost(Name = "CreatePlaylist")]
        public ActionResult CreatePlaylist(string name, int idUser)
        {
            try
            {
                principal.AddPlaylist(name, idUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut(Name = "EditPlaylist")]
        public ActionResult EditPlaylist(int id, string name, int idUser)
        {
            try
            {
                principal.ModPlaylist(id, name, idUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete(Name = "DeletePlaylist")]
        public ActionResult DeletePlaylist(int IdPlaylist)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var playlist = db.Playlists.Include(p => p.Songs).FirstOrDefault(p => p.id == IdPlaylist);
                    if (playlist == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        principal.DeletePlaylist(playlist);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("{idPlaylist}/songs")]
        public ActionResult AddSongToPlaylist(int idPlaylist, int idSong)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                   principal.AddSongToPlaylist(idPlaylist, idSong);
                   return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("{idPlaylist}/songs")]
        public ActionResult RemoveSongFromPlaylist(int idPlaylist, int idSong)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.RemoveSongFromPlaylist(idPlaylist, idSong);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
