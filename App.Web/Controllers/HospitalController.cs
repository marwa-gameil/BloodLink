using App.Application.DTOs;
using App.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HospitalServices _hospitalServices;
        public HospitalController(HospitalServices hospitalServices)
        {
            _hospitalServices = hospitalServices;
        }
       
        public async Task<IActionResult> GetRquestsAsync()
        {
            var requests = await _hospitalServices.GetHospitalRequestsAsync();
            return View(requests);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRequestAsync(CreateRequestDto createRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createRequestDto);
            }
            var success = await _hospitalServices.CreateBloodRequestAsync(createRequestDto);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to create request. Please try again.");
                return View(createRequestDto);
            }
            return RedirectToAction(nameof(GetRquestsAsync));
        }





        [HttpPost]
        public async Task<IActionResult> CompleteRequest(int requestId)
        {
            var success = await _hospitalServices.CompleteRequestAsync(requestId);
            if (!success)
            {
                return BadRequest("Failed to complete request.");
            }
            return RedirectToAction(nameof(GetRquestsAsync));
        }



        [HttpPost]
        public async Task<IActionResult> CancelRequest(int requestId)
        {
            var success = await _hospitalServices.CancelRequestAsync(requestId);
            if (!success)
            {
                return BadRequest("Failed to cancel request.");
            }
            return RedirectToAction(nameof(GetRquestsAsync));
        }
    }
}
