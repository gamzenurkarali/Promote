using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Bu namespace'i ekleyin
using Promote.website.Models;

var builder = WebApplication.CreateBuilder(args);

// Servisleri konteynere ekle
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddRazorRuntimeCompilation();

// IConfiguration nesnesini al
var configuration = builder.Configuration;

// Ba�lant� dizesini ekleyin
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Kimlik do�rulama servislerini ekle
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    // �erez kimlik do�rulama se�enekleri
    options.Cookie.Name = "SizinCerezAdiniz"; // �htiyaca g�re �zel bir �erez ad� belirleyin
});

var app = builder.Build();

// HTTP iste�i pipeline'�n� yap�land�r
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Yetkilendirme �ncesinde kimlik do�rulamay� kullan
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
