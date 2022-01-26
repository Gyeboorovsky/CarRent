using CarRent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Service
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAll();
        Task<Vehicle> GetById(Guid id);
        Task<Vehicle> Add(Vehicle vehicle);
        Task<Vehicle> Update(Vehicle rent);
    }
}
