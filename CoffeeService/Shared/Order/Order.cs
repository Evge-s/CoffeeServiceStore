using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeService.Shared.Order
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }
}
