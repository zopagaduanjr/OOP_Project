using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Class
{
    public class Tax
    {
        public string MinIncome { get; set; }
        public string MaxIncome { get; set; }
        public string Rate { get; set; }
        public string FixedExcess { get; set; }

        public Tax(string minIncome, string maxIncome, string rate, string fixedExcess)
        {
            MinIncome = minIncome;
            MaxIncome = maxIncome;
            Rate = rate;
            FixedExcess = fixedExcess;
        }
    }
}
