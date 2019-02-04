using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Core.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            LoadProductPage();
        }
        ProductViewModel productViewModel = new ProductViewModel();
        CalculationsViewModel calculationsViewModel = new CalculationsViewModel();
        public void LoadPersonPage()
        {
            ActivateItem(productViewModel.UserPersonViewModel);
        }
        public void LoadProductPage()
        {
            ActivateItem(productViewModel);
        }
        public void LoadCalculationsPage()
        {
            calculationsViewModel.UserPersonViewModel = productViewModel.UserPersonViewModel;
            ActivateItem(calculationsViewModel);
        }
    }
}
