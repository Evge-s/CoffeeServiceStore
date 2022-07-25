global using CoffeeService.Shared;
global using CoffeeService.Shared.User;
global using CoffeeService.Shared.Order;
global using CoffeeService.Client.Services.AuthService;
global using CoffeeService.Client.Services.CartService;
global using CoffeeService.Client.Services.CategoryService;
global using CoffeeService.Client.Services.ProductService;
global using CoffeeService.Client.Services.OrderService;
global using CoffeeService.Client.Services.AddressService;
global using CoffeeService.Shared.DeliveryAddress;
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Components.Authorization;
global using CoffeeService.Client.Services.ProductTypeService;
using CoffeeService.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
