using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeService.Shared
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsVisible { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        public bool IsEditing { get; set; } = false;

        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
