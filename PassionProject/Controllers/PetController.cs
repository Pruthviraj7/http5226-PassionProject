using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using PassionProject.Migrations;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class PetController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44377/api/petdata/";

        public ActionResult List()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiBaseUrl + "listpets";
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var pets = response.Content.ReadAsAsync<List<PetDto>>().Result;
                    return View("PetList", pets);
                }
                else
                {
                    ViewBag.ErrorMessage = "Error retrieving pets.";
                    return View("Error");
                }
            }
        }


        public ActionResult Apply(int petId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var petDetails = db.Pets
                    .Where(p => p.PetId == petId)
                    .Select(p => new PetDto
                    {
                        PetId = p.PetId,
                        PetName = p.PetName,
                       
                    })
                    .FirstOrDefault();

                if (petDetails != null)
                {
                    ViewBag.PetDetails = petDetails;

                   
                    return RedirectToAction("ApplicationList", "Application", new { petId });
                }
            }

            ViewBag.ErrorMessage = "Error retrieving pet details.";
            return View("Error");
        }

       
    }
}
