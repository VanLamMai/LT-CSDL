using LabKiemTraGiuaKi.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.Models
{
    public class Management
    {
        List<Department> _departments = new List<Department>();
        private IIOStudents _IOStudent;
        private IIOGrade _IOGrade;


        public List<Department> Departments
        {
            get
            {
                return _departments;
            }
        }

        public Management()
        {
            _IOStudent = new IOtxt();
            Reload();
        }

        public Department findDepartment(string name)
        {
            return _departments.Find(x => x.Name == name);
        }

        public List<Student> Students(string de)
        {
            return findDepartment(de).StudentsOfDepatment;
        }

        public List<Student> StudentsOfGrade(string de, string grade)
        {
            return findDepartment(de).Grades.Find(x=>x.Name==grade).Students;
        }

        public bool AddStudent(Student student)
        {
            var st = Students(student.Department).Find(x => x.ID == student.ID);
            if (st == null)
            {
                Students(student.Department).Add(student);
                StudentsOfGrade(student.Department, student.Grade).Add(student);
                _IOStudent.Save(_departments);
                return true;
            }
            return false;
        }

        public bool UpdateStudent(Student student,string department, string grade)
        {
            var i = Students(department).FindIndex(x => x.ID == student.ID);
            if (i == -1)
            {
                return false;
            }
            if (student.Department != department)
            {
                Students(department).RemoveAll(x => x.ID == student.ID);
                Students(student.Department).Add(student);
                StudentsOfGrade(department, grade).RemoveAll(x=>x.ID==student.ID);
                StudentsOfGrade(student.Department, student.Grade).Add(student);
            }else if(student.Grade != grade)
            {
                Students(department)[i] = student;
                StudentsOfGrade(department, grade).RemoveAll(x => x.ID == student.ID);
                StudentsOfGrade(department, student.Grade).Add(student);
            }
            else
            {
                Students(department)[i] = student;
            }
            _IOStudent.Save(_departments);
            return true;
        }

        public void Reload()
        {
            _departments = _IOStudent.Read();
        }

        public void RemoveStudent(Student student)
        {
            Students(student.Department).RemoveAll(x => x.ID == student.ID);
            StudentsOfGrade(student.Department, student.Grade).RemoveAll( x=>x.ID==student.ID);
        }

        public void SaveJSON(List<Student> students, string path)
        {
            IIOGrade grade = new IOJSon();
            grade.Save(students,path);
        }

        public void SaveEXCEL(List<Student> students, string path)
        {
            IIOGrade grade = new IOExcel();
            grade.Save(students, path);
        }

        public void SaveTXT()
        {
            _IOStudent.Save(_departments);
        }
    }
}
