using WebApp.Exceptions;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Authentication;
using Microsoft.Extensions.Options;
using WebApp.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Authorization;
using WebApp.Infrastructure.Authentication.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly UserApiClient _userApiClient;
        private readonly IOptions<ApiClientConfig> _apiClientConfig;

        public UserController(IOptions<ApiClientConfig> apiClientConfig, ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
            _apiClientConfig = apiClientConfig;
            var authHandler = new AuthHandler(_apiClientConfig.Value, _tokenAcquisition);
            _userApiClient = new UserApiClient(_apiClientConfig.Value, authHandler);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _userApiClient.GetAllAsync();
                if (result?.Data == null)
                    ViewBag("No User found");

                return View(result?.Data.Data);
            }
            catch (Exception ex)
            {
                throw new GeneralApplicationException("Failed to get user data from API.", ex);
            }
        }
    }
}
