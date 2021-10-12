using CustomerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
