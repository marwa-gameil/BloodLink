using App.API.Controllers;
using App.Application.DTOs;
using App.Application.Interfaces;
using App.Application.Services;
using App.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BloodBankController : ApiBaseController
    {
        private readonly IBloodBankService _bloodBankService;
        public BloodBankController(IBloodBankService bloodBankService )
        {
            _bloodBankService = bloodBankService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodBankDto>>> GetAll(string Governorate) =>
             HandleResult(await _bloodBankService.GetAllBloodBanksAsync(Governorate));

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddBloodBank( CreateBloodBankDto createBloodBankDto) =>
            HandleResult(await _bloodBankService.AddBloodBankAsync(createBloodBankDto));


      
        [Authorize(Roles = "bloodbank")]
        [HttpPut("requests/{requestId}/approve")]
        public async Task<ActionResult> UpdateRequestStatusApproved(

            int requestId) =>
            HandleResult(await _bloodBankService.UpdateRqeuestStatusAsync(requestId,BloodRequestStatus.Approved));
            

        [Authorize(Roles = "bloodbank")]
        [HttpPut("requests/{requestId}/reject")]
        public async Task<ActionResult> UpdateRequestStatusRejected(

            int requestId) =>
            HandleResult(await _bloodBankService.UpdateRqeuestStatusAsync(requestId, BloodRequestStatus.Rejected));

        [Authorize(Roles = "bloodbank")]
        [HttpGet("requests")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetPendingRequests() =>
            HandleResult(await _bloodBankService.GetAllRequests());

        [Authorize(Roles = "bloodbank")]
        [HttpPut("stock/increment")]
        public async Task<ActionResult> StockIncreament(StockIncreamentDto stockIncreamentDto) =>
            HandleResult(await _bloodBankService.StockIncreament(stockIncreamentDto));

        [Authorize(Roles = "bloodbank")]
        [HttpGet("stock")]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStockDetails([FromQuery] string bloodType = null) =>
            HandleResult(await _bloodBankService.GetStockDetailsAsync(bloodType));
    }
}
