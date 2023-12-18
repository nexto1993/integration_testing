using ABCStore.Application.Services;
using ABCStore.Infrastructure.Data;
using ABCStore.Infrastructure.Services.Categories;
using Infrastructure.IntegrationTests.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.IntegrationTests
{
    public class CategoryServiceIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;

        public CategoryServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);
            _categoryService = new CategoryService(_context);
            var initDataJson = File.ReadAllText("Data\\SeedData.json");
            var initData = JsonConvert.DeserializeObject<DataInit>(initDataJson);
            _context.Categories.AddRange(initData.Categories);
            _context.Products.AddRange(initData.Products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetCategoryById_With_Valid_CategoryId_Return_Category()
        {
            // Arrange
            int categoryId = 1;


            // Act
            var categoryInDb = await _categoryService.GetAsync(categoryId);

            // Assert
            Assert.NotNull(categoryInDb);
            Assert.Equal(categoryId, categoryInDb.Id);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
