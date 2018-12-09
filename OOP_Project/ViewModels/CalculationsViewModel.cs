using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.ViewModels
{
    public class CalculationsViewModel : Screen
    {
        private DataTable _uploadedDataTable;
        public string FileName { get; set; }

        public DataTable UploadedDataTable
        {
            get => _uploadedDataTable;
            set
            {
                _uploadedDataTable = value;
                NotifyOfPropertyChange(() => UploadedDataTable);
            } 
        }

        public void OpenExcelDialogCommand()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "EXCEL Files (*.xls)|*.xlsx";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true) FileName = dlg.FileName;
            ExcelToDataGridCommand(dlg.FileName);

        }

        public void ExcelToDataGridCommand(string fileName)
        {
            String name = "Items";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileName +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            UploadedDataTable = data;
            NotifyOfPropertyChange(() => UploadedDataTable);


        }
    }
}
