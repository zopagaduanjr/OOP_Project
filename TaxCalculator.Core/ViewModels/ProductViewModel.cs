using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator.Core.Models;

namespace TaxCalculator.Core.ViewModels
{
    public class ProductViewModel : Screen
    {
        public ProductViewModel()
        {
            Products.Add(new ProductModel("Necklace", "Gold", 5000));
            Products.Add(new ProductModel("Earrings", "Gold", 5000));
            Products.Add(new ProductModel("Ring", "Gold", 5000));
            Products.Add(new ProductModel("Bracelet", "Gold", 5000));
        }

        private BindableCollection<ProductModel> _products = new BindableCollection<ProductModel>();
        private ProductModel _selectedProduct;
        private PersonViewModel _userPersonViewModel = new PersonViewModel();

        public PersonViewModel UserPersonViewModel
        {
            get => _userPersonViewModel;
            set => _userPersonViewModel = value;
        }
        public BindableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddCartCommand);
            }

        }


        //commands
        public bool CanAddCartCommand
        {
            get
            {
                if (SelectedProduct != null)
                    return true;
                else return false;
            }
        }
        public void AddCartCommand()
        {
            UserPersonViewModel.UserCartCollection.Add(SelectedProduct);
        }

    }
}
