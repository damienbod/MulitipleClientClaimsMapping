
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MulitipleClientClaimsMapping.Pages;

//[Authorize(AuthenticationSchemes = "t1,t2")]
public class IndexModel : PageModel
{
    [BindProperty]
    public IEnumerable<Claim> Claims { get; set; } = Enumerable.Empty<Claim>();

    public void OnGet()
    {
        Claims = User.Claims;
    }
}