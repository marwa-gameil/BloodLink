using App.Application.DTOs;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class HospitalController : ApiBaseController
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }
        [Authorize(Roles = "hospital")]
        [HttpPost("requests")]
        public async Task<ActionResult> CreateBloodRequest(CreateRequestDto createRequestDto) =>
            HandleResult(await _hospitalService.CreateRequest(createRequestDto));
        [Authorize(Roles = "hospital")]
        [HttpGet("requests")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetHospitalRequests() =>
            HandleResult(await _hospitalService.GetRequestsByHospitalAsync());
        [Authorize(Roles = "hospital")]
        [HttpPut("requests/{requestId}/complete")]
        public async Task<ActionResult> CompleteRequest(int requestId) =>
            HandleResult(await _hospitalService.CompleteRequestAsync(requestId));
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddHospital(CreateHospitalDto createHospitalDto) =>
            HandleResult(await _hospitalService.AddHospitalAsync(createHospitalDto));

        [Authorize (Roles = "hospital")]
        [HttpDelete("requests/{requestId}/cancle")]
        public async Task<ActionResult> CancelRequest( int requestId) =>
            HandleResult(await _hospitalService.CancelRequest(requestId));
    }
} 
