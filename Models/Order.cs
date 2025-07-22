using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Product { get; set; }

        [Required]
        public decimal? Quantity { get; set; }

        [Required]
        public decimal? Price { get; set; }

        private decimal? _totalPrice;

        [Required]
        public decimal? TotalPrice
        {
            get
            {
                if (Quantity.HasValue && Price.HasValue)
                    return Quantity.Value * Price.Value;
                return null;
            }
            set
            {
                _totalPrice = value;
            }
        }

        [Required]
        public DateTime? OrderDate { get; set; }

        [Required]
        public DateTime? DeliveredDate { get; set; }

        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}
