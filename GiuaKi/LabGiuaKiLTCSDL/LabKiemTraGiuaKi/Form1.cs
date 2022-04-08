using LabKiemTraGiuaKi.Enum;
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
    public partial class Form1 : Form
    {
        private Management mn;
        private SearchType searchType;
        private string _selectParent, _selectNode;
        private List<Student> _students;
        private string Holder = "Vui lòng nhập thông tin cần tìm !!!!!!!";

        public Form1()
        {
            InitializeComponent();
            mn = new Management();
            _students = new List<Student>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowFeedOnTreeView();
            SetUpSearchInputText();
            this.rbtnID.Checked = true;
        }

        private void SetUpSearchInputText()
        {
            searchTXT.Text = Holder;
            searchTXT.GotFocus += RemovePlaceHolderText;
            searchTXT.LostFocus += ShowPlaceHolderText;
        }
        private void ShowPlaceHolderText(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTXT.Text))
            {
                searchTXT.Text = Holder;
            }
        }

        private void RemovePlaceHolderText(object sender, EventArgs e)
        {
            if (searchTXT.Text == Holder)
            {
                searchTXT.Text = "";
            }
        }

        private void LoadStudentList(List<Student> students)
        {
            lvStudentList.Items.Clear();
            foreach (Student st in students)
            {
                ListViewItem i = new ListViewItem(st.ID);
                i.SubItems.Add(st.LastName);
                i.SubItems.Add(st.FirstName);
                string sex = st.Sex;
                i.SubItems.Add(sex);
                i.SubItems.Add(st.BirthDay.ToString("dd/MM/yyyy"));
                i.SubItems.Add(st.Mobile);
                i.SubItems.Add(st.Grade);
                lvStudentList.Items.Add(i);
            }
        }

        private void ShowFeedOnTreeView()
        {
            twDepartment.Nodes.Clear();
            lvStudentList.Items.Clear();
            foreach (var department in mn.Departments)
            {
                var departmentNode = twDepartment.Nodes.Add(department.Name);
                foreach (var grade in department.Grades)
                {
                    departmentNode.Nodes.Add(grade.Name);
                }
            }
            twDepartment.ExpandAll();
        }

        private void twDepartment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lvStudentList.Items.Clear();
            if (e.Node.Level == 0)
            {
                _students = mn.Students(e.Node.Text);
                _selectNode = "";
                _selectParent = e.Node.Text;
            }
            else if (e.Node.Level == 1)
            {
                _selectParent = e.Node.Parent.Text;
                _selectNode = e.Node.Text;
                _students = mn.StudentsOfGrade(_selectParent, _selectNode);
            }
            this.lbInfor.Text = e.Node.Text;
            LoadStudentList(_students);
        }

        private void rbtnID_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton radioButton = (RadioButton)sender;
                if (radioButton.Checked)
                {
                    searchTXT.Text = "";
                    searchType = SearchType.ID;
                }
            }
        }

        private void rbtnName_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton radioButton = (RadioButton)sender;
                if (radioButton.Checked)
                {
                    searchTXT.Text = "";
                    searchType = SearchType.NAME;
                }
            }
        }

        private void rbtnMobile_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton radioButton = (RadioButton)sender;
                if (radioButton.Checked)
                {
                    searchTXT.Text = "";
                    searchType = SearchType.MOBILE;
                }
            }
        }

        private bool TypeSelect(Student student, string input, SearchType type)
        {
            string fullName = student.FirstName;
            if (type == SearchType.ID)
            {
                return student.ID.Contains(input);
            }
            else if (type == SearchType.NAME)
            {
                return fullName.Contains(input);
            }
            return student.Mobile.Contains(input);
        }

        private List<Student> FilterStudent()
        {
            return _students.FindAll(s => TypeSelect(s, searchTXT.Text, searchType));
        }

        private void searchTXT_TextChanged(object sender, EventArgs e)
        {
            LoadStudentList(FilterStudent());
        }

        private void lvStudentList_DoubleClick(object sender, EventArgs e)
        {
            int count = lvStudentList.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem item = lvStudentList.SelectedItems[0];
                AddForm form = new AddForm(mn);
                if(_selectNode!="")
                {
                    form.Student = mn.StudentsOfGrade(_selectParent, _selectNode).Find(x => x.ID == item.SubItems[0].Text);
                }
                else
                {
                    form.Student = mn.Students(_selectParent).Find(x => x.ID == item.SubItems[0].Text);
                }
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _students = mn.StudentsOfGrade(form.Student.Department, form.Student.Grade);
                    LoadStudentList(_students);
                    _selectParent = form.Student.Department;
                    _selectNode = form.Student.Grade;
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = lvStudentList.SelectedItems.Count-1;
            while (count >= 0)
            {
                ListViewItem item = lvStudentList.SelectedItems[count];
                var student = mn.Students(_selectParent).Find(x => x.ID == item.SubItems[0].Text);
                mn.RemoveStudent(student);
                count--;
            }
            LoadStudentList(_students);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mn.Reload();
            if (_selectNode == "")
                _students = mn.Students(_selectParent);
            else
                _students = mn.StudentsOfGrade(_selectParent, _selectNode);
            LoadStudentList(_students);
        }

        private void saveJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter = "Json file (*.json)|*.*";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Where do you want to save the file?";
            saveFileDialog1.InitialDirectory = @"D:";
            saveFileDialog1.FileName = _selectNode;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mn.SaveJSON(mn.StudentsOfGrade(_selectParent, _selectNode), saveFileDialog1.FileName);
                MessageBox.Show("You selected the file: " + saveFileDialog1.FileName);
            }
            saveFileDialog1.Dispose();
        }

        private void saveEXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "excel file (*.xlsx)|*.*";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Where do you want to save the file?";
            saveFileDialog1.InitialDirectory = @"D:";
            saveFileDialog1.FileName = _selectNode;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mn.SaveEXCEL(mn.StudentsOfGrade(_selectParent, _selectNode), saveFileDialog1.FileName);
                MessageBox.Show("You selected the file: " + saveFileDialog1.FileName);
            }
            saveFileDialog1.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var s = MessageBox.Show("Bạn có muốn lưu hem >_< (Nhấn yes để lưu O_O )", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (s == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            if (s == DialogResult.Yes)
            {
                mn.SaveTXT();
            }
        }

        private void nameSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _students.Sort((x,y) => x.FirstName.CompareTo(y.FirstName));
            LoadStudentList(_students);
        }

        private void mSSVSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _students.Sort((x, y) => x.ID.CompareTo(y.ID));
            LoadStudentList(_students);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm(mn);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                LoadStudentList(mn.StudentsOfGrade(form.Student.Department, form.Student.Grade));
            }
        }
    }
}
