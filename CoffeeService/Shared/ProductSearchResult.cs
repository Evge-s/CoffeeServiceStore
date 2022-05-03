namespace CoffeeService.Shared
{
    public class ProductSearchResult
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Pages { get; set; }
        public int CurentPage { get; set; }
    }
}
