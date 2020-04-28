using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ViewModels
{
    public class TypeWithProduct
    {
        public int TypeId { get; set; }
        public string  TypeName { get; set; }
        public int ProductCount { get; set; }
        public List<Product> Products { get; set; }
    }
}
