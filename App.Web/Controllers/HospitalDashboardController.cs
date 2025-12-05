using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "hospital")]
public class HospitalDashboardController : Controller
{
    public IActionResult Index()
    {
        var model = new HospitalDashboardVM();

        model.NewRequestsToday = 0; // placeholder

        return View(model);
    }
}
