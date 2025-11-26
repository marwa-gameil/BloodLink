using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    internal class HospitalService : IHospitalService
    {

        private readonly IHospitalRepository _hospitalRepo;
        private readonly IRequestRepository _requestRepo;
        private readonly IBloodBankRepository _bloodBankRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentLoggedInUser _currentLoggedInUser;

        public HospitalService(
            IHospitalRepository hospitalRepo,
            IRequestRepository requestRepo,
            IBloodBankRepository bloodBankRepo,
            IStockRepository stockRepo,
            UserManager<User> userManager,
            ICurrentLoggedInUser currentLoggedInUser)
        {
            _hospitalRepo = hospitalRepo;
            _requestRepo = requestRepo;
            _bloodBankRepo = bloodBankRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _currentLoggedInUser = currentLoggedInUser;
        }


        public async Task<Result> AddHospitalAsync(CreateHospitalDto hospital)
        {
            try
            {
                var newUser = new User
            {
                UserName = hospital.Email,
                Name = hospital.Name,
                Address = hospital.Address,
                Governorate = hospital.Governorate,
                PhoneNumber = hospital.PhoneNumber,
                Email = hospital.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedById = Guid.Parse(_currentLoggedInUser.UserId)

            };
                var createUserResult = await _userManager.CreateAsync(newUser, hospital.Password);
                if (!createUserResult.Succeeded)
                {
                    return Result.Fail(new Response(
                        400,
                        string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                    ));
                }
                
            await _userManager.AddToRoleAsync(newUser, "hospital");

            var newHospital = new Hospital

            {
                UserId = newUser.Id,
                Latitude = hospital.Latitude,
                Longitude = hospital.Longitude,
                LicenseNumber = hospital.LicenseNumber,
            };
            await _hospitalRepo.AddAsync(newHospital);
            await _hospitalRepo.SaveAsync();


            return Result.Success(new Response(201, "Hospital created successfully"));
        }
         catch (Exception ex)
            {
                return Result.Fail(new Response(500, $"An error occurred: {ex.Message}"));
            }
}



//public async Task<IEnumerable<Hospital>> GetAllAsync()
//{
//    return await _hospitalRepo.GetAllAsync();
//}


//void IHospitalService.UpdateHospitalAsync(Hospital hospital)
//{
//    throw new NotImplementedException();
//}

//void IHospitalService.DeleteHospitalAsync(Hospital hospital)
//{
//    throw new NotImplementedException();
//}



public async Task<Result> CreateRequest(CreateRequestDto requestDto)
        {
            
            bool result = Enum.TryParse<BloodType>(requestDto.BloodType.ToString(), out var bloodType);
            if (!result)
                return Result.Fail<RequestDto>(new Response(400, "Invalid blood type value"));

            var newRequest = new BloodRequest
            {
                BloodBankId = requestDto.BLoodBankID,
                HospitalId = Guid.Parse(_currentLoggedInUser.UserId),
                Quantity = requestDto.Quantity,
                BloodType = bloodType,
                EndAt = requestDto.EndAt,
                PatientName = requestDto.PatientName,
                PatientPhoneNumber = requestDto.PatientPhoneNumber,
                NationalId = requestDto.NationalId,
                CreatedAt = DateTime.UtcNow,
                Status = BloodRequestStatus.Pending
            };
            await _requestRepo.AddAsync(newRequest);
            await _requestRepo.SaveAsync();
            return Result.Success(201);
        }
        public async Task<Result> CancelRequest(int id)
        {
            var request = await _requestRepo.GetByIdAsync(id);
            if (request == null)
                return Result.Fail<RequestDto>(new Response(404, "Request not found"));
            if (request.Status != BloodRequestStatus.Pending)
                return Result.Fail<RequestDto>(new Response(400, "Only pending requests can be cancelled"));

            _requestRepo.DeleteAsync(request);
            await _requestRepo.SaveAsync();
            return Result.Success(204);

        }


        public async Task<Result> CompleteRequestAsync(int requestId)
        {
            var currentUser = await _currentLoggedInUser.GetUser();

            var bloodRequest = await _requestRepo.GetByIdAsync(requestId);

            if (bloodRequest is null)
            {
                return Result.Fail(new Response(404, "Request not found"));
            }
            if ((bloodRequest.HospitalId != currentUser.Id))
            {
                return Result.Fail(new Response(403, "You are not authorized to Complete this request"));
            }
            if (bloodRequest.Status != BloodRequestStatus.Approved) 
            {
                return Result.Fail(new Response(400, "Only Approved requests can be Complete"));
            }

            
                var stock = await _stockRepo.GetOneAsync(bloodRequest.BloodBankId, bloodRequest.BloodType);
                if (stock.Quantity < bloodRequest.Quantity)
                {
                    return Result.Fail(new Response(400, "Insufficient stock to approve the request"));
                }
                else 
                {
                    stock.Quantity -= bloodRequest.Quantity;
                    _stockRepo.UpdateAsync(stock);
                    await _stockRepo.SaveAsync();
                }
            
            bloodRequest.Status = BloodRequestStatus.Completed;
            _requestRepo.UpdateAsync(bloodRequest);
            await _requestRepo.SaveAsync();
            return Result.Success(new Response(200, "Request status updated successfully"));
        }

        public async Task <Result<IEnumerable<RequestDto>>> GetRequestsByHospitalAsync()
        {
            var requests = _requestRepo.GetRequestsByHospitalAsync( Guid.Parse(_currentLoggedInUser.UserId));

            return Result.Success( requests.Select(r => new RequestDto
            {
                Id = r.Id,
                Quantity = r.Quantity,
                BloodType = r.BloodType.ToString(),
                PatientName = r.PatientName,
                PatientPhoneNumber = r.PatientPhoneNumber,
                NationalId = r.NationalId,
                Status = r.Status.ToString(),
                CreatedAt = r.CreatedAt
            }));
        }
    }
}
