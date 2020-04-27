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
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    [Authorize]
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PaymentTypes
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var paymentTypes = await _context.PaymentType
                .Where(pt => pt.UserId == user.Id)
                .ToListAsync();
            return View(paymentTypes);
        }

        // GET: PaymentTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentType paymentType)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var paymentTypeInstance = new PaymentType
                {
                    Description = paymentType.Description,
                    AccountNumber = paymentType.AccountNumber,
                };

                paymentTypeInstance.UserId = user.Id;


                _context.PaymentType.Add(paymentTypeInstance);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentTypes/Edit/5
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

        // GET: PaymentTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentTypes/Delete/5
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