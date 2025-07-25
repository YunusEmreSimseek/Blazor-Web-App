﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal? Quantity { get; set; }

        [Required]
        public decimal? TotalPrice { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        [Required]
        public DateTime? DeliveredDate { get; set; }

        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
