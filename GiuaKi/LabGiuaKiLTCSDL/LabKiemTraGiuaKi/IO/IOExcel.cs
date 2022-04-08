using ClosedXML.Excel;
using LabKiemTraGiuaKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKiemTraGiuaKi.IO
{
    class IOExcel : IIOGrade
    {
        public void Save(List<Student> students, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var wsDetailedData = workbook.AddWorksheet(filePath.Split('\\')[1]); //creates the worksheet with sheetname 'data'
                wsDetailedData.Cell(1, 1).InsertTable(students); //inserts the data to cell A1 including default column name
                workbook.SaveAs(filePath); //saves the workbook
            }
        }
    }
}
