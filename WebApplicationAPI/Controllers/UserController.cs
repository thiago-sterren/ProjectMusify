using Microsoft.EntityFrameworkCore;
using BibliotecaProjectMusify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        public Principal principal = new Principal();
        [HttpGet(Name = "GetUsers")]
        public List<User> GetUsers()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Users.ToList();
            }
        }
        [HttpGet("{IdUser}")]
        public ActionResult GetUser(int IdUser)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.id == IdUser);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
        }
        [HttpPost(Name = "CreateUser")]
        public ActionResult CreateUser(string name, string username, string password)
        {
            User user = new User(name, username, password);
            try
            {
                principal.AddRegularUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut(Name = "EditUser")]
        public ActionResult EditUser(int id, string name, string username, string password)
        {
            User userMod = new User(name, username, password);
            userMod.id = id;
            try
            {
                principal.ModRegularUser(userMod);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete(Name = "DeleteUser")]
        public ActionResult DeleteUser(int IdUser)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(u => u.id == IdUser);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        principal.DeleteRegularUser(user);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("{idUser}/favAlbums")]
        public ActionResult FavAlbum(int idUser, int idAlbum)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.FavAlbum(idUser, idAlbum);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("{idUser}/favAlbums")]
        public ActionResult UnfavAlbum(int idUser, int idAlbum)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    principal.UnfavAlbum(idUser, idAlbum);
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
