using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models
{
    public class Customer
    {
        public int? Index { get; set; }

        [Key]
        public string? Customer_Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Company { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone_1 { get; set; }
        public string? Phone_2 { get; set; }
        public string? Email { get; set; }
        public DateOnly? Subscription_Date { get; set; }
        public string? Website { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
