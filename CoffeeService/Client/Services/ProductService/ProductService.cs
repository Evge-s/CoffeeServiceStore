namespace CoffeeService.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        public List<Product> Products { get; set; }

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task GetProducts()
        {
            var result =
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            if (result != null && result.Data != null)
                Products = result.Data;
        }
    }
}
