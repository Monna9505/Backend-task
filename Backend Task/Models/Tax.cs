using Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    class Tax : ITax
    {
        private string name;
        private double grossIncome;

        public Tax(string Name, double GrossIncome) 
        {
            this.name = Name;
            this.grossIncome = GrossIncome;
        }
        public string Name
        {
            get { return this.name;  }
            set { this.name = value;  }
        }

        public double GrossIncome
        {
            get { return this.grossIncome; }
            set 
            {
                this.grossIncome = value;
            }
        }

        public double TakeTax(int taxRate)
        {
            double calculatedTax = (this.GrossIncome * taxRate) / 100;
            return calculatedTax;
        }

        public double TakeSocialContributions() 
        {
            this.GrossIncome -= (this.GrossIncome * 0.15);
            return this.GrossIncome;
        }
    }
}