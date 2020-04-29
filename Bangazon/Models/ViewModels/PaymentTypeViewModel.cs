using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models.ViewModels
{
    public class PaymentTypeViewModel
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        [DataType(DataType.Date)]
        [CheckDateRange]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(55)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }
    }
    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Expiration date cannot be in the past");
        }

    }
}
