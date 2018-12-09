using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Models
{
    public class PersonModel
    {

        public PersonModel(string firstName, string middleInitial, string lastName, string birthdate, string address)
        {
            FirstName = firstName;
            MiddleInitial = middleInitial;
            LastName = lastName;
            Birthdate = birthdate;
            Address = address;
        }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public List<ProductModel> ProductList { get; set; }
        public List<ProductModel> CartList { get; set; }

    }
}
