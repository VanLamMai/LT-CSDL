using System;
using System.Collections.Generic;
using System.Text;
using Demo_1_9_2021.Models;

namespace Demo_1_9_2021.IO
{
    public interface INewsRepository
    {
        List<Publisher> GetNews();
        void Save(List<Publisher> publishers);
    }
}
