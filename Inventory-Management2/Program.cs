using Inventory_Management.Data;
using Inventory_Management2.Interfaces;
using Inventory_Management2.Repositories;
using Inventory_Management2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Inventory_Management2.Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("POSTGRESQL_CONNECTION_STRING") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var authConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ItemContext>(opt =>
    opt.UseNpgsql((connectionString))
);

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(authConnectionString));



builder.Services.AddRazorPages();



builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.Configure<MailService>(builder.Configuration.GetSection("MailSettings"));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    //options.AccessDeniedPath = "/Error";
});

builder.Services.AddScoped<IMailRepository, MailRepository>();

builder.Services.AddHttpClient<IWebClientRepository, WebClientRepository>(client =>
{
    
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("API_BASE_ADDRESS"));
})
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
{
    UseCookies = true,
    CookieContainer = new CookieContainer()
}); ;

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
