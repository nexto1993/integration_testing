﻿using ABCStore.Application.Services;
using ABCStore.Domain.Entities;
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
            var categoryInDb = await _categoryService.GetAsync(categoryId);
            Assert.NotNull(categoryInDb);
            Assert.Equal(categoryId, categoryInDb.Id);
        }

        [Theory(DisplayName = "TC2: Get Category By Invalid Id")]
        [InlineData(10)]
        [InlineData(23)]
        public async Task GetCategoryById_With_Valid_CategoryId_Return_Null(int categoryId)
        {
            var categoryInDb = await _categoryService.GetAsync(categoryId);
            Assert.Null(categoryInDb);
        }

        [Fact(DisplayName = "TC3 : Get All categories")]

        public async Task GetAllCategories_Returns_Categories_List()
        {
            var categories = await _categoryService.GetAllAsync();
            Assert.NotEmpty(categories);
            Assert.NotNull(categories);
            Assert.Equal(2 , categories.Count());
        }

        [Fact(DisplayName = "TC4 : Get All categories empty")]

        public async Task GetAllCategories_Returns_Categories_Empty_List()
        {
            _context.Categories.RemoveRange(_context.Categories);
            _context.SaveChanges();
            var categories = await _categoryService.GetAllAsync();
            Assert.Empty(categories);
            Assert.Equal(0, categories.Count());
        }

        [Theory(DisplayName =("TC5 : Create Category with valid category input"))]
        [MemberData(nameof(CategoryParamData.GetValidCategory), MemberType = typeof(CategoryParamData))]

        public async Task CreateCataegory_With_Valid_Category_Should_Return_Category(Category category)
        {
            var newCategory = await _categoryService.CreateAsync(category);
            Assert.NotNull(newCategory);
            Assert.Equal(category.Id, newCategory.Id);
        }

        [Fact(DisplayName = ("TC6 : Create Category with Invalid category input"))]
        public async Task CreateCataegory_With_InValid_Category_Should_Return_Null()
        {
            var newCategory = await _categoryService.CreateAsync(null);
            Assert.Equal(new Category().Id, 0);

        }

        [Theory(DisplayName = ("TC7 : Update Categgory With Existing Category"))]
        [MemberData(nameof(CategoryParamData.GetValidCategoryForUpdating), MemberType = typeof(CategoryParamData))]
        public async Task UpdateCataegory_With_Valid_Category_Should_Return_Category(Category category)
        {
            var categoryInDb = await _categoryService.GetAsync(category.Id); 
            categoryInDb.Description = category.Description;
            categoryInDb.Name = category.Name;
            var updatedCategory = await _categoryService.UpdateAsync(categoryInDb);
            Assert.Equal(category.Id, updatedCategory);
        }

        [Theory(DisplayName = ("TC8 : Update Categgory With Existing Category with invalid"))]
        [MemberData(nameof(CategoryParamData.GetValidCategoryInvalidForUpdating), MemberType = typeof(CategoryParamData))]
        public async Task UpdateCataegory_With_InValid_Category_Should_Return_Zero(Category category)
        {
            var categoryInDb = await _categoryService.GetAsync(category.Id);
            var updatedCategory = await _categoryService.UpdateAsync(categoryInDb);
            Assert.Equal(0, updatedCategory);
        }

        [Theory(DisplayName = "TC9: Delete Category By Valid Id")]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteCataegory_With_Valid_CategoryId_Should_Return_CatId(int categoryId)
        {
            var existingCategory = await _categoryService.GetAsync(categoryId);
            var deletedCategory = await _categoryService.DeleteAsync(existingCategory);
            Assert.Equal(categoryId,deletedCategory);

        }

        [Theory(DisplayName = "TC10: Delete Category By InValid Id")]
        [InlineData(100)]
        [InlineData(200)]
        public async Task DeleteCataegory_With_InValid_CategoryId_Should_Return_Zero(int categoryId)
        {
            var existingCategory = await _categoryService.GetAsync(categoryId);
            var deletedCategory = await _categoryService.DeleteAsync(existingCategory);
            Assert.Equal(0, deletedCategory);

        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
