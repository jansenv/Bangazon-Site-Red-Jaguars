using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated {get;set;}

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(55, ErrorMessage="Please shorten the product title to 55 characters")]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0.01, 10000.00,
            ErrorMessage = "Price must be between $0.01 and $10,000")]
        public double Price { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity,
            ErrorMessage = "Quantity Must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public string UserId {get; set;}

        public string City {get; set;}

        public string ImagePath {get; set;}

        public bool Active { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Product type is required")]
        [Display(Name="Product Category")]
        public int ProductTypeId { get; set; }

        
        public ProductType ProductType { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public Product ()
        {
            Active = true;
        }

    }
}
