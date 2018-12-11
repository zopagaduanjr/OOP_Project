using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOP_Project.Class
{
    public class ExcelSheet
    {
        public string Name { get; set; }
        public string Count { get; set; }
        public DataTable DataTable { get; set; }

        public ExcelSheet(string name, string count, DataTable dataTable)
        {
            Name = name;
            Count = count;
            DataTable = dataTable;
        }

        public ExcelSheet()
        {
            
        }
    }
}
