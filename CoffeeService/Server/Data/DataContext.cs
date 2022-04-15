namespace CoffeeService.Server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                Price = 9.99m,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Title = "Russell Hobbs Chester Grind",
                Description = "Coffee Machine 22000 - Black",
                ImageUrl = "https://m.media-amazon.com/images/I/814DpiiLshL._AC_SX679_.jpg",
                Price = 12.50m,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Title = "De'Longhi Magnifica S",
                Description = "Automatic Bean to Cup Coffee Machine, Espresso and Cappuccino Maker, ECAM22.110.B, Black",
                ImageUrl = "https://m.media-amazon.com/images/I/61Gm5OKA6rL._AC_SX679_.jpg",
                Price = 35.00m,
                CategoryId = 1
            },
            new Product
            {
                Id = 4,
                Title = "Starbucks Medium Roast Ground Coffee — Pike Place Roast — 100% Arabica",
                Description = "Pike Place Roast is well-rounded with subtle notes of cocoa and toasted nuts balancing the smooth mouthfeel",
                ImageUrl = "https://m.media-amazon.com/images/I/71jZYgFuW+S._SX679_.jpg",
                Price = 12.00m,
                CategoryId = 2
            }
            );
        }
    }
}
