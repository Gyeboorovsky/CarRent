using CarRent.DAL;
using CarRent.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Repository.Repositories
{
    public class RentRepository : RepositoryBase<ApplicationDbContext, Rent>, IRentRepository
    {
        public RentRepository(ApplicationDbContext ctx) : base(ctx) 
        {

        }
    }
}
