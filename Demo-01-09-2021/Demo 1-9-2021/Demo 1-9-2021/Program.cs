using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo_1_9_2021.IO;
using Demo_1_9_2021.Components;
using Demo_1_9_2021.RssFeed;

namespace Demo_1_9_2021
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            INewsRepository repository = new NewRepostitory();
            NewsParser parser = new NewsParser();
            RssReader reader = new RssReader(parser);
            var manager = new NewFeedManager(repository, reader);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(manager));
        }
    }
}
