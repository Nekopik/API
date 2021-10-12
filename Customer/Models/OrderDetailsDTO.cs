using System;
using CustomerAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Models
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

        public float OrderPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}