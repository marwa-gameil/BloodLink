using App.Application.DTOs;
using App.Domain.Models;
using App.Domain.Responses;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{


    public interface IHospitalService
    {
        //Task<Hospital> GetByIdAsync(Guid id);
        // Task<IEnumerable<Hospital>> GetAllAsync();
        Task<Result> AddHospitalAsync(CreateHospitalDto hospital);

        Task<Result> CompleteRequestAsync(int requestId);
        Task<Result> CreateRequest(CreateRequestDto requestDto);
        Task<Result> CancelRequest(int id);
       
        Task<Result<IEnumerable<RequestDto>>>GetRequestsByHospitalAsync();
        
    }

  
}
