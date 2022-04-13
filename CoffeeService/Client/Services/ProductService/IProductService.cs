namespace CoffeeService.Client.Services.ProductService
{
    public interface IProductService
    {
        List<Product> Products { get; set; }

        Task GetProducts();
        Task<Product> GetProductAsync(int id);
    }
}
