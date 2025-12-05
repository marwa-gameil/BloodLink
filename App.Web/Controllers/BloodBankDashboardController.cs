using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "bloodbank")]
public class BloodBankDashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}