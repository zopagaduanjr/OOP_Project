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
using TaxCalculator.Core.Class;

namespace TaxCalculator.Core.ViewModels
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
        private string _taxDeduction;
        private BindableCollection<ExcelSheet> _uploadedExcelSheetCollection = new BindableCollection<ExcelSheet>();
        private ExcelSheet _selectedExcelSheet;

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
        public string TaxDeduction
        {
            get => _taxDeduction;
            set
            {
                _taxDeduction = value;
                NotifyOfPropertyChange(() => TaxDeduction);
            }
        }
        public BindableCollection<Tax> TaxIncomeCollection
        {
            get => _taxIncomeCollection;
            set
            {
                _taxIncomeCollection = value;
                NotifyOfPropertyChange(() => TaxIncomeCollection);
                NotifyOfPropertyChange(() => CanCalculateTaxCommand);

            }
        }
        public BindableCollection<ExcelSheet> UploadedExcelSheetCollection
        {
            get => _uploadedExcelSheetCollection;
            set
            {
                _uploadedExcelSheetCollection = value;
                NotifyOfPropertyChange(() => _uploadedExcelSheetCollection);

            }
        }

        public ExcelSheet SelectedExcelSheet
        {
            get => _selectedExcelSheet;
            set
            {
                _selectedExcelSheet = value;
                NotifyOfPropertyChange(() => SelectedExcelSheet);
                NotifyOfPropertyChange(() => CanCalculateTaxCommand);

            }

        }


        //main methods//
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

            UploadedExcelSheetCollection.Clear();
            TaxIncomeCollection.Clear();
            ExcelToDataTableCommand(dlg.FileName);
            SelectedExcelSheet = UploadedExcelSheetCollection.FirstOrDefault();
            RowsToTaxClassCommand();
            NotifyOfPropertyChange(() => SelectedExcelSheet);


        }
        public void CalculateTaxCommand()
        {
            TaxIncomeCollection.Clear();
            RowsToTaxClassCommand();
            TaxRangeCheckerCommand();
            SalaryDeductionCommand();
        }
        public bool CanCalculateTaxCommand
        {
            get
            {
                if (TaxIncomeCollection.Count != 0)
                    return true;
                else return false;
            }
        }


        ///generic methods ///

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
                    if (reader.ResultsCount > 0)
                    {
                        for (int i = 0; i < reader.ResultsCount; i++)
                        {
                            var name = reader.Name;
                            var sheetcount = i + 1;
                            var datatable = result.Tables[i];
                            var currentsheet = new ExcelSheet(name, sheetcount.ToString(), datatable);
                            UploadedExcelSheetCollection.Add(currentsheet);
                            reader.NextResult();
                        }
                        NotifyOfPropertyChange(() => UploadedExcelSheetCollection);
                    }
                    NotifyOfPropertyChange(() => SelectedExcelSheet);
                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
        public void RowsToTaxClassCommand()
        {
            if (SelectedExcelSheet != null)
            {
                if (SelectedExcelSheet.DataTable.Columns.Count == 4)
                {
                    foreach (DataRow dataRow in SelectedExcelSheet.DataTable.Rows)
                    {
                        var mintaxincome = dataRow[0].ToString();
                        var maxtaxincome = dataRow[1].ToString();
                        var taxrate = dataRow[2].ToString();
                        var taxfixedexcess = dataRow[3].ToString();
                        var tax = new Tax(mintaxincome, maxtaxincome, taxrate, taxfixedexcess);
                        TaxIncomeCollection.Add(tax);
                    }
                    NotifyOfPropertyChange(() => TaxIncomeCollection);
                    NotifyOfPropertyChange(() => CanCalculateTaxCommand);

                }
                else
                {
                    return;
                }

            }

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
        public void SalaryDeductionCommand()
        {
            var income = Convert.ToDouble(UserPersonViewModel.Income);
            var rate = Convert.ToDouble(TaxType.Rate);
            var excess = income - Convert.ToDouble(TaxType.MinIncome);
            var totaldeduct = (excess * rate) + Convert.ToDouble(TaxType.FixedExcess);
            TaxDeduction = totaldeduct.ToString();
            NotifyOfPropertyChange(() => TaxDeduction);

        }

    }
}
