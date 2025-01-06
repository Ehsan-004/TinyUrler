using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyUrler_DotNetVersion.Data;
using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.Models.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IlinkRepository, LinkRepository>();
builder.Services.AddDbContext<TContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Authentication
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true; // نیاز به حداقل یک عدد
        options.Password.RequireLowercase = true; // نیاز به حداقل یک حرف کوچک
        options.Password.RequireNonAlphanumeric = true; // نیاز به حداقل یک کاراکتر خاص (!@#$%^&*)
        options.Password.RequiredLength = 8; // حداقل طول رمز عبور
    })
    .AddEntityFrameworkStores<TContext>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();
builder.Services.AddSession();  // Cookie authentication

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider; 
//     var userManager = services.GetRequiredService<UserManager<AppUser>>();
//     var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//     
//     if (!await roleManager.RoleExistsAsync("Admin"))
//     {
//         await roleManager.CreateAsync(new IdentityRole("Admin"));
//     }
//
//     var adminName = "Admin";
//     var adminEmail = "admin@example.com";
//     var adminPassword = "Admin1234!";
//     var adminUser = await userManager.FindByEmailAsync(adminEmail);
//     if (adminUser == null)
//     {
//         adminUser = new AppUser
//         {
//             UserName = adminName, Email = adminEmail, EmailConfirmed = true
//         };
//         await userManager.CreateAsync(adminUser, adminPassword);
//         await userManager.AddToRoleAsync(adminUser, "Admin");
//     }
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();