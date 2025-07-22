using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services
{
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Customer_Id);
            if (existingCustomer == null) return;
            existingCustomer.Index = customer.Index;
            existingCustomer.Customer_Id = customer.Customer_Id;
            existingCustomer.City = customer.City;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone_1 = customer.Phone_1;
            existingCustomer.Phone_2 = customer.Phone_2;
            existingCustomer.Company = customer.Company;
            existingCustomer.Country = customer.Country;
            existingCustomer.First_Name = customer.First_Name;
            existingCustomer.Last_Name = customer.Last_Name;
            existingCustomer.Subscription_Date = customer.Subscription_Date;
            existingCustomer.Website = customer.Website;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Customer_Id == id);
            if (customer is not null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Customer_Id == id);  
        }
    }
}
