using CarRent.DAL;
using CarRent.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Repository.Repositories
{
    public class VehicleRepository : RepositoryBase<ApplicationDbContext, Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
