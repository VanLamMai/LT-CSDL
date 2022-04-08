using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.Models
{
    public class Student
    {
        public string ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Grade { get; set; }
        public string Department { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public Student(string line)
        {
            string[] str = line.Split('\t');
            ID = str[0].Trim();
            LastName = str[1].Trim();
            FirstName = str[2].Trim();
            Grade = str[3].Trim();
            Department = str[4].Trim();
            Mobile = "";
            Sex = "Nam";
            BirthDay = DateTime.ParseExact("01/01/1901", "dd/MM/yyyy", null);
            if (str.Length > 5)
            {
                Sex = str[5].Trim();
                BirthDay = DateTime.ParseExact(str[6], "dd/MM/yyyy", null);
                Mobile = str[7].Trim();
                Address = str[8].Trim();
            }
        }

        public Student()
        {
        }

        public override string ToString()
        {
            return string.Format($"{ID}\t{LastName}\t{FirstName}\t{Grade}\t{Department}\t{Sex}\t{BirthDay.ToString("dd/MM/yyyy")}\t{Mobile}\t{Address}");
        }
    }
}
