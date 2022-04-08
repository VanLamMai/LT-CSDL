using LabKiemTraGiuaKi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.IO
{
    class IOJSon : IIOGrade
    {
        public void Save(List<Student> students, string _filePath)
        {
            var studentsData = JsonConvert.SerializeObject(students);
            File.WriteAllText(_filePath, studentsData);
        }
    }
}
