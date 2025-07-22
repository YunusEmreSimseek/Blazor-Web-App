using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>?> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _context.Orders.Where(o => o.CustomerId == customerId).OrderBy(o => o.OrderDate).ToListAsync();
        }
    

        public async Task AddOrderAsync(Order order)
        {
            order.TotalPrice = order.Quantity.HasValue && order.Price.HasValue
                ? order.Quantity.Value * order.Price.Value
                : 0; 
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.Id);
    if (existingOrder != null)
    {
        existingOrder.Product = order.Product;
        existingOrder.Price = order.Price;
        existingOrder.Quantity = order.Quantity;
        existingOrder.TotalPrice = order.Quantity.HasValue && order.Price.HasValue
            ? order.Quantity.Value * order.Price.Value
            : 0; 
        existingOrder.OrderDate = order.OrderDate;
        existingOrder.DeliveredDate = order.DeliveredDate;
        await _context.SaveChangesAsync();
    }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order is not null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

    }
}
