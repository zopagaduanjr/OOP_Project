using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            LoadProductPage();

        }

        ProductViewModel productViewModel = new ProductViewModel();
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
            ActivateItem(new CalculationsViewModel());
        }
    }
}
