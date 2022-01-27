using CarRent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Service
{
    public interface IRentService
    {
        Task<List<Rent>> GetAll();
        Task<Rent> Add(Rent rent);
        Task<Rent> GetById(Guid id);
        Task<Rent> Update(Rent rent);
        Task<List<Rent>> GetByUserId(string userId);
        Task<Rent> SetDateOfReceipt(Rent rent, DateTime date);
        Task<Rent> SetDateOfReturn(Rent rent, DateTime date);
    }
}
