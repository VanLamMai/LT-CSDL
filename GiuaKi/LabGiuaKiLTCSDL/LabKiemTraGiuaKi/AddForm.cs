using LabKiemTraGiuaKi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabKiemTraGiuaKi
{
    public partial class AddForm : Form
    {
        private Management _mg;
        private bool _isUpdate;
        private string department,grade;

        public Student Student { get; set; }



        public AddForm(Management management)
        {
            InitializeComponent();
            _mg = management;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            rbMale.Checked = true;
            foreach (var dp in _mg.Departments)
            {
                cbxDepartment.Items.Add(dp.Name);
            }
            cbxDepartment.SelectedIndex = 0;
            change();
            if (Student != null)
            {
                PushInfor();
                _isUpdate = true;
            }

        }

        private void change()
        {
            foreach (var gr in _mg.findDepartment(cbxDepartment.Text).Grades)
            {
                cbxClass.Items.Add(gr.Name);
            }
            cbxClass.SelectedIndex = 0;
        }

        public void PushInfor()
        {
            txtID.Text = Student.ID;
            txtFirstName.Text = Student.FirstName;
            txtLastName.Text = Student.LastName;
            cbxDepartment.Text = Student.Department;
            cbxClass.Text = Student.Grade;
            rbMale.Checked = true;
            if (Student.Sex == "Nữ") rbFermale.Checked = true;
            txtAddress.Text = Student.Address;
            try
            {
                dtpBirthDay.Value = Student.BirthDay.Date;
            }
            catch
            {
                dtpBirthDay.Value = DateTime.Now;
            }
            txtMobile.Text = Student.Mobile;
        }

        public void GetStudent()
        {
            Student = new Student();
        }

        public void upStudent()
        {
            // get the old Dep,Grd
            department = Student.Department;
            grade = Student.Grade;
            // update Student;
            Student.ID = txtID.Text.Trim();
            Student.FirstName = txtFirstName.Text.Trim();
            Student.LastName = txtLastName.Text.Trim();
            Student.Grade = cbxClass.Text;
            Student.Department = cbxDepartment.Text;
            Student.Sex = rbMale.Checked == true ? "Nam" : "Nữ";
            Student.Address = txtAddress.Text;
            Student.BirthDay = dtpBirthDay.Value.Date;
            Student.Mobile = txtMobile.Text;
        }

        private bool IsEmpty()
        {
            return (
                txtID.Text == "" ||
                txtFirstName.Text == "" ||
                txtLastName.Text == "" ||
                dtpBirthDay.Value.Date == DateTime.Now.Date
                );
        }

        private void ShowAddDialog()
        {
            if (IsEmpty())
            {
                MessageBox.Show("Vui lòng điền đầu đủ MSSV, Họ tên, Ngày sinh", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_mg.AddStudent(Student))
            {
                MessageBox.Show("Đã nhập thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void ShowUpdateDialog()
        {
            if (_mg.UpdateStudent(Student,department,grade))
            {
                MessageBox.Show("Đã cập nhật thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("không tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (_isUpdate == true)
            {
                upStudent();
                ShowUpdateDialog();
            }
            else
            {
                GetStudent();
                upStudent();
                ShowAddDialog();
            }
        }

        private void cbxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxClass.Items.Clear();
            change();
        }
    }
}
