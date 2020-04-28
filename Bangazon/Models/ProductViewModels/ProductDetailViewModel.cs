using Bangazon.Models;
using Bangazon.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bangazon.Models.ProductViewModels
{
  public class ProductDetailViewModel
  {
    public Product Product { get; set; }
    public Preference Preference { get; set; }
    public List<SelectListItem> ProductTypes { get; set; }
  }
}