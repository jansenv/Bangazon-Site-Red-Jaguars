using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bangazon.Controllers
{
    [Authorize]
    public class PreferencesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PreferencesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Preferences
        public ActionResult Index()
        {
            return View();
        }

        // GET: Preferences/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Preferences/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Like(Preference likePreference)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var likeInstance = new Preference
                {
                    ProductId = likePreference.ProductId,
                    Like = true,
                };

                likeInstance.UserId = user.Id;

                _context.Preference.Add(likeInstance);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Products");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Dislike(Preference dislikePreference)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var dislikeInstance = new Preference
                {
                    ProductId = dislikePreference.ProductId,
                    Like = false,
                };

                dislikeInstance.UserId = user.Id;

                _context.Preference.Add(dislikeInstance);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Products");
            }
            catch
            {
                return View();
            }
        }


        // GET: Preferences/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Preferences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Preferences/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Preferences/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}