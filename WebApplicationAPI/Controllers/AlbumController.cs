using BibliotecaProjectMusify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/album")]
    public class AlbumController : ControllerBase
    {
        public Principal principal = new Principal();
        [HttpGet(Name = "GetAlbums")]
        public List<Album> GetAlbums()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Albums.ToList();
            }
        }
        [HttpGet("{IdAlbum}")]
        public ActionResult GetAlbum(int IdAlbum)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var album = db.Albums.Include(a => a.Songs).FirstOrDefault(a => a.id == IdAlbum);
                if (album == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(album);
                }
            }
        }
        [HttpPost(Name = "CreateAlbum")]
        public ActionResult CreateAlbum(string name, int idArtist)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.AddAlbum(name, idArtist);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPut(Name = "EditAlbum")]
        public ActionResult EditAlbum(int id, string name, int idArtist)
        {
            try
            {
                principal.ModAlbum(id, name, idArtist);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete(Name = "DeleteAlbum")]
        public ActionResult DeleteAlbum(int IdAlbum)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var album = db.Albums.Include(a => a.Songs).FirstOrDefault(a => a.id == IdAlbum);
                    if (album == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        principal.DeleteAlbum(album);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("{idAlbum}/songs")]
        public ActionResult AddSongToAlbum(int idAlbum, int idSong)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.AddSongToAlbum(idAlbum, idSong);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("{idAlbum}/songs")]
        public ActionResult RemoveSongFromAlbum(int idAlbum, int idSong)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.RemoveSongFromAlbum(idAlbum, idSong);
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
