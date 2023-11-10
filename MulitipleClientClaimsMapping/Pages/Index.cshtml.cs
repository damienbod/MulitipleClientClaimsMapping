
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MulitipleClientClaimsMapping.Pages;

//[Authorize(AuthenticationSchemes = "t1,t2")]
public class IndexModel : PageModel
{
    [BindProperty]
    public IEnumerable<Claim> Claims { get; set; } = Enumerable.Empty<Claim>();

    [BindProperty]
    public string? AuthScheme { get; set; } = string.Empty;

    public void OnGet()
    {
        if(User.Identity!.IsAuthenticated)
        {
            var authProperties = HttpContext.Features.GetRequiredFeature<IAuthenticateResultFeature>();
            AuthScheme = authProperties.AuthenticateResult!.Ticket!.Properties.Items[".AuthScheme"];
        }
       
        Claims = User.Claims;
    }
}