namespace CoffeeService.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Product>> CreateProduct(Product product)
        {
            foreach (var variant in product.Variants)
            {
                variant.ProductType = null;
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            var dbProduct = await _context.Products.FindAsync(productId);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found"
                };
            }

            dbProduct.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(p => !p.IsDeleted)
                    .Include(p => p.Variants.Where(p => !p.IsDeleted))
                    .ThenInclude(v => v.ProductType)
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(p => p.Featured && p.IsVisible && !p.IsDeleted)
                    .Include(p => p.Variants.Where(p => p.IsVisible && !p.IsDeleted))
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            Product product = null;

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _context.Products
                    .Include(p => p.Variants.Where(v => !v.IsDeleted))
                    .ThenInclude(v => v.ProductType)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            }
            else
            {
                product = await _context.Products
                   .Include(p => p.Variants.Where(v => v.IsVisible && !v.IsDeleted))
                   .ThenInclude(v => v.ProductType)
                   .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted && p.IsVisible);
            }

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
                .Include(p => p.Images)
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
                .Include(p => p.Images)
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
                                .Include(p => p.Images)
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

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products
                 .Include(p => p.Images)
                 .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.IsVisible = product.IsVisible;
            dbProduct.Featured = product.Featured;

            var productImages = dbProduct.Images;
            if (productImages != null && productImages.Count > 0)
                _context.Images.RemoveRange(productImages);

            dbProduct.Images = product.Images;

            foreach (var variant in product.Variants)
            {
                var dbVariant = await _context.ProductVariants
                    .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
                        v.ProductTypeId == variant.ProductTypeId);
                if (dbVariant == null)
                {
                    variant.ProductType = null;
                    _context.ProductVariants.Add(variant);
                }
                else
                {
                    dbVariant.ProductTypeId = variant.ProductTypeId;
                    dbVariant.Price = variant.Price;
                    dbVariant.OriginalPrice = variant.OriginalPrice;
                    dbVariant.IsVisible = variant.IsVisible;
                    dbVariant.IsDeleted = variant.IsDeleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
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
