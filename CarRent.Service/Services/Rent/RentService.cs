using CarRent.Common.CacheProvider;
using CarRent.Common.Enums;
using CarRent.DAL;
using CarRent.DAL.DataModel;
using CarRent.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Service
{
    public class RentService : ServiceBase, IRentService
    {
        private readonly IRepositoryBase<Rent> _rentRepository;
        private readonly ICacheProvider _cacheProvider;

        public RentService(ApplicationDbContext context, IRepositoryBase<Rent> rentRepository, ICacheProvider cacheProvider) : base(context)  
        {
            _rentRepository = rentRepository;
            _cacheProvider = cacheProvider;
        }


        public async Task<List<Rent>> GetAll()
        {
            return await _rentRepository.GetAll(); // With caching version: _cacheProvider.TryGetCached(CacheKeyType.AllRents.AsString(), () => _rentRepository.GetAll());
        }
        public async Task<Rent> GetById(Guid id)
        {
            return await _rentRepository.GetById(id);
        }

        public async Task<Rent> Update(Rent rent)
        {
            return await _rentRepository.Update(rent);
        }

        public async Task<Rent> Add(Rent rent)
        {
            await _rentRepository.Add(rent);
            return rent;
        }
    }
}
