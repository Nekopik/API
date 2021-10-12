using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Contracts
{
    public interface IRaportRepository
    {
        public Task<IEnumerable<Raport>> GetRaport();
    }
}
