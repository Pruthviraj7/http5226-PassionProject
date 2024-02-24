using PassionProject.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PassionProject.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44377/api/appdata/";

        // GET: List of Applications
        public ActionResult AppList()
        {
            using (HttpClient client = new HttpClient())
            {
                string applicationsUrl = apiBaseUrl + "applications";
                HttpResponseMessage applicationsResponse = client.GetAsync(applicationsUrl).Result;

                if (applicationsResponse.IsSuccessStatusCode)
                {
                    var applications = applicationsResponse.Content.ReadAsAsync<List<AppDto>>().Result;
                    return View("AppList", applications);
                }
                else
                {
                    ViewBag.ErrorMessage = "Error retrieving applications.";
                    return View("Error");
                }
            }
        }

        // GET: Apply for Adoption
        public ActionResult Apply(int petId)
        {
            // Create a new ApplicationDto and set the PetId
            var application = new AppDto { PetId = petId };

            return View("Apply", application);
        }

        // POST: Submit Adoption Application
        [HttpPost]
        public ActionResult Apply(AppDto newApplication)
        {
            using (HttpClient client = new HttpClient())
            {
                
                string applyUrl = apiBaseUrl + "apply?petId=" + newApplication.PetId;
                HttpResponseMessage response = client.PostAsJsonAsync(applyUrl, newApplication).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AppList");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error submitting application.";
                    return View("Error");
                }
            }
        }

        // GET: Update Application Form
        public ActionResult Update(int appId)
        {
            using (HttpClient client = new HttpClient())
            {
                string getApplicationUrl = apiBaseUrl + "application/" + appId;
                HttpResponseMessage response = client.GetAsync(getApplicationUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var application = response.Content.ReadAsAsync<AppDto>().Result;
                    return View("Update", application);
                }
                else
                {
                    ViewBag.ErrorMessage = "Error retrieving application.";
                    return View("Error");
                }
            }
        }

        // POST: Update Application
        [HttpPost]
        public ActionResult Update(int appId, AppDto updatedApplication)
        {
            using (HttpClient client = new HttpClient())
            {
                string updateUrl = apiBaseUrl + "update/" + appId;
                HttpResponseMessage response = client.PutAsJsonAsync(updateUrl, updatedApplication).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AppList");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error updating application.";
                    return View("Error");
                }
            }
        }

        // POST: Delete Application
        public ActionResult Delete(int appId)
        {
            using (HttpClient client = new HttpClient())
            {
                string deleteUrl = apiBaseUrl + "delete/" + appId;
                HttpResponseMessage response = client.DeleteAsync(deleteUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AppList");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error deleting application.";
                    return View("Error");
                }
            }
        }
    }
}
