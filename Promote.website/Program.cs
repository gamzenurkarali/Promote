using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Bu namespace'i ekleyin
using Promote.website.Models;
using Promote.website.Services;
var builder = WebApplication.CreateBuilder(args);

// Servisleri konteynere ekle
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddRazorRuntimeCompilation();

// IConfiguration nesnesini al
var configuration = builder.Configuration;

// Baðlantý dizesini ekleyin
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
// Add LayoutService to the DI container
builder.Services.AddScoped<LayoutService>();
// Kimlik doðrulama servislerini ekle
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    // Çerez kimlik doðrulama seçenekleri
    options.Cookie.Name = "SizinCerezAdiniz"; // Ýhtiyaca göre özel bir çerez adý belirleyin
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// HTTP isteði pipeline'ýný yapýlandýr
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Session
app.UseSession();
// Yetkilendirme öncesinde kimlik doðrulamayý kullan
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
