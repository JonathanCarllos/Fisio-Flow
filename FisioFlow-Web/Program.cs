using FisioFlow_Web.Extensions;
using FisioFlow_Web.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


// Permite acessar HttpContext dentro do Handler
builder.Services.AddHttpContextAccessor();


// Handler que envia o JWT para a API
builder.Services.AddTransient<AuthTokenHandler>();


// Session para guardar o JWT
builder.Services.AddSession();


// HttpClient da API
builder.Services.AddHttpClient("FisioFlowAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7161/");
})
.AddHttpMessageHandler<AuthTokenHandler>();



// Authentication
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


// Dependency Injection
builder.Services.AddRepositories();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// Session precisa vir antes dos Controllers
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