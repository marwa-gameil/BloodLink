using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "hospital")]
public class HospitalDashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
