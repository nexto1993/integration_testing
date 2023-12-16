using ABCStore.Application.Services;
using ABCStore.Domain.Entities;
using ABCStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABCStore.Infrastructure.Services.Products
{
    public class ProductsService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (product == null)
            {
                return new Product();
            }
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<int> DeleteAsync(Product product)
        {
            if (product == null)
            {
                return 0;
            }
            _dbContext.Products.Remove(product);
            return product.Id;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int id)
        {
            return await _dbContext.Products.Include(prod => prod.Category)
                .Where(x => x.CategoryId==id).ToListAsync();
        }

        public async Task<int> UpdateAsync(Product product)
        {
            if (product == null)
            {
                return 0;
            }
            _dbContext.Products.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
