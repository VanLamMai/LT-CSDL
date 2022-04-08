using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.Models
{
    public class Grade
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public Grade(string name)
        {
            Name = name;
            Students = new List<Student>();
        }
    }
}
