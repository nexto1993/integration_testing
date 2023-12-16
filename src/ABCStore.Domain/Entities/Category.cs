namespace ABCStore.Domain.Entities
{
    public class Category : BaseEntitiy
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
