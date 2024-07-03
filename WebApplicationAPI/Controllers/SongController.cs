using BibliotecaProjectMusify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/song")]
    public class SongController : ControllerBase
    {
        public Principal principal = new Principal();
        [HttpGet(Name = "GetSongs")]
        public List<Song> GetSongs()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Songs.ToList();
            }
        }
        [HttpGet("{IdSong}")]
        public ActionResult GetSong(int IdSong)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var song = db.Songs.FirstOrDefault(s => s.id == IdSong);
                if (song == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(song);
                }
            }
        }
        [HttpPost(Name = "CreateSong")]
        public ActionResult CreateSong(string name, int duration, int idArtist)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.AddSong(name, duration, idArtist);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPut(Name = "EditSong")]
        public ActionResult EditSong(int id, string name, int duration, int idArtist)
        {
            try
            {
                principal.ModSong(id, name, duration, idArtist);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete(Name = "DeleteSong")]
        public ActionResult DeleteSong(int IdSong)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var song = db.Songs.FirstOrDefault(s => s.id == IdSong);
                    if (song == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        principal.DeleteSong(song);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
