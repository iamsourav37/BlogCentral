using BlogCentral.Web.Models.Data;
using BlogCentral.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Injecting the services
builder.Services.AddScoped<ITagRepository, TagRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogCentralDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DevelopmentConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area=Public}/{controller=Home}/{action=Index}/{id?}");


app.Run();
