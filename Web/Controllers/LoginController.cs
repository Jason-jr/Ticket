using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web.Models;

namespace Web.Controllers;

public class LoginController : Controller
{
    private readonly RestClient _apiClient;

    public LoginController(RestClient apiClient) => _apiClient = apiClient;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginModel user)
    {
        var request = new RestRequest("/api/auth/login", Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(user);
        var access = await _apiClient.ExecuteAsync<string>(request);
        if (!access.IsSuccessful)
        {
            ViewBag.ErrorMsg = access.Content + Environment.NewLine + access.ErrorMessage;
            return View();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Account),
            new Claim(ClaimTypes.Name, access.Data)
        };

        request = new RestRequest("/api/auth/roles", Method.Get)
            .AddHeader("Authorization", $"Bearer {access.Data}");
        var auth = await _apiClient.ExecuteAsync<string>(request);
        if (!auth.IsSuccessful)
        {
            ViewBag.ErrorMsg = auth.Content + Environment.NewLine + auth.ErrorMessage;
            return View();
        }

        claims = claims.Append(new Claim(ClaimTypes.Role, auth.Data)).ToArray();
        ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal principal = new(claimsIdentity);
        await HttpContext.SignInAsync(principal, new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) });

        return RedirectToAction("Index", "Home");
    }
}