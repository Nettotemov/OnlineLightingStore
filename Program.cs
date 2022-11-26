using LampStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration["ConnectionStrings:LampStoreConnection"]);
});
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<ICatalogRepository, EFCatalogRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddDistributedMemoryCache(); //добавляем кеш в приложение
builder.Services.AddSession(); //добавление сохранения сессии пользователя
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); //указывает, что один и тот же объект должен использоваться для удовлетворения связанных запросов для экземпляров Корзины
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //указывает, что всегда должен использоваться один и тот же объект

var app = builder.Build();
app.UseSession(); //включения фукции сессии

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapDefaultControllerRoute(); //рассказывает ASP.NET Core, как сопоставить URL
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
