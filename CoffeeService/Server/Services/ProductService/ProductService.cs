namespace CoffeeService.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(p => p.Featured && p.IsVisible && !p.IsDeleted)
                    .Include(p => p.Variants.Where(p => p.IsVisible && !p.IsDeleted))
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(p => p.Variants.Where(v => v.IsVisible && !v.IsDeleted))
                .ThenInclude(v => v.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted && p.IsVisible);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Invalid product ID";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.IsVisible && !p.IsDeleted)
                .Include(p => p.Variants.Where(p => p.IsVisible && !p.IsDeleted))
                .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var responce = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) &&
                    p.IsVisible && !p.IsDeleted)
                .Include(p => p.Variants.Where(p => p.IsVisible && !p.IsDeleted))
                .ToListAsync()
            };

            return responce;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string serachText)
        {
            var products = await FindProductsBySearchText(serachText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(serachText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(serachText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<ProductSearchResultResponse>> SearchProducts(string serachText, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(serachText)).Count / pageResults);
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(serachText.ToLower()) ||
                                    p.Description.ToLower().Contains(serachText.ToLower()) &&
                                    p.IsVisible && !p.IsDeleted)
                                .Include(p => p.Variants)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            var response = new ServiceResponse<ProductSearchResultResponse>
            {
                Data = new ProductSearchResultResponse
                {
                    Products = products,
                    CurentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private async Task<List<Product>> FindProductsBySearchText(string serachText)
        {
            return await _context.Products
                                .Where(p => p.Title.ToLower().Contains(serachText.ToLower()) ||
                                    p.Description.ToLower().Contains(serachText.ToLower()) &&
                                    p.IsVisible && !p.IsDeleted)
                                .Include(p => p.Variants)
                                .ToListAsync();
        }
    }
}
