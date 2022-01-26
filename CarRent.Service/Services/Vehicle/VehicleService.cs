using CarRent.Common.CacheProvider;
using CarRent.Common.Enums;
using CarRent.DAL;
using CarRent.DAL.DataModel;
using CarRent.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRent.Service
{
    public class VehicleService : ServiceBase, IVehicleService
    {
        private readonly IRepositoryBase<Vehicle> _vehicleRepository;
        private readonly ICacheProvider _cacheProvider;

        public VehicleService(ApplicationDbContext context, IRepositoryBase<Vehicle> vehicleRepository, ICacheProvider cacheProvider) : base(context)
        {
            _vehicleRepository = vehicleRepository;
            _cacheProvider = cacheProvider;
        }


        public async Task<List<Vehicle>> GetAll()
        {
            return await _vehicleRepository.GetAll(); // With caching version: _cacheProvider.TryGetCached(CacheKeyType.AllVehicles.AsString(), () => _vehicleRepository.GetAll());
        }

        public async Task<Vehicle> GetById(Guid id)
        {
            return await _vehicleRepository.GetById(id);
        }

        public async Task<Vehicle> Update(Vehicle rent)
        {
            return await _vehicleRepository.Update(rent);
        }

        public async Task<Vehicle> Add(Vehicle vehicle)
        {
            await _vehicleRepository.Add(vehicle);

            return vehicle;
        }
    }
}
