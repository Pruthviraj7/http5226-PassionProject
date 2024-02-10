using PassionProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
/*<summary>
 * UserDataController handles API requests related to user data, including listing users,
 * creating new user entries, updating user information, and deleting user entries.
 *
 * Endpoints:
 * 1. Users: GET /api/UserData/users
 *    - Retrieves a list of UserDto containing basic information about all users.
 *
 * 2. CreateUser: POST /api/UserData/create
 *    - Creates a new user entry based on the provided UserDto object in the request body.
 *
 * 3. DeleteUser: DELETE /api/UserData/delete/{id}
 *    - Deletes the user entry with the specified UserId.
 *
 * 4. GetUser: GET /api/UserData/user/{id}
 *    - Retrieves detailed information about the user with the specified UserId.
 *
 * 5. UpdateUser: PUT /api/UserData/update/{id}
 *    - Updates the information of the user with the specified UserId based on the
 *      provided UserDto object in the request body.
 *
 * Examples:
 * 1. To list all users, make a GET request to:
 *    /api/UserData/users
 *
 * 2. To create a new user entry, make a POST request to:
 *    /api/UserData/create with the User details in the request body.
 *
 * 3. To delete a user entry with UserId = "exampleId", make a DELETE request to:
 *    /api/UserData/delete/exampleId
 *
 * 4. To retrieve detailed information about a user with UserId = "exampleId", make a GET request to:
 *    /api/UserData/user/exampleId
 *
 * 5. To update information of a user with UserId = "exampleId", make a PUT request to:
 *    /api/UserData/update/exampleId with the updated User details in the request body.
 *
 * Responses:
 * - For successful operations, it returns an Ok status.
 * - If the specified user is not found (for deletion, retrieval, or update), it returns a
 *   404 Not Found status.
 * - If there are validation errors during user creation or update, it returns a BadRequest
 *   status along with model validation errors.


</summary>*/
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
