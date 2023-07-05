using LampStore.Models;
using LampStore.Models.AboutPages;
using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LampStore.Pages.Admin.Services.Interface;
using LampStore.Pages.Admin.Services;
using SimpleMvcSitemap;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration["ConnectionStrings:LampStoreConnection"]);
});

builder.Services.AddScoped<IAboutPageRepository, EFAboutPageRepository>();
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<ICatalogRepository, EFCatalogRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<ISettingsRepository, EFSettingsRepository>();
builder.Services.AddScoped<IInfoRepository, EFInfoRepository>();
builder.Services.AddScoped<ICooperationRepository, EFCooperationRepository>();
builder.Services.AddScoped<IConfidentPolicyRepository, EFConfidentPolicyRepository>();
builder.Services.AddScoped<ICollectionLight, EFCollectionLight>();
builder.Services.AddScoped<IModelLight, EFModelLight>();
builder.Services.AddDistributedMemoryCache(); //добавляем кеш в приложение
builder.Services.AddSession(); //добавление сохранения сессии пользователя
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); //указывает, что один и тот же объект должен использоваться для удовлетворения связанных запросов для экземпляров Корзины
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //указывает, что всегда должен использоваться один и тот же объект
builder.Services.AddServerSideBlazor(); //создает службы, которые использует Blazor
builder.Services.AddSingleton<ISitemapProvider, SitemapProvider>(); //карта сайта

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"])); //подключение к БД для админа
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>(); //добавление идентифкации для админа
builder.Services.AddHttpClient(); //отправка файлов на сервер

builder.Services.AddSingleton<IPopupNotification, PopupNotification>(); //регистриуем сервис уведомлений 
builder.Services.AddScoped<IFolderManager, FolderManager>(); //регистриуем сервис для создания/удаления папок
builder.Services.AddScoped<IMetaManager, MetaManager>(); //регистриуем сервис для создания мета
builder.Services.AddScoped<IPageStateService, PageStateService>(); //регистриуем сервис для сохранеия странциы

var app = builder.Build();


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
app.UseStatusCodePagesWithRedirects("/errors/{0}"); //стртаницы ошибок
app.UseAuthentication();
app.UseAuthorization();

app.UseSession(); //включения фукции сессии

app.MapRazorPages();
app.MapBlazorHub();//регистрирует компоненты промежуточного программного обеспечения Blazor.
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index"); //усовершенствовании системы маршрутизации, чтобы гарантировать бесперебойную работу Blazor с остальной частью приложения.

IdentitySeedData.EnsurePopulated(app);

app.Run();
