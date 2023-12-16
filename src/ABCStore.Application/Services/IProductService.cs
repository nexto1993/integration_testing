using ABCStore.Domain.Entities;

namespace ABCStore.Application.Services
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<int> DeleteAsync(Product category);
        Task<Product> CreateAsync(Product category);
        Task<int> UpdateAsync(Product category);
    }
}
