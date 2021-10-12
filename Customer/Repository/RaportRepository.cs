using CustomerAPI.Contracts;
using CustomerAPI.Entities;
using CustomerAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Repository
{
    public class RaportRepository : IRaportRepository
    {
        private readonly DapperContext _context;
        public RaportRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Raport>> GetRaport()
        {
            var query = "SELECT * FROM Raport";
            using (var connection = _context.CreateConnection())
            {
                var raports = await connection.QueryAsync<Raport>(query);
                return raports.ToList();
            }
        }
    }
}
