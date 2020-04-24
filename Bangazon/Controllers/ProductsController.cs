﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var allProductTypes = await _context.ProductType
                .ToListAsync();

            var viewModel = new ProductDetailViewModel();

            viewModel.ProductTypes = allProductTypes.Select(pt => new SelectListItem() 
            { 
                Text = pt.Label, 
                Value = pt.ProductTypeId.ToString() 
            }).ToList();

            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDetailViewModel productDetailViewModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var product = new Product
                {
                    DateCreated = productDetailViewModel.Product.DateCreated,
                    Description = productDetailViewModel.Product.Description,
                    Title = productDetailViewModel.Product.Title,
                    Price = productDetailViewModel.Product.Price,
                    Quantity = productDetailViewModel.Product.Quantity,
                    UserId = user.Id,
                    City = productDetailViewModel.Product.City,
                    ImagePath = productDetailViewModel.Product.ImagePath,
                    Active = productDetailViewModel.Product.Active,
                    ProductTypeId = productDetailViewModel.Product.ProductTypeId
                };

                _context.Product.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
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

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
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