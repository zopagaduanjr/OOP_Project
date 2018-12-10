using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using OOP_Project.Class;
namespace OOP_Project.ViewModels
{
    public class CalculationsViewModel : Screen
    {
        public CalculationsViewModel()
        {
        }
        private DataTable _uploadedDataTable = new DataTable();
        private string _income;
        private BindableCollection<Tax> _taxIncomeCollection = new BindableCollection<Tax>();
        private PersonViewModel _userPersonViewModel = new PersonViewModel();
        private Tax _taxType;
        ///props////
        public PersonViewModel UserPersonViewModel
        {
            get => _userPersonViewModel;
            set => _userPersonViewModel = value;
        }
        public Tax TaxType
        {
            get => _taxType;
            set
            {
                _taxType = value;
                NotifyOfPropertyChange(() => TaxType);
            }
        } 
        public string FileName { get; set; }
        public string Income
        {
            get
            {
                //string formattedMoneyValue = $"{UserPersonViewModel.Income:C}";
                string formattedvalue = "₱" + UserPersonViewModel.Income;
                return formattedvalue;
            } 
            set
            {
                _income = value;
                NotifyOfPropertyChange(() => Income);

            }
        }
        public BindableCollection<Tax> TaxIncomeCollection
        {
            get => _taxIncomeCollection;
            set => _taxIncomeCollection = value;
        }
        public DataTable UploadedDataTable
        {
            get => _uploadedDataTable;
            set
            {
                _uploadedDataTable = value;
                NotifyOfPropertyChange(() => UploadedDataTable);
            } 
        }


        //methods/
        public void OpenExcelFilePathDialogCommand()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "EXCEL Files (*.xls)|*.xlsx";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true) FileName = dlg.FileName;
            else
            {
                return;
            }

            UploadedDataTable.Clear();
            TaxIncomeCollection.Clear();
            ExcelToDataTableCommand(dlg.FileName);
            RowsToTaxClassCommand();
            TaxRangeCheckerCommand();

        }
        public void ExcelToDataTableCommand(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension metho
                    // d
                    var result = reader.AsDataSet();
                    UploadedDataTable = result.Tables[0];
                    NotifyOfPropertyChange(() => UploadedDataTable);
                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
        public void RowsToTaxClassCommand()
        {
            foreach (DataRow dataRow in UploadedDataTable.Rows)
            {
                var mintaxincome = dataRow[0].ToString();
                var maxtaxincome = dataRow[1].ToString();
                var taxrate = dataRow[2].ToString();
                var taxfixedexcess = dataRow[3].ToString();
                var tax = new Tax(mintaxincome,maxtaxincome,taxrate,taxfixedexcess);
                TaxIncomeCollection.Add(tax);
            }
            NotifyOfPropertyChange(() => TaxIncomeCollection);
        }   
        public void TaxRangeCheckerCommand()
        {
            var income = Convert.ToDouble(UserPersonViewModel.Income);
            for (int i = 1; i < TaxIncomeCollection.Count; i++)
                {
                    var minrange = Convert.ToDouble(TaxIncomeCollection[i].MinIncome);
                    var maxrange = Convert.ToDouble(TaxIncomeCollection[i].MaxIncome);

                    if (income >= minrange && income <= maxrange)
                    {
                        TaxType = TaxIncomeCollection[i];
                        NotifyOfPropertyChange(() => TaxType);
                }
            }
            NotifyOfPropertyChange(() => TaxType);


        }

    }
}
