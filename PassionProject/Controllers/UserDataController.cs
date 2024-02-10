using PassionProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace PassionProject.Controllers
{
    public class UserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/UserData/users")]
        public List<UserDto> Users()
        {
            List<User> Users = db.Users.ToList();
            List<UserDto> UserDtos = new List<UserDto>();
            Users.ForEach(b => UserDtos.Add(new UserDto()
            {
                UserId = b.UserId,
                UserName = b.UserName,
                UserEmail = b.UserEmail,
                UserPassword = b.UserPassword
            }));
            return UserDtos;
        }

        [HttpPost]
        [Route("api/UserData/create")]
        public IHttpActionResult CreateUser(UserDto newUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserId = newUser.UserId,
                    UserName = newUser.UserName,
                    UserEmail = newUser.UserEmail,
                    UserPassword = newUser.UserPassword
                };

                db.Users.Add(user);
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("api/UserData/delete/{id}")]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound(); 
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(); 
        }

        [HttpGet]
        [Route("api/UserData/user/{id}")]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound(); 
            }

            UserDto userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword
            };

            return Ok(userDto); 
        }

        [HttpPut]
        [Route("api/UserData/update/{id}")]
        public IHttpActionResult UpdateUser(string id, UserDto updatedUser)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(id);

                if (user == null)
                {
                    return NotFound(); 
                }

                user.UserName = updatedUser.UserName;
                user.UserEmail = updatedUser.UserEmail;
                user.UserPassword = updatedUser.UserPassword;

                db.SaveChanges();

                return Ok(); 
            }
            else
            {
                return BadRequest(ModelState); 
            }
        }
    }
}
