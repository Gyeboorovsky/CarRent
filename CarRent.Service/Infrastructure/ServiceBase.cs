using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Service
{
    public class ServiceBase : IServiceBase
    {
        private readonly DbContext _context;
        public ServiceBase(DbContext context)
        {
            _context = context;
        }
    }
}
