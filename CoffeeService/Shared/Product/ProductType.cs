using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeService.Shared
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public bool IsEditing { get; set; } = false;

        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
