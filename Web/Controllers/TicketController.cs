using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web.Models;

namespace Web.Controllers;

public class TicketController : Controller
{
    private readonly RestClient _apiClient;

    public TicketController(RestClient apiClient) => _apiClient = apiClient;

    public async Task<IActionResult> Index()
    {
        var auths = await ClientGetAsync<IList<TicketAuthModel>>("/api/auth/ticket");
        ViewData["Create"] = auths.Any(a => a.CanCreate);
        return View();
    }

    public async Task<IActionResult> Detail(int? id, [FromQuery(Name = "t")] int? type = null)
    {
        var auths = await ClientGetAsync<IList<TicketAuthModel>>("/api/auth/ticket");
        var detail = new TicketDetailViewModel
        {
            Id = id,
            Type = type,
            Auth = type.HasValue ? auths.FirstOrDefault(a => a.Type == type) ?? new TicketAuthModel() : new TicketAuthModel { CanCreate = true },
            Ticket = id.HasValue ? await ClientGetAsync<TicketModel>($"/api/ticket/{id}") : new TicketModel()
        };

        return View(detail);
    }

    private async Task<T> ClientGetAsync<T>(string url)
    {
        var request = new RestRequest(url, Method.Get)
            .AddHeader("Authorization", $"Bearer {User.FindFirstValue(ClaimTypes.Role)}");
        var response = await _apiClient.ExecuteAsync<T>(request);
        return response.Data;
    }
}