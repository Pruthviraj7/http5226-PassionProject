using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
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
            using (HttpClient client = new HttpClient())
            {
                string petDetailsUrl = apiBaseUrl + $"pets/{petId}";
                HttpResponseMessage petDetailsResponse = client.GetAsync(petDetailsUrl).Result;

                if (petDetailsResponse.IsSuccessStatusCode)
                {
                    var petDetails = petDetailsResponse.Content.ReadAsAsync<PetDto>().Result;

                    string applicationsUrl = apiBaseUrl + $"applications?petId={petId}";
                    HttpResponseMessage applicationsResponse = client.GetAsync(applicationsUrl).Result;

                    if (applicationsResponse.IsSuccessStatusCode)
                    {
                        var applications = applicationsResponse.Content.ReadAsAsync<List<AppDto>>().Result;

                       
                        ViewBag.PetDetails = petDetails;
                        return View("~/Views/Application/ApplicationForm.cshtml", applications);
                    }
                }

                ViewBag.ErrorMessage = "Error retrieving pet details or applications.";
                return View("Error");
            }
        }

        public ActionResult CreatePet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePet(Pet pet)
        {
            using (HttpClient client = new HttpClient())
            {
                string createPetUrl = apiBaseUrl + "createpet";
                HttpResponseMessage response = client.PostAsJsonAsync(createPetUrl, pet).Result;

                if (response.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error creating pet listing.";
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public ActionResult DeletePet(int petId)
        {
            using (HttpClient client = new HttpClient())
            {
                string deletePetUrl = apiBaseUrl + $"deletepet/{petId}";
                HttpResponseMessage response = client.DeleteAsync(deletePetUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error deleting pet.";
                    return View("Error");
                }
            }
        }
    }
}
