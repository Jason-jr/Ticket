using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly RestClient _apiClient;

        public UserController(RestClient apiClient) => _apiClient = apiClient;
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            var request = new RestRequest($"/api/user/{id}", Method.Get)
                .AddHeader("Authorization", $"Bearer {User.FindFirstValue(ClaimTypes.Role)}");
            var response = await _apiClient.ExecuteAsync<UserModel>(request);            
            var detail = new UserDetailViewModel
            {
                Id = id,
                User = id.HasValue ? response.Data : new UserModel()
            };

            return View(detail);
        }
    }
}