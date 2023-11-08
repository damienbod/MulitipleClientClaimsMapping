using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
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
        }, "t1");
    }

    [HttpGet("LoginIdentityServer")]
    public ActionResult LoginIdentityServer(string returnUrl)
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/"
        }, "t2");
    }

    [ValidateAntiForgeryToken]
    [Authorize]
    [HttpPost("Logout")]
    public IActionResult Logout() => SignOut(new AuthenticationProperties 
    { 
        RedirectUri = "/" 
    },
    CookieAuthenticationDefaults.AuthenticationScheme,
    OpenIdConnectDefaults.AuthenticationScheme);
}
