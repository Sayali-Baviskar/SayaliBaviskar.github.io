using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC_UI.Controllers
{
    public class ComponentProcessingsController : Controller
    {
        private readonly AppDbContext _context;
        public ComponentProcessingsController(AppDbContext context)
        {
            _context = context;
        }

        private string tokenG;

        // GET: Orders
        public ActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                HttpResponseMessage response = client.GetAsync("/api/ComponentProcessings").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<ComponentProcessing> data = JsonConvert.DeserializeObject<List<ComponentProcessing>>(stringData);
                return View(data);
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.componentProcessings.FirstOrDefaultAsync(m => m.RequestId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(ComponentProcessing obj)
        {
            using (HttpClient client = new HttpClient())
            {
                obj.Name = HttpContext.Session.GetString("Username");
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/api/ComponentProcessings", contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Success";
                return View(obj);
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                HttpResponseMessage response = client.GetAsync("/api/ComponentProcessings/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                ComponentProcessing data = JsonConvert.DeserializeObject<ComponentProcessing>(stringData);
                return View(data);
            }
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(ComponentProcessing obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("/api/ComponentProcessings/" + obj.RequestId, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View(obj);
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                HttpResponseMessage response = client.DeleteAsync("/api/ComponentProcessings/" + id).Result;
                TempData["Message"] = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index", "Payments");
            }
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                HttpResponseMessage response = client.GetAsync("/api/ComponentProcessings/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                ComponentProcessing data = JsonConvert.DeserializeObject<ComponentProcessing>(stringData);
                return View(data);
            }
        }
    }
}
