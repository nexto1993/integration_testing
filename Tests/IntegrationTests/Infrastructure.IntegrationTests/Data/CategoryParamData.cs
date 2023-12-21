using ABCStore.Domain.Entities;

namespace Infrastructure.IntegrationTests.Data
{
    public static class CategoryParamData
    {
        public static IEnumerable<object[]> GetValidCategory()
        {
            yield return new object[]
            {
                new Category(){Id=3, Name="cat1" , Description="desc 1"}
            };
            yield return new object[]
            {
                new Category(){Id=4, Name="cat2" , Description="desc 3"}
            };
        }
    }
}
