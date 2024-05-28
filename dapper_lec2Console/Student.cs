using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapper_lec2Console
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
