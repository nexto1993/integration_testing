using ABCStore.Domain.Entities;

namespace ABCStore.Application.Services
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryAsync(int id);

        Task<IEnumerable<Product>> GetAllAsync();
        Task<int> DeleteAsync(Product product);
        Task<Product> CreateAsync(Product product);
        Task<int> UpdateAsync(Product product);
    }
}
