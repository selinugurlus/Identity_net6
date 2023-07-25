using blog.Identity;
using blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);



var ConnectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(ConnectionString)
);
//var PGConnectionString = builder.Configuration.GetConnectionString("PGSqlConnection");
//builder.Services.AddDbContext<BlogContext>(options => options.UseNpgsql(PGConnectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<BlogContext>();

builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option => { option.LoginPath = "/Account/Login"; });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ModeratorPolicy", policy => policy.RequireRole("Moderator"));
    // Diðer rolleri buraya ekleyebilirsiniz
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
