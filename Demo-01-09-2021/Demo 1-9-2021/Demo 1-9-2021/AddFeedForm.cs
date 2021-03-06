using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Demo_1_9_2021
{
    public partial class AddFeedForm : Form
    {
        private readonly NewFeedManager _newsManager;
        public bool HasChanges { get; set; }

        public AddFeedForm(NewFeedManager newsManager)
        {
            _newsManager = newsManager;
            InitializeComponent();
        }

        private void AddFeedForm_Load(object sender, EventArgs e)
        {
            var publishers = _newsManager.GetNewsFeed();
            foreach (var publisher in publishers)
            {
                cbbPublisher.Items.Add(publisher.Name);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            var publisherName = cbbPublisher.Text;
            var categporyName = txtCategoryName.Text;
            var rssLink = txtRssLink.Text;

            if (string.IsNullOrWhiteSpace(publisherName)||
                string.IsNullOrWhiteSpace(categporyName) ||
                string.IsNullOrWhiteSpace(rssLink))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ dữ liệu", "Lỗi");
                return;
            }
            HasChanges = true;
            var success = _newsManager.AddCategory(publisherName, categporyName, rssLink, false);
            if(success)
            {
                ClearForm();
                return;
            }
            if (MessageBox.Show("Chuyên mục này đã tồn tại, Bạn có muốn cập nhận RSS Link không?",
                "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _newsManager.AddCategory(publisherName, categporyName, rssLink, true);
            }
            ClearForm();  
        }
        private void ClearForm()
        {
            cbbPublisher.Text = "";
            txtCategoryName.Text = "";
            txtRssLink.Text = "";
        }

        private void btn_dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
