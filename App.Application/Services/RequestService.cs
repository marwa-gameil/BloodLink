using App.Application.DTOs;
using App.Application.Interfaces;
using App.Application.Utilities;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IBloodBankRepository _bloodBankRepository;

        public RequestService(
           IRequestRepository requestRepository,
           IBloodBankRepository bloodBankRepository)
        {
            _requestRepository = requestRepository;
            _bloodBankRepository = bloodBankRepository;
        }

        public async Task<Result> CreateRequest(CreateRequestDto requestDto,Guid HospitalId)
        {
            var bloodBank = await _bloodBankRepository.GetByIdAsync(requestDto.BloodBankId);
            if(bloodBank == null)
            {
                return Result.Fail<RequestDto>(new Response(404, "Blood Bank not found"));
            }
            bool result = Enum.TryParse<BloodType>(requestDto.BloodType.ToString(), out var bloodType);
            if (!result)
                return Result.Fail<RequestDto>(new Response(400, "Invalid blood type value"));

            var newRequest = new BloodRequest
            {
                BloodBankId = requestDto.BloodBankId,
                Quantity = requestDto.Quantity,
                BloodType = bloodType,
                EndAt = requestDto.EndAt,
                PatientName = requestDto.PatientName,
                PatientPhoneNumber = requestDto.PatientPhoneNumber,
                NationalId = requestDto.NationalId,
                CreatedAt = DateTime.UtcNow,
                Status = BloodRequestStatus.Pending
            };
            await _requestRepository.AddAsync(newRequest);
            await _requestRepository.SaveAsync();
            return Result.Success(201);
        }

        public async Task<Result> CancelRequest(int id)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            if (request == null)
                return Result.Fail<RequestDto>(new Response(404, "Request not found"));


             _requestRepository.DeleteAsync(request);
            await _requestRepository.SaveAsync();
            return Result.Success(204);

        }

     

        /*
        public async Task<Result> GetRequestById(int id)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            if (request == null)
                return Result.Fail(new Response(404, "Request not found"));

            return Result.Success(request);
        }
        */

        /*
        public async Task<Result> UpdateRequest(int id, UpdateRequestDto requestDto)
        {
            var existing = await _requestRepository.GetByIdAsync(id);
         
            if (existing == null)
                return Result.Fail(new Response(404, "Request not found"));
            else
            {
                if (existing.Status != BloodRequestStatus.Pending)
                    return Result.Fail(new Response(400, "Cannot update a completed or canceled request"));
            }
                bool result = Enum.TryParse<BloodRequestStatus>(requestDto.Status.ToString(), out var status);
            if (!result)
                return Result.Fail(new Response(400, "Invalid status value"));

            existing.Quantity = requestDto.Quantity;
            existing.BloodType = requestDto.BloodType;
            existing.Status =status;
            existing.EndAt = requestDto.EndAt;

             _requestRepository.UpdateAsync(existing);
            await _requestRepository.SaveAsync();
            return Result.Success(new Response(200, "Request updated successfully"));
        }

       */

    }
}
