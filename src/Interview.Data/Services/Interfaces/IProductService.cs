using System.Threading.Tasks;
using Interview.Data.Models;

namespace Interview.Data.Services.Interfaces;

public interface IProductService
{
    Task AddOrUpdateProductAsync(Product product);

    Task<Product> GetProductAsync(int id);
}
