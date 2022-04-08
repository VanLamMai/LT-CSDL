
namespace LabKiemTraGiuaKi
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.twDepartment = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchTXT = new System.Windows.Forms.TextBox();
            this.rbtnMobile = new System.Windows.Forms.RadioButton();
            this.rbtnName = new System.Windows.Forms.RadioButton();
            this.rbtnID = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lvStudentList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sắpXếpTheoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbInfor = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nhậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEXCELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // twDepartment
            // 
            this.twDepartment.Location = new System.Drawing.Point(18, 131);
            this.twDepartment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.twDepartment.Name = "twDepartment";
            this.twDepartment.Size = new System.Drawing.Size(356, 855);
            this.twDepartment.TabIndex = 0;
            this.twDepartment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twDepartment_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchTXT);
            this.groupBox1.Controls.Add(this.rbtnMobile);
            this.groupBox1.Controls.Add(this.rbtnName);
            this.groupBox1.Controls.Add(this.rbtnID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lvStudentList);
            this.groupBox1.Location = new System.Drawing.Point(386, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(832, 971);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm";
            // 
            // searchTXT
            // 
            this.searchTXT.Location = new System.Drawing.Point(201, 65);
            this.searchTXT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchTXT.Name = "searchTXT";
            this.searchTXT.Size = new System.Drawing.Size(476, 26);
            this.searchTXT.TabIndex = 7;
            this.searchTXT.TextChanged += new System.EventHandler(this.searchTXT_TextChanged);
            // 
            // rbtnMobile
            // 
            this.rbtnMobile.AutoSize = true;
            this.rbtnMobile.Location = new System.Drawing.Point(552, 29);
            this.rbtnMobile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbtnMobile.Name = "rbtnMobile";
            this.rbtnMobile.Size = new System.Drawing.Size(127, 24);
            this.rbtnMobile.TabIndex = 6;
            this.rbtnMobile.TabStop = true;
            this.rbtnMobile.Text = "Số điện thoại";
            this.rbtnMobile.UseVisualStyleBackColor = true;
            this.rbtnMobile.CheckedChanged += new System.EventHandler(this.rbtnMobile_CheckedChanged);
            // 
            // rbtnName
            // 
            this.rbtnName.AutoSize = true;
            this.rbtnName.Location = new System.Drawing.Point(378, 29);
            this.rbtnName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbtnName.Name = "rbtnName";
            this.rbtnName.Size = new System.Drawing.Size(61, 24);
            this.rbtnName.TabIndex = 5;
            this.rbtnName.TabStop = true;
            this.rbtnName.Text = "Tên";
            this.rbtnName.UseVisualStyleBackColor = true;
            this.rbtnName.CheckedChanged += new System.EventHandler(this.rbtnName_CheckedChanged);
            // 
            // rbtnID
            // 
            this.rbtnID.AutoSize = true;
            this.rbtnID.Location = new System.Drawing.Point(201, 29);
            this.rbtnID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbtnID.Name = "rbtnID";
            this.rbtnID.Size = new System.Drawing.Size(80, 24);
            this.rbtnID.TabIndex = 4;
            this.rbtnID.TabStop = true;
            this.rbtnID.Text = "MSSV";
            this.rbtnID.UseVisualStyleBackColor = true;
            this.rbtnID.CheckedChanged += new System.EventHandler(this.rbtnID_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tìm theo";
            // 
            // lvStudentList
            // 
            this.lvStudentList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvStudentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvStudentList.ContextMenuStrip = this.contextMenuStrip1;
            this.lvStudentList.FullRowSelect = true;
            this.lvStudentList.GridLines = true;
            this.lvStudentList.HideSelection = false;
            this.lvStudentList.Location = new System.Drawing.Point(0, 111);
            this.lvStudentList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvStudentList.Name = "lvStudentList";
            this.lvStudentList.Size = new System.Drawing.Size(830, 856);
            this.lvStudentList.TabIndex = 2;
            this.lvStudentList.UseCompatibleStateImageBehavior = false;
            this.lvStudentList.View = System.Windows.Forms.View.Details;
            this.lvStudentList.DoubleClick += new System.EventHandler(this.lvStudentList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "MSSV";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Họ và tên lót";
            this.columnHeader2.Width = 108;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tên";
            this.columnHeader3.Width = 83;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Giới tính";
            this.columnHeader4.Width = 76;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Ngày sinh";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Số Điện thoại";
            this.columnHeader6.Width = 99;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Lớp";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.sắpXếpTheoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 132);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(188, 32);
            this.addToolStripMenuItem.Text = "Thêm ";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(188, 32);
            this.removeToolStripMenuItem.Text = "Xóa";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(188, 32);
            this.loadToolStripMenuItem.Text = "Load ";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // sắpXếpTheoToolStripMenuItem
            // 
            this.sắpXếpTheoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSSVToolStripMenuItem,
            this.tênToolStripMenuItem});
            this.sắpXếpTheoToolStripMenuItem.Name = "sắpXếpTheoToolStripMenuItem";
            this.sắpXếpTheoToolStripMenuItem.Size = new System.Drawing.Size(188, 32);
            this.sắpXếpTheoToolStripMenuItem.Text = "Sắp xếp theo";
            // 
            // mSSVToolStripMenuItem
            // 
            this.mSSVToolStripMenuItem.Name = "mSSVToolStripMenuItem";
            this.mSSVToolStripMenuItem.Size = new System.Drawing.Size(161, 34);
            this.mSSVToolStripMenuItem.Text = "MSSV";
            this.mSSVToolStripMenuItem.Click += new System.EventHandler(this.mSSVSortToolStripMenuItem_Click);
            // 
            // tênToolStripMenuItem
            // 
            this.tênToolStripMenuItem.Name = "tênToolStripMenuItem";
            this.tênToolStripMenuItem.Size = new System.Drawing.Size(161, 34);
            this.tênToolStripMenuItem.Text = "Tên";
            this.tênToolStripMenuItem.Click += new System.EventHandler(this.nameSortToolStripMenuItem_Click);
            // 
            // lbInfor
            // 
            this.lbInfor.AutoSize = true;
            this.lbInfor.Location = new System.Drawing.Point(18, 94);
            this.lbInfor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfor.Name = "lbInfor";
            this.lbInfor.Size = new System.Drawing.Size(292, 20);
            this.lbInfor.TabIndex = 8;
            this.lbInfor.Text = "Chọn lớp để hiển thị danh sách sinh viên";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nhậpToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.inToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1236, 35);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nhậpToolStripMenuItem
            // 
            this.nhậpToolStripMenuItem.Name = "nhậpToolStripMenuItem";
            this.nhậpToolStripMenuItem.Size = new System.Drawing.Size(71, 29);
            this.nhậpToolStripMenuItem.Text = "Nhập";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveJSONToolStripMenuItem,
            this.saveEXCELToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(57, 29);
            this.saveToolStripMenuItem.Text = "Lưu";
            // 
            // saveJSONToolStripMenuItem
            // 
            this.saveJSONToolStripMenuItem.Name = "saveJSONToolStripMenuItem";
            this.saveJSONToolStripMenuItem.Size = new System.Drawing.Size(196, 34);
            this.saveJSONToolStripMenuItem.Text = "Lưu JSON";
            this.saveJSONToolStripMenuItem.Click += new System.EventHandler(this.saveJSONToolStripMenuItem_Click);
            // 
            // saveEXCELToolStripMenuItem
            // 
            this.saveEXCELToolStripMenuItem.Name = "saveEXCELToolStripMenuItem";
            this.saveEXCELToolStripMenuItem.Size = new System.Drawing.Size(196, 34);
            this.saveEXCELToolStripMenuItem.Text = "Lưu EXCEL";
            this.saveEXCELToolStripMenuItem.Click += new System.EventHandler(this.saveEXCELToolStripMenuItem_Click);
            // 
            // inToolStripMenuItem
            // 
            this.inToolStripMenuItem.Name = "inToolStripMenuItem";
            this.inToolStripMenuItem.Size = new System.Drawing.Size(43, 29);
            this.inToolStripMenuItem.Text = "In";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 1008);
            this.Controls.Add(this.lbInfor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.twDepartment);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView twDepartment;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox searchTXT;
        private System.Windows.Forms.RadioButton rbtnMobile;
        private System.Windows.Forms.RadioButton rbtnName;
        private System.Windows.Forms.RadioButton rbtnID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvStudentList;
        private System.Windows.Forms.Label lbInfor;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nhậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolStripMenuItem saveJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEXCELToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sắpXếpTheoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mSSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tênToolStripMenuItem;
    }
}

