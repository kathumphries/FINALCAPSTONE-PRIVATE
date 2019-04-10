using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IVenueSqlDal
    {
        List<Venue> GetAllVenues();
    }
}
