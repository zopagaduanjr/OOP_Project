using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Project.Models;

namespace OOP_Project.ViewModels
{
    class PersonViewModel : Screen
    {
        public PersonViewModel()
        {
            PersonModel = new PersonModel("Zaldy", "Pagaduan");
        }
        private PersonModel _personModel;

        public PersonModel PersonModel
        {
            get => _personModel;
            set => _personModel = value;
        }
    }
}
