using MudBlazor;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using Client.Infrastructure.Configuration;
using Client.Infrastructure.Authentication;
using Client.Infrastructure.ApiClientManagers;
using ViewModel = Client.Infrastructure.ViewModels;

namespace MudBlazorClient.Pages.Features
{
    [Authorize]
    public partial class User
    {
        private bool _loaded;
        private string _searchString = "";
        private ViewModel.User _user = new();
        private List<ViewModel.User>? _userList = new();

        [Inject] private ITokenAcquisition _tokenAcquisition { get; set; }
        [Inject] private  IOptions<AuthConfig> AuthConfig { get; set; }
        [Inject] private IOptions<ApiClientConfig> ApiClientConfig { get; set; }
        
        private UserApiClientManager UserApiClientManager;

        protected override async Task OnInitializedAsync()
        {
            var authHandler = new AuthHandler(AuthConfig.Value, _tokenAcquisition);
            UserApiClientManager = new UserApiClientManager(ApiClientConfig.Value, authHandler);

            await GetUserAsync();
            _loaded = true;
        }

        private async Task GetUserAsync()
        {
            var response = await UserApiClientManager.GetAllAsync();
            if (response?.Status == 200)
            {
                _userList = response.Data.Data?.ToList();
            }
            else
            {
                _snackBar.Add(response?.ServerError.ToString(), Severity.Error);
            }
        }

        private bool Search(ViewModel.User user)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (user.FirstName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.LastName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.UserId?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

    }
}
