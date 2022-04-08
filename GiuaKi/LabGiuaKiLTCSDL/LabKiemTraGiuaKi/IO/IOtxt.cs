using LabKiemTraGiuaKi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.IO
{
    class IOtxt : IIOStudents
    {
        private const string path = "Data/data.txt";
        public List<Department> departments = new List<Department>();

        public List<Department> Read()
        {
            Department department;
            List<Department> departments = new List<Department>();
            string line = "";
            if (File.Exists(path))
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            Student student = new Student(line);
                            int i = departments.FindIndex(x => x.Name == student.Department);
                            if (i == -1)
                            {
                                department = new Department(student.Department);
                                departments.Add(department);
                                i = departments.IndexOf(department);
                            }
                            departments[i].AddStudentToGrade(student);
                        }
                    }
                }
            }
            return departments;
        }

     

        public void Save(List<Department> departments)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var write = new StreamWriter(stream))
                {
                    foreach (var dp in departments)
                    {
                        foreach (var st in dp.StudentsOfDepatment)
                        {
                            write.WriteLine(st);
                        }
                    }
                }
            }
        }
    }
}
