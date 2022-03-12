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
    public class ProcessResponsesController : Controller
    {
        private readonly AppDbContext _context;
        private string tokenG;
        public ProcessResponsesController(AppDbContext context)
        {
            _context = context;
        }

        //GET: All Data
        public ActionResult AIndex()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/Package").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                try
                {
                    List<ProcessResponse> data = JsonConvert.DeserializeObject<List<ProcessResponse>>(stringData);
                    return View(data);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Error", "Home", e);
                }

            }
        }

        // GET: Package for Logged in User
        public ActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                string uname = HttpContext.Session.GetString("Username");
                tokenG = HttpContext.Session.GetString("token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync("/api/Package/" + uname).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;

                try
                {
                    List<ProcessResponse> data = JsonConvert.DeserializeObject<List<ProcessResponse>>(stringData);
                    return View(data);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Error", "Home", e);
                }

            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.processResponses.FirstOrDefaultAsync(m => m.RequestId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(ProcessResponse obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/api/Package", contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
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
                ProcessResponse data = JsonConvert.DeserializeObject<ProcessResponse>(stringData);
                return View(data);
            }
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(ProcessResponse obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("/api/Package/" + obj.RequestId, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View(obj);
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                HttpResponseMessage response = client.DeleteAsync("/api/Package/" + id).Result;
                TempData["Message"] = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index");
            }
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                HttpResponseMessage response = client.GetAsync("/api/Package/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                ProcessResponse data = JsonConvert.DeserializeObject<ProcessResponse>(stringData);
                return View(data);
            }
        }
    }
}
