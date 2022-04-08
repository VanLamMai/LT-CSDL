using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.Models
{
    public class Department
    {
        public string Name { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Student> StudentsOfDepatment { get; set; }

        public Department(string name)
        {
            Name = name;
            Grades = new List<Grade>();
            StudentsOfDepatment = new List<Student>();
        }

        public void AddStudentToGrade(Student student)
        {
            int i = StudentsOfDepatment.FindIndex(x => x.ID == student.ID);
            if (i != -1) return;
            StudentsOfDepatment.Add(student);
            int j = Grades.FindIndex(x => x.Name == student.Grade);
            if (j == -1)
            {
                Grade grade = new Grade(student.Grade);
                grade.Students.Add(student);
                Grades.Add(grade);
            }
            else
            {
                Grades[j].Students.Add(student);
            }
        }
    }
}
