using System;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext ctx, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = ctx;
        }

        // GET: Products
        public async Task<ActionResult> Index(string searchString, string citySearchString, int? id)
        {
            var products = from p in _context.Product
                           select p;
            if (id != null)
            {
                products = products.Where(i => i.ProductTypeId == id);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(citySearchString))
            {
                products = products.Where(s => s.City.Contains(citySearchString));
            }

            return View(await products.ToListAsync());
        }
        //public async Task<ActionResult> TypeIndex (int id)
        //{
        //    var products = from p in _context.Product
        //                   .Where(p=> p.ProductTypeId == id)
        //                   select p;
        //    return View(await products.ToListAsync());

        //}

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int id)
        {
            // Find the product matching the ID passed in
            var product = await _context.Product
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            // Check to see if a user is signed in so that controller will use this information. Otherwise details page will break if user is not logged in, because it is looking for user Preference info that doesn't exist.
            if (_signInManager.IsSignedIn(User))
            {
                // Get logged in user
                var user = await GetCurrentUserAsync();
                
                // Find the row in the Preference table that matches the product ID and logged in User and turn it into a List (this is crappy code since the List will always be a single item but idk how to refactor it yet)
                var userPreferences = await _context.Preference
                    .Where(p => p.ProductId == id)
                    .Where(p => p.UserId == user.Id)
                    .ToListAsync();
                
                // Find the same thing but this time just convert it into an object
                var userLikeOrDislike = await _context.Preference
                    .Where(p => p.ProductId == id)
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);
                
                // Make the view model
                var viewModel = new ProductDetailViewModel()
                {
                    ProductId = product.ProductId,
                    DateCreated = product.DateCreated,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    ProductType = product.ProductType
                };

                // Write an if statement that will render the Like/Dislike buttons if the user does not have a preference (if the length of the List we made is 0 that means there is no preference stored for the user)
                if (userPreferences.Count == 0)
                {
                    viewModel.HasLikeButton = true;
                    viewModel.HasDislikeButton = true;
                } else
                
                // Now check that object we made that has the same info and drill down to see if the user has a preference. These will only trigger if that preference exists and they will render an option based on if the user Liked or Dislike before.
                if (userLikeOrDislike.Like == true)
                {
                    viewModel.HasLikeButton = false;
                    viewModel.HasDislikeButton = true;
                } else
                if (userLikeOrDislike.Like == false)
                {
                    viewModel.HasLikeButton = true;
                    viewModel.HasDislikeButton = false;
                }
                
                // Now render that View!
                return View(viewModel);
            }
            
            // Remember when we said the product details view will break if there isn't a user logged in? This is what we do to prevent that. Instead of rendering a view that has a dependency on user Preference we take all of that info out and don't worry about the Like/Dislike buttons doing anything anymore. We can still render them on the details page if we so choose, but when the user clicks them it can just redirect them to the login page.

            if (!_signInManager.IsSignedIn(User))
            {
                var viewModel = new ProductDetailViewModel()
                {
                    ProductId = product.ProductId,
                    DateCreated = product.DateCreated,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    ProductType = product.ProductType
                };

                return View(viewModel);
            }
            
            // Had to put this here or else I would get an error saying 'Not all code paths return a value'. I don't get it either.
            return View(product);

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

                return RedirectToAction("Details", new { id = product.ProductId });

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