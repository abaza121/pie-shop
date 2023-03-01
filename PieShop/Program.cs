using Microsoft.EntityFrameworkCore;
using PieShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var conString=builder.Configuration["ConnectionStrings:PieShopDbContextConnection"];
builder.Services.AddDbContext<PieShopDbContext>(options => options.UseSqlServer(conString));

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
if(app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.MapDefaultControllerRoute();
DbInitializer.Seed(app);
app.Run();