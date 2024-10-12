
using Microsoft.AspNetCore.Identity;
 using Microsoft.EntityFrameworkCore;
using PH.Data;
using PH.Web.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationServices(builder);
builder.Services.ConfigureAppDbServices(builder);

//
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Login}");
app.Run();