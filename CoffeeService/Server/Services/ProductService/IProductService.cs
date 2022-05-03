namespace CoffeeService.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string serachText, int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string serachText);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    }
}
