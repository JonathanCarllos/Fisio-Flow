using FisioFlow_Web.Extensions;
using FisioFlow_Web.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ======================================
// Localização (pt-BR)
// ======================================
var supportedCultures = new[]
{
    new CultureInfo("pt-BR")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// ======================================
// MVC
// ======================================
builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        // Faz o ASP.NET utilizar as mensagens do [Required]
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });

// ======================================
// HttpContext
// ======================================
builder.Services.AddHttpContextAccessor();

// ======================================
// Session
// ======================================
builder.Services.AddSession();

// ======================================
// Handler JWT
// ======================================
builder.Services.AddTransient<AuthTokenHandler>();

// ======================================
// HttpClient
// ======================================
builder.Services.AddHttpClient("FisioFlowAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7161/");
})
.AddHttpMessageHandler<AuthTokenHandler>();

// ======================================
// Autenticação
// ======================================
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AcessoNegado";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// ======================================
// Injeção de Dependência
// ======================================
builder.Services.AddRepositories();

var app = builder.Build();

// ======================================
// Pipeline
// ======================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Aplica a cultura pt-BR
app.UseRequestLocalization();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();