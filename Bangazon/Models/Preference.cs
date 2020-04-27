using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class Preference
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public bool? Like { get; set; }
    }
}
