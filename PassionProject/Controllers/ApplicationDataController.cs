using PassionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
