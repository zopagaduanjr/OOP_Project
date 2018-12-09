using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Project.Models;

namespace OOP_Project.ViewModels
{
    public class PersonViewModel : Screen
    {
        public PersonViewModel()
        {
            PersonModel = new PersonModel("Zaldy", "O", "Pagaduan", "08/15/1997", "Davao City");
        }
        private PersonModel _personModel;
        private BindableCollection<ProductModel> _userCartCollection = new BindableCollection<ProductModel>();
        private char _firstLetter;
        private string _fullName;
        private string _address;
        private string _birthday;

        public PersonModel PersonModel
        {
            get => _personModel;
            set => _personModel = value;
        }

        public BindableCollection<ProductModel> UserCartCollection
        {
            get => _userCartCollection;
            set => _userCartCollection = value;
        }

        public char FirstLetter
        {
            get => PersonModel.FirstName[0];
            set => _firstLetter = value;
        }

        public string Address
        {
            get => PersonModel.Address;
            set => _address = value;
        }

        public string Birthday
        {
            get => PersonModel.Birthdate;
            set => _birthday = value;
        }

        public string FullName
        {
            get
            {
                var fullname = PersonModel.FirstName +" "+ PersonModel.MiddleInitial + ". " + PersonModel.LastName;
                return fullname;
            }
            set => _fullName = value;
        }

    }
}
