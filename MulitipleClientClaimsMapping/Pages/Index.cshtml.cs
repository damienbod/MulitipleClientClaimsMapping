
namespace MulitipleClientClaimsMapping.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public IEnumerable<Claim> Claims { get; set; } = Enumerable.Empty<Claim>();

    public void OnGet()
    {
        Claims = User.Claims;
    }
}