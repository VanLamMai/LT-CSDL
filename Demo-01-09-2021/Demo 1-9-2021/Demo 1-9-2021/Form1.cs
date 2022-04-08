using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo_1_9_2021.IO;
using Demo_1_9_2021.Models;
using Demo_1_9_2021.Components;

namespace Demo_1_9_2021
{
    public partial class Form1 : Form
    {
        private readonly NewFeedManager _newsMangaer;
        public Form1(NewFeedManager newsManager)
        {
            _newsMangaer = newsManager;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dialog = new AddFeedForm(_newsMangaer);
            dialog.ShowDialog(this);
            if(dialog.HasChanges)
            {
                _newsMangaer.SaveChanger();
                ShowFeedOnTreeView(_newsMangaer.GetNewsFeed());
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tvwPublicsher.SelectedNode == null) return;
            if (tvwPublicsher.SelectedNode.Level ==0)
            {
                _newsMangaer.RemovePublisher(tvwPublicsher.SelectedNode.Text);
            }
            else
            {
                var publisherNode = tvwPublicsher.SelectedNode.Parent;
                _newsMangaer.RemoveCategory(publisherNode.Text, tvwPublicsher.SelectedNode.Text);
            }
            tvwPublicsher.SelectedNode.Remove();
        }

        private void tvwPublicsher_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pnlNews.Controls.Clear();
            if(e.Node.Level == 1)
            {
                var articles = _newsMangaer.GetNews(e.Node.Parent.Text, e.Node.Text);
                foreach (var article in articles)
                {
                    var item = new NewsControl();
                    item.Size = new Size(500, 100);
                    item.Dock = DockStyle.Top;
                    item.SetArticle(article);

                    pnlNews.Controls.Add(item);
                }
            }

       

        }
        private void ShowFeedOnTreeView(List<Publisher> publishers)
        {
            tvwPublicsher.Nodes.Clear();
            pnlNews.Controls.Clear();
            foreach (var publisher in publishers)
            {
                var publisherNote = tvwPublicsher.Nodes.Add(publisher.Name);
                foreach (var category in publisher.Categories)
                {
                    publisherNote.Nodes.Add(category.Name);
                }
                tvwPublicsher.ExpandAll();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowFeedOnTreeView(_newsMangaer.GetNewsFeed());

        }
    }
}
