using FisioFlow_Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace FisioFlow_Web.Controllers;

public class AuthController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    private const string ApiEndpoint = "api/Auth/";

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Login()
    {     
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var client = _httpClientFactory
                .CreateClient("FisioFlowAPI");

            var request = new
            {
                username = model.Username,
                password = model.Password
            };

            var response = await client.PostAsJsonAsync(
                $"{ApiEndpoint}login",
                request);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"API retornou HTTP {(int)response.StatusCode}: {responseContent}");

                return View(model);
            }

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "A API retornou uma resposta vazia.");

                return View(model);
            }

            using var document =
                JsonDocument.Parse(responseContent);

            var root = document.RootElement;

            string? token = null;

            if (root.TryGetProperty(
                    "token",
                    out var tokenProperty))
            {
                token = tokenProperty.GetString();
            }

            if (string.IsNullOrWhiteSpace(token) &&
                root.TryGetProperty(
                    "accessToken",
                    out var accessTokenProperty))
            {
                token = accessTokenProperty.GetString();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Nenhum token foi encontrado na resposta da API.");

                return View(model);
            }

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            var claims = new List<Claim>();

            foreach (var claim in jwt.Claims)
            {
                if (
                    claim.Type.Equals(
                        "role",
                        StringComparison.OrdinalIgnoreCase) ||
                    claim.Type.Equals(
                        "roles",
                        StringComparison.OrdinalIgnoreCase) ||
                    claim.Type.Equals(
                        ClaimTypes.Role,
                        StringComparison.OrdinalIgnoreCase))
                {
                    claims.Add(
                        new Claim(
                            ClaimTypes.Role,
                            claim.Value));
                }
                else if (
                    claim.Type.Equals(
                        "name",
                        StringComparison.OrdinalIgnoreCase) ||
                    claim.Type.Equals(
                        "unique_name",
                        StringComparison.OrdinalIgnoreCase) ||
                    claim.Type.Equals(
                        ClaimTypes.Name,
                        StringComparison.OrdinalIgnoreCase))
                {
                    claims.Add(
                        new Claim(
                            ClaimTypes.Name,
                            claim.Value));
                }
                else
                {
                    claims.Add(claim);
                }
            }

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name,
                ClaimTypes.Role);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc =
                        DateTimeOffset.UtcNow.AddHours(8),
                    AllowRefresh = true
                });

            if (principal.IsInRole("Admin"))
            {
                return RedirectToAction(
                    "Index",
                    "Admin",
                    new { area = "Admin" });
            }

            return RedirectToAction(
                "Index",
                "Home");
        }
        catch (HttpRequestException ex)
        {
            ModelState.AddModelError(
                string.Empty,
                $"Não foi possível conectar à API: {ex.Message}");

            return View(model);
        }
        catch (JsonException ex)
        {
            ModelState.AddModelError(
                string.Empty,
                $"Resposta inválida da API: {ex.Message}");

            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                $"Erro no login: {ex.Message}");

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(
            "Login",
            "Auth");
    }

    [HttpGet]
    public IActionResult AcessoNegado()
    {
        return View();
    }
}