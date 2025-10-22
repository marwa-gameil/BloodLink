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
    public interface IRequestService
    {
        //Task<Result> GetRequestById(int id);
        Task<Result<IEnumerable<RequestDto>>>GetAllRequests(int BankId);
        Task<Result> CreateRequest(CreateRequestDto requestDto,int HospitalId);
        Task<Result> UpdateRequest(int id, UpdateRequestDto requestDto);
        Task<Result> CancelRequest(int id);  // we should not delete requests, just cancel them 
                                             // a bool in the model Canceled = false;

    }
}
