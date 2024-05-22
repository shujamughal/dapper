using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperLec1console
{
    internal class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public User User { get; set; } // Reference to the related User
    }
}
