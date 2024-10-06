using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace MulitipleClientClaimsMapping.Controllers;

[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet("LoginOpenIddict")]
    public ActionResult LoginOpenIddict(string returnUrl)
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/",
        },
            "t2");
    }

    [HttpGet("LoginIdentityServer")]
    public ActionResult LoginIdentityServer(string returnUrl)
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/"
        },
            "t1");
    }

    /// <summary>
    /// Can be used if you need to link from the UI
    /// You can also use the logout page
    /// </summary>
    [ValidateAntiForgeryToken]
    [Authorize]
    [HttpPost("Logout")]
    public IActionResult Logout()
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

        // default, does not logout of the OIDC server because the scheme does not match
        return SignOut(new AuthenticationProperties
        {
            RedirectUri = "/SignedOut"
        },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}
