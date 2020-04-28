using Bangazon.Models;
using Bangazon.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Bangazon.Models.ProductViewModels
{
  public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> ProductTypes { get; set; }
        public Preference Preference { get; set; }
        public int ProductId { get; set; }
        public DateTime DateCreated { get;set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string UserId {get; set;}
        public string City {get; set;}
        public string ImagePath {get; set;}
        public bool Active { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}