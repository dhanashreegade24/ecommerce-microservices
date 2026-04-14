using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
    }
}
