using System.Threading.Tasks;
using Interview.Data.Models;

namespace Interview.Data.Services.Interfaces;

public interface ICustomerService
{
    Task AddOrUpdateCustomerAsync(Customer customer);

    Task<Customer> GetCustomerAsync(string email);
}
