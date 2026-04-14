using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Interface;

namespace ProductService.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();


        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
