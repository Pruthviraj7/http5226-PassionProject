using PassionProject.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PassionProject.Controllers
{
    public class UserController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44377/api/userdata/";

        public ActionResult List()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiBaseUrl + "users";
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var users = response.Content.ReadAsAsync<List<UserDto>>().Result;
                    return View("List", users);
                }
                else
                {
                    
                    return RedirectToAction("List");
                }
            }
        }

        public ActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        public ActionResult Create(UserDto newUser)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = apiBaseUrl + "create";
                    HttpResponseMessage response = client.PostAsJsonAsync(url, newUser).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        
                        ModelState.AddModelError(string.Empty, "Error creating user");
                    }
                }
            }

            return View("New", newUser);
        }
    }
}
