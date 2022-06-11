namespace CoffeeService.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Category> AdminCategories { get; set; } = new List<Category>();

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (response != null && response.Data != null)
                Categories = response.Data;
        }

        public async Task GetAdminCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category/admin");
            if (response != null && response.Data != null)
                AdminCategories = response.Data;
        }

        public async Task AddCategories(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/category/admin", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public async Task UpdateCategories(Category category)
        {
            var response = await _http.PutAsJsonAsync($"api/category/admin/", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public async Task DeleteCategories(int categoryId)
        {
            var response = await _http.DeleteAsync($"api/category/admin/{categoryId}");
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public Category CreateNewCategory()
        {
            var newCategory = new Category { IsNew = true, IsEditing = true };
            AdminCategories.Add(newCategory);
            OnChange.Invoke();
            return newCategory;
        }
    }
}
