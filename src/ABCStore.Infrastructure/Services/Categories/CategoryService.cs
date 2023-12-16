using ABCStore.Application.Services;
using ABCStore.Domain.Entities;
using ABCStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABCStore.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            if (category == null)
            {
                return new Category();
            }
           await _dbContext.Categories.AddAsync(category);
           await _dbContext.SaveChangesAsync();
           return category;
        }

        public async Task<int> DeleteAsync(Category category)
        {
            if (category == null)
            {
                return 0;
            }
            _dbContext.Categories.Remove(category);
            return category.Id;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Category category)
        {
            if (category == null)
            {
                return 0;
            }
            _dbContext.Categories.Entry(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return category.Id;

        }
    }
}
