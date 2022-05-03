﻿namespace CoffeeService.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public event Action ProductsChanged;

        public List<Product> Products { get; set; }
        public string Message { get; set; } = "Loading products...";

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null
                ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured")
                : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            if (result != null && result.Data != null)
                Products = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No Products found";

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return result.Data;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<ProductSearchResultResponse>>($"api/product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
            {

                Products = result.Data.Products;
                CurrentPage = result.Data.CurentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0) Message = "No products found.";
            ProductsChanged?.Invoke();
        }
    }
}
