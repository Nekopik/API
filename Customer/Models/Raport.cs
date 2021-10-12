using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class Raport
    {
        public int Id { get; set; }
        public string Month { get; set; }

        public float Profit { get; set; }
    }
}
