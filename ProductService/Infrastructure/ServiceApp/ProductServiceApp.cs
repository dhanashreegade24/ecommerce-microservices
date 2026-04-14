using ProductService.Domain.Entities;
using ProductService.Infrastructure.DTO;
using ProductService.Infrastructure.Interface;

namespace ProductService.Infrastructure.ServiceApp
{
    public class ProductServiceApp
    {
        private readonly IProductRepository _repo;

        public ProductServiceApp(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repo.GetAllAsync();
        }

        public async Task AddProduct(ProductDto dto)
        {
            var product = new Product
            {
                Id = 1,
                Name = dto.Name,
                Price = dto.Price
            };

            await _repo.AddAsync(product);
        }
    }
}
