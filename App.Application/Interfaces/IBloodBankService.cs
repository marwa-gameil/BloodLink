using App.Application.DTOs;
using App.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IBloodBankService
    {
        Task<Result<IEnumerable<BloodBankDto>>> GetAllBloodBanksAsync(string Governorate);
        //Task<Result<BloodBankDto>> GetBloodBankByIdAsync(int id);
        Task<Result> AddBloodBankAsync(CreateBloodBankDto CreateBloodBankDto);
        Task<Result> UpdateBloodBankAsync(int id, UpdateBloodBankDto UpdateBloodBankDto);
        Task<Result> DeleteBloodBankAsync(int id);
    }
}
