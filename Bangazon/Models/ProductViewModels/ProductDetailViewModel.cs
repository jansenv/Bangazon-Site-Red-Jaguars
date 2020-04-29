using Bangazon.Models;
using Bangazon.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Http;

namespace Bangazon.Models.ProductViewModels
{
  public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> ProductTypes { get; set; }
        public Preference Preference { get; set; }
        public int ProductId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string City { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
        public ProductType ProductType { get; set; }
        public bool HasLikeButton { get; set; }
        public bool HasDislikeButton { get; set; }
    }
}