
namespace MulitipleClientClaimsMapping.Pages;

[Authorize]
public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("/SignedOut");
    }
}