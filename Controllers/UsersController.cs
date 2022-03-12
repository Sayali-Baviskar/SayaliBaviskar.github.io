using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_UI.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC_UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("Username", "");
            return View();
        }
        public IActionResult Welcome()
        {
            var user = HttpContext.Session.GetString("Username");
            ViewBag.Username = user;
            return View();
        }
        public IActionResult WelcomeAdmin()
        {
            var user = HttpContext.Session.GetString("Username");
            ViewBag.Username = user;
            return View();
        }
        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }
        // POST: Users/Login       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                string u = JsonConvert.SerializeObject(user);
                var contentData = new StringContent(u, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/Login", contentData).Result;
                //ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                string result = response.Content.ReadAsStringAsync().Result;

                if (!result.Equals("Unauthorized"))
                {
                    HttpContext.Session.SetString("token", result);
                    HttpContext.Session.SetString("Username", user.UserName);
                    if (user.UserName == "Admin")
                        return RedirectToAction("WelcomeAdmin");
                    else
                        return RedirectToAction("Welcome");
                }
                else
                    ViewBag.Mystm = "Wrong Crediatials";
                return View();
            }
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380");

                string tokenG = HttpContext.Session.GetString("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenG);

                string stringData = JsonConvert.SerializeObject(user);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/api/Users", contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View();
            }

            //if (ModelState.IsValid)
            //{
            //    _context.Add(user);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Password")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
