using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ViewModels
{
    public class ProductTypeViewModel
    {
       

        public List<TypeWithProduct> Types { get; set; }

    }
}
