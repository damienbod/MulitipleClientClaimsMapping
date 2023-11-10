using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MulitipleClientClaimsMapping.Pages;

[Authorize]
public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        if (User.Identity!.IsAuthenticated)
        {
            var authProperties = HttpContext.Features.GetRequiredFeature<IAuthenticateResultFeature>();
            var schemeToLogout = authProperties.AuthenticateResult!.Ticket!.Properties.Items[".AuthScheme"];

            if (schemeToLogout != null)
            {
                return SignOut(new AuthenticationProperties
                {
                    RedirectUri = "/SignedOut"
                },
                CookieAuthenticationDefaults.AuthenticationScheme,
                schemeToLogout);
            }
        }

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("/SignedOut");
    }
}