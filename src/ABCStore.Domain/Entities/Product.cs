namespace ABCStore.Domain.Entities
{
    public class Product : BaseEntitiy
    {
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
