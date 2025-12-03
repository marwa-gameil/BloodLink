using App.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        if (User.IsInRole("admin"))
            return RedirectToAction("Index", "AdminDashboard");

        if (User.IsInRole("hospital"))
            return RedirectToAction("Index", "HospitalDashboard");

        if (User.IsInRole("bloodbank"))
            return RedirectToAction("Index", "BloodBankDashboard");

        return RedirectToAction("Index", "Home");
    }
}
