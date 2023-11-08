﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.BFF.OpenIddict.Server.Controllers;

[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet("Login")]
    public ActionResult Login(string returnUrl) => Challenge(new AuthenticationProperties
    {
        RedirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/"
    });

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
