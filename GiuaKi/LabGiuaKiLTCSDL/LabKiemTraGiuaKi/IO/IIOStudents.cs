using LabKiemTraGiuaKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.IO
{
    interface IIOStudents
    {
        List<Department> Read();
        void Save(List<Department> departments);
    }
}
