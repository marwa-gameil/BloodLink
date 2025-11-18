using App.Application.DTOs;
using App.Application.Interfaces;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using App.Infrastructure.Utilities;
namespace App.Web.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService;
        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<IActionResult> Index()
        {
            // For demonstration, assuming BankId is 1
            int BankId = 1;
            var requests = await _requestService.GetAllRequests(BankId);
            if (!requests.Succeeded)
            {
                // Handle error scenario, e.g., log the error, show an error page, etc.
                return View("Error", requests.Response.Message);
            }
            return View(requests.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestDto requestDto, int hospitalId)
        {
            if (!ModelState.IsValid)
                return View(requestDto);

            var result = await _requestService.CreateRequest(requestDto, hospitalId);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Response.Message);
                return View(requestDto);
            }
            TempData["Success"] = "Request is created successfully!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}
