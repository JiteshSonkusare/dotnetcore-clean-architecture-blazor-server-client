using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MudBlazorClient.Shared
{
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public partial class LogoutModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {

        }

        //public async Task<IActionResult> OnPost()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Redirect("Login");
        //}
    }
}
