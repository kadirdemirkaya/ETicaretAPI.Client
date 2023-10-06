using ETicaretAPI.Client.Filters.Validation;
using ETicaretAPI.Client.Services.Authentication;
using ETicaretAPI.Client.Services.Basket;
using ETicaretAPI.Client.Services.Category;
using ETicaretAPI.Client.Services.Chat;
using ETicaretAPI.Client.Services.Communication;
using ETicaretAPI.Client.Services.Generic;
using ETicaretAPI.Client.Services.Order;
using ETicaretAPI.Client.Services.Product;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(ErrorHandlingFilter));
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
