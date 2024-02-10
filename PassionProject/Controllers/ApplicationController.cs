using PassionProject.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PassionProject.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44377/api/appdata/";

        [HttpGet]
        [Route("Application/ApplicationList")]
        public ActionResult ApplicationList(int petId)
        {
            using (HttpClient client = new HttpClient())
            {
                
                string applicationsUrl = apiBaseUrl + $"applications?petId={petId}";
                HttpResponseMessage applicationsResponse = client.GetAsync(applicationsUrl).Result;

                if (applicationsResponse.IsSuccessStatusCode)
                {
                    var applications = applicationsResponse.Content.ReadAsAsync<List<AppDto>>().Result;

                    
                    ViewBag.PetId = petId;
                    return View("~/Views/Application/AppList.cshtml", applications);
                }
                else
                {
                   
                    ViewBag.ErrorMessage = "Error retrieving applications.";
                    return View("Error");
                }
            }
        }
    }
}
