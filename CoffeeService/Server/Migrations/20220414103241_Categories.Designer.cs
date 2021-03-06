// <auto-generated />
using CoffeeService.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeService.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220414103241_Categories")]
    partial class Categories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoffeeService.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Coffee Machines",
                            Url = "coffeeMachines"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Coffee",
                            Url = "coffee"
                        });
                });

            modelBuilder.Entity("CoffeeService.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Breville One-Touch CoffeeHouse Coffee Machine | Espresso, Cappuccino & Latte Maker | 19 Bar Italian Pump | Automatic Milk Frother",
                            ImageUrl = "https://m.media-amazon.com/images/I/81J4JXh9q3S._AC_SX679_.jpg",
                            Price = 9.99m,
                            Title = "ESE Pod Compatible | Black"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Coffee Machine 22000 - Black",
                            ImageUrl = "https://m.media-amazon.com/images/I/814DpiiLshL._AC_SX679_.jpg",
                            Price = 12.50m,
                            Title = "Russell Hobbs Chester Grind"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Automatic Bean to Cup Coffee Machine, Espresso and Cappuccino Maker, ECAM22.110.B, Black",
                            ImageUrl = "https://m.media-amazon.com/images/I/61Gm5OKA6rL._AC_SX679_.jpg",
                            Price = 35.00m,
                            Title = "De'Longhi Magnifica S"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = "Pike Place Roast is well-rounded with subtle notes of cocoa and toasted nuts balancing the smooth mouthfeel",
                            ImageUrl = "https://m.media-amazon.com/images/I/71jZYgFuW+S._SX679_.jpg",
                            Price = 12.00m,
                            Title = "Starbucks Medium Roast Ground Coffee — Pike Place Roast — 100% Arabica"
                        });
                });

            modelBuilder.Entity("CoffeeService.Shared.Product", b =>
                {
                    b.HasOne("CoffeeService.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
