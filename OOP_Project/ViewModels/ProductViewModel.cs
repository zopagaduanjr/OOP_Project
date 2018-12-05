using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Project.Models;

namespace OOP_Project.ViewModels
{
    public class ProductViewModel : Screen
    {
        private BindableCollection<ProductModel> _products = new BindableCollection<ProductModel>();
        private ProductModel _selectedProduct;

        public BindableCollection<ProductModel> Products
        {
            get => _products;
            set => _products = value;
        }
        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
            }

        }

        public ProductViewModel()
        {
            Products.Add(new ProductModel("Necklace","Gold",5000));
            Products.Add(new ProductModel("Earrings","Gold",5000));
            Products.Add(new ProductModel("Ring","Gold",5000));
            Products.Add(new ProductModel("Bracelet","Gold",5000));
        }
    }
}
