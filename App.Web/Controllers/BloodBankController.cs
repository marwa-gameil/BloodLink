using App.Application.DTOs;
using App.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class BloodBankController : Controller
    {
        private readonly BloodBankService _bloodBankService;
        public BloodBankController(BloodBankService bloodBankService)
        {
            _bloodBankService = bloodBankService;
        }
        public async Task<IActionResult> GetBloodBanks(string govern)
        {
            var list = await _bloodBankService.GetBloodBanksAsync(govern);
            return View(list);


        }
        public IActionResult AddBloodBank()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBloodBank(CreateBloodBankDto createBloodBankDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createBloodBankDto);
            }
            var success = await _bloodBankService.AddBloodBankAsync(createBloodBankDto);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to add blood bank. Please try again.");
                return View(createBloodBankDto);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Requests()
        {
            var requests = await _bloodBankService.GetAllRequestsAsync();
            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            var success = await _bloodBankService.ApproveRequestAsync(requestId);
            if (!success)
            {
                return BadRequest("Failed to approve request.");
            }
            return RedirectToAction(nameof(Requests));


        }
        [HttpPost]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            var success = await _bloodBankService.RejectRequestAsync(requestId);
            if (!success)
            {
                return BadRequest("Failed to reject request.");
            }
            return RedirectToAction(nameof(Requests));
        }
        public async Task<IActionResult> StockDetails(string bloodType)
        {
            var stockDetails = await _bloodBankService.GetStockDetailsAsync(bloodType);
            return View(stockDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockIncreament(StockIncreamentDto stockIncreamentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(stockIncreamentDto);
            }
            var success = await _bloodBankService.StockIncreamentAsync(stockIncreamentDto);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to update stock. Please try again.");
                return View(stockIncreamentDto);
            }
            return RedirectToAction("StockDetails");
        }

    }

    }
