using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapper_lec2Console
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
