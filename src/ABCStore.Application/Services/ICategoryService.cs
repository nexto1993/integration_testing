using ABCStore.Domain.Entities;

namespace ABCStore.Application.Services
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<int> DeleteAsync(Category category);
        Task<Category> CreateAsync(Category category);
        Task<int> UpdateAsync(Category category);
    }
}
