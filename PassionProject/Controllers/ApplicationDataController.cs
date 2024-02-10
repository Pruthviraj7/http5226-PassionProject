using PassionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
/*<summary>

 * ApplicationDataController handles API requests related to pet applications and form data.
 * It provides endpoints to retrieve application details for a specific pet.
 *
 * Example:
 * To get the application form data for a pet with PetId = 1, make a GET request to:
 *     /api/AppData/ApplicationForm?petId=1
 *
 * Response:
 * If the pet exists, it returns a PetDto object with basic pet information and a list of AppDto
 * containing details of applications associated with the specified pet.
 * If the pet does not exist, it returns a 404 Not Found status.
 


</ summary>*/
namespace PassionProject.Controllers
{
    public class ApplicationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/AppData/ApplicationForm")]
        public IHttpActionResult GetApplicationForm(int petId)
        {
            var pet = db.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound();
            }

            var petDto = new PetDto
            {
                PetId = pet.PetId,
                PetName = pet.PetName,
                
            };

            var applications = db.Applications.Where(app => app.PetId == petId).ToList();
            var appDtos = new List<AppDto>();

            foreach (var application in applications)
            {
                var appDto = new AppDto
                {
                    AppId = application.AppId,
                    AdoptId = application.AppId,
                    PetId = application.PetId,
                    AppSubmission = application.AppSubmission,
                    AppStatus = application.AppStatus,
                    AppComments = application.AppComments
                };

                appDtos.Add(appDto);
            }

            petDto.Applications = appDtos;
            return Ok(petDto);
        }
    }
}
