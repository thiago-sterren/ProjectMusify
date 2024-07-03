using BibliotecaProjectMusify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/artist")]
    public class ArtistController : ControllerBase
    {
        public Principal principal = new Principal();
        [HttpGet(Name = "GetArtists")]
        public List<Artist> GetArtists()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Artists.ToList();
            }
        }
        [HttpGet("{IdArtist}")]
        public ActionResult GetArtist(int IdArtist)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var artist = db.Artists.FirstOrDefault(a => a.id == IdArtist);
                if (artist == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(artist);
                }
            }
        }
        [HttpPost(Name = "CreateArtist")]
        public ActionResult CreateArtist(string name, string username, string password)
        {
            Artist artist = new Artist(name, username, password);
            try
            {
                principal.AddArtist(artist);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut(Name = "EditArtist")]
        public ActionResult EditArtist(int id, string name, string username, string password)
        {
            Artist artistMod = new Artist(name, username, password);
            artistMod.id = id;
            try
            {
                principal.ModArtist(artistMod);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete(Name = "DeleteArtist")]
        public ActionResult DeleteArtist(int IdArtist)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var artist = db.Artists.FirstOrDefault(a => a.id == IdArtist);
                    if (artist == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        principal.DeleteArtist(artist);
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