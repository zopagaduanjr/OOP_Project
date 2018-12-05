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
        public void LoadPersonPage()
        {
            ActivateItem(new PersonViewModel());
        }
        public void LoadProductPage()
        {
            ActivateItem(new ProductViewModel());
        }
        public void LoadCalculationsPage()
        {
            ActivateItem(new CalculationsViewModel());
        }
    }
}
