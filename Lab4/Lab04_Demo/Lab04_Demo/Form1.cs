using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Lab04_Demo
{
    public delegate int SoSanh(object sv1, object sv2);

    public partial class Form1 : Form
    {
        public string Path = "DSSV.txt";

        QuanLySinhVien DSSV;
        bool check = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            DSSV = new QuanLySinhVien();
            DSSV.DocTuFile(Path);
            LoadToLV(DSSV.DanhSach);
        }

        private void btnChon_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Chọn hình ảnh";// "Add Photos";
            dlg.Multiselect = true;
            dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
            + "*.jpg;*.jpeg;*.gif;*.bmp;"
            + "*.tif;*.tiff;*.png|"
            + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
            + "GIF files (*.gif)|*.gif|"
            + "BMP files (*.bmp)|*.bmp|"
            + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
            + "PNG files (*.png)|*.png|"
            + "All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                txtHinh.Text = fileName;
                try
                {
                    pbHinh.Load(fileName);
                }
                catch (Exception ex)
                {
                    pbHinh.ImageLocation = "";
                }
            }
        }

        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            List<string> cn = new List<string>();
            sv.MaSo = mtxtMaSo.Text;
            sv.HoTen = txtHoTen.Text;
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.DiaChi = txtDiaChi.Text;
            sv.Lop = cboLop.Text;
            sv.Hinh = txtHinh.Text;
            if (rdNu.Checked)
                gt = false;
            sv.GioiTinh = gt;
            sv.SDT = mtxtSoDT.Text;
            sv.Email = txtEmail.Text;
            return sv;
        }

        private void LoadToLV(List<SinhVien> ds)
        {
            lvDanhSach.Items.Clear();
            foreach (SinhVien sv in ds)
            {
                AddtoLV(sv);
            }
        }

        private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = lvitem.SubItems[0].Text;
            sv.HoTen = lvitem.SubItems[1].Text;
            sv.GioiTinh = false;
            if (lvitem.SubItems[2].Text == "Nam")
                sv.GioiTinh = true;
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[3].Text);
            sv.Lop = lvitem.SubItems[4].Text;
            sv.SDT = lvitem.SubItems[5].Text;
            sv.Email = lvitem.SubItems[6].Text;
            sv.DiaChi = lvitem.SubItems[7].Text;
            sv.Hinh = lvitem.SubItems[8].Text;
            return sv;
        }

        private void GetSVToConTrols(SinhVien sv)
        {
            mtxtMaSo.Text = sv.MaSo;
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh;
            cboLop.Text = sv.Lop;
            txtDiaChi.Text = sv.DiaChi;
            txtHinh.Text = sv.Hinh;
            try
            {
                pbHinh.ImageLocation = sv.Hinh;
            }
            catch
            {
                pbHinh.ImageLocation = "";
            }
            if (sv.GioiTinh)
                rdNam.Checked = true;
            else
                rdNu.Checked = true;
            mtxtSoDT.Text = sv.SDT;
            txtEmail.Text = sv.Email;

        }

        private void AddtoLV(SinhVien sv)
        {
            ListViewItem lvItem = new ListViewItem(sv.MaSo);
            lvItem.SubItems.Add(sv.HoTen);
            string gt = "Nữ";
            if (sv.GioiTinh)
                gt = "Nam";
            lvItem.SubItems.Add(gt);
            lvItem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvItem.SubItems.Add(sv.Lop);
            lvItem.SubItems.Add(sv.SDT);
            lvItem.SubItems.Add(sv.Email);
            lvItem.SubItems.Add(sv.DiaChi);
            lvItem.SubItems.Add(sv.Hinh);
            lvDanhSach.Items.Add(lvItem);
        }

        private void btnMacDinh_Click(object sender, System.EventArgs e)
        {
            txtHoTen.Text = "";
            mtxtMaSo.Text = "";
            txtDiaChi.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cboLop.Text = cboLop.Items[0].ToString();
            txtHinh.Text = "";
            pbHinh.ImageLocation = "";
            txtEmail.Text = "";
            mtxtSoDT.Text = "";
            rdNam.Checked = true;

            LoadToLV(DSSV.DanhSach);
        }

        private void remove()
        {
            int i, count;
            ListViewItem lvItem;
            count = lvDanhSach.Items.Count - 1;
            for (i = count; i >= 0; i--)
            {
                lvItem = lvDanhSach.Items[i];
                if (lvItem.Checked)
                    DSSV.Xoa(lvItem.SubItems[0].Text, SoSanhTheoMa);
            }
            LoadToLV(DSSV.DanhSach);
            check = true;
            btnMacDinh.PerformClick();
        }

        private int SoSanhTheoMa(object obj1, object obj2)
        {
            SinhVien sv = obj2 as SinhVien;
            return sv.MaSo.CompareTo(obj1);
        }

        private void xóaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            remove();
        }

        private void btnLuu_Click(object sender, System.EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            if (DSSV.Sua(sv, sv.MaSo, SoSanhTheoMa))
            {
                LoadToLV(DSSV.DanhSach);
                check = true;
            }
            else if (sv.MaSo == "" || sv.MaSo.Length < 7)
                MessageBox.Show("Vui lòng nhập MSSV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DSSV.Them(sv);
                LoadToLV(DSSV.DanhSach);
                check = true;
            }
        }
        private void lvDanhSach_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int count = lvDanhSach.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvItem = lvDanhSach.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvItem);
                GetSVToConTrols(sv);
            }
        }

        private void btnThoat_Click(object sender, System.EventArgs e)
        {
            if (check)
            {
                DialogResult messageBox = MessageBox.Show("Bạn có muốn lưu những thay đổi trước khi thoát?",
                    "Thông báo", MessageBoxButtons.YesNo);
                if (messageBox == DialogResult.Yes)
                {
                    DSSV.LuuFile(Path);
                }
            }
            Application.Exit();
            this.Close();
        }

        private void tảiLạiToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DSSV.DanhSach.Clear();
            DSSV.DocTuFile(Path);
            LoadToLV(DSSV.DanhSach);
        }
        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {


        }
    }
}
