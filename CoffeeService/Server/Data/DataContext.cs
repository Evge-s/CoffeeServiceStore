using CoffeeService.Shared.Order;

namespace CoffeeService.Server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(item => new { item.UserId, item.ProductId, item.ProductTypeId });

            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.ProductTypeId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductTypeId });

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Default" },
                new ProductType { Id = 2, Name = "500mg" },
                new ProductType { Id = 3, Name = "1kg" },
                new ProductType { Id = 4, Name = "5kg" }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Coffee Machines",
                    Url = "coffeeMachines"
                },
                new Category
                {
                    Id = 2,
                    Name = "Coffee",
                    Url = "coffee"
                }
                );

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "ESE Pod Compatible | Black",
                Description = "Breville One-Touch CoffeeHouse Coffee Machine | Espresso, Cappuccino & Latte Maker | 19 Bar Italian Pump | Automatic Milk Frother",
                ImageUrl = "https://m.media-amazon.com/images/I/81J4JXh9q3S._AC_SX679_.jpg",
                CategoryId = 1,
                Featured = true
            },
            new Product
            {
                Id = 2,
                Title = "Russell Hobbs Chester Grind",
                Description = "Coffee Machine 22000 - Black",
                ImageUrl = "https://m.media-amazon.com/images/I/814DpiiLshL._AC_SX679_.jpg",
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Title = "De'Longhi Magnifica S",
                Description = "Automatic Bean to Cup Coffee Machine, Espresso and Cappuccino Maker, ECAM22.110.B, Black",
                ImageUrl = "https://m.media-amazon.com/images/I/61Gm5OKA6rL._AC_SX679_.jpg",
                CategoryId = 1
            },
            new Product
            {
                Id = 4,
                Title = "Starbucks Medium Roast Ground Coffee — Pike Place Roast — 100% Arabica",
                Description = "Pike Place Roast is well-rounded with subtle notes of cocoa and toasted nuts balancing the smooth mouthfeel",
                ImageUrl = "https://m.media-amazon.com/images/I/71jZYgFuW+S._SX679_.jpg",
                CategoryId = 2
            }
            );

            modelBuilder.Entity<ProductVariant>().HasData(
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    Price = 29.99m,
                    OriginalPrice = 42.00m
                },
                new ProductVariant
                {
                    ProductId = 2,
                    ProductTypeId = 1,
                    Price = 37m,
                },
                new ProductVariant
                {
                    ProductId = 3,
                    ProductTypeId = 1,
                    Price = 34.99m,
                    OriginalPrice = 42.00m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 2,
                    Price = 9.99m,
                    OriginalPrice = 12.00m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 3,
                    Price = 17.99m,
                    OriginalPrice = 24.00m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 4,
                    Price = 17.99m,
                    OriginalPrice = 24.00m
                }
                );
        }
    }
}
