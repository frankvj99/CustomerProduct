using System.Threading.Tasks;
using AutoMapper;
using Interview.Data.Contexts;
using Interview.Data.Entities;
using Interview.Data.Models;
using Interview.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Interview.Data.Services;

public class CustomerService : ICustomerService
{
    private readonly InterviewContext _context;
    private readonly IMapper _mapper;

    public CustomerService(InterviewContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddOrUpdateCustomerAsync(Customer customer)
    {
        var existingCustomerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Email == customer.Email);

        if (existingCustomerEntity is not null)
        {
            existingCustomerEntity.LastProduct = customer.LastProduct;
        }
        else
        {
            var customerEntity = _mapper.Map<CustomerEntity>(customer);

            await _context.Customers.AddAsync(customerEntity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Customer> GetCustomerAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);

        if (customerEntity is not null)
        {
            return _mapper.Map<Customer>(customerEntity);
        }

        return null;
    }
}
