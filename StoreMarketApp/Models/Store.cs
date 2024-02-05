namespace StoreMarketApp.Models
{
    public class Store
    {
        public int Id { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public int Count { get; set; }

    }
}
