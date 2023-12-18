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

        [Theory(DisplayName ="TC1: Get Category By Valid Id")]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetCategoryById_With_Valid_CategoryId_Return_Category(int categoryId)
        {
            // Arrange
            // int categoryId = 1;


            // Act
            var categoryInDb = await _categoryService.GetAsync(categoryId);

            // Assert
            Assert.NotNull(categoryInDb);
            Assert.Equal(categoryId, categoryInDb.Id);
        }

        [Theory(DisplayName = "TC2: Get Category By Invalid Id")]
        [InlineData(10)]
        [InlineData(23)]
        public async Task GetCategoryById_With_Valid_CategoryId_Return_Null(int categoryId)
        {
            // Arrange


            // Act
            var categoryInDb = await _categoryService.GetAsync(categoryId);

            // Assert
            Assert.Null(categoryInDb);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
