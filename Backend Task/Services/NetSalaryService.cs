using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    class NetSalaryService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("NetSalaryService.cs");
        private double grossSalary;
        private Tax tax { get; set; }
        private Dictionary<int, int> taxRates = new Dictionary<int, int>() { { 1, 10 } }; // Dictionary for new upcoming taxes.
        private int selectedTaxRate;
        public void RunSalaryService()
        {
            try
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                Console.Write("Enter gross salary: ");
                this.grossSalary = double.Parse(Console.ReadLine());

                Console.Write("Select which tax rate should be applied to gross income: ");
                foreach (var tax in this.taxRates)
                {
                    Console.WriteLine($"{tax.Key}. {tax.Value}");
                }

                Console.Write("Pick a number: ");
                int chosenTaxRate = int.Parse(Console.ReadLine());
                if (this.taxRates.ContainsKey(chosenTaxRate))
                {
                    this.selectedTaxRate = this.taxRates[chosenTaxRate];
                }
                else 
                {
                    Console.WriteLine("No such number.");
                    return;
                }

                tax = new Tax(name, grossSalary);

                this.RemoveChosenPercentTax();

                // Adding new tax to list of taxes
                Console.Write("Add new tax (by choice): ");
                int newTax = int.Parse(Console.ReadLine());
                taxRates[taxRates.Count + 1] = newTax;
            }
            catch (Exception)
            {
                log.Error("Gross salary should be over 0 and a number");
                throw new Exception("Please enter a valid number");
            }
        }

        private void RemoveChosenPercentTax()
        {
            double taxFreeIncome = 1000;
            double moneyOver3000 = 0;

            // If gross salary is lower or equal to 1000 no taxes and social contributions are applied.
            if (this.tax.GrossIncome > 0 && this.tax.GrossIncome <= taxFreeIncome)
            {
                Console.WriteLine($"{this.tax.Name}'s net salary is {this.tax.GrossIncome}.");
                Console.WriteLine($"{this.tax.Name} doesn't have to pay taxes because his salary is uner 1000 IDR.");
            }
            else if (this.tax.GrossIncome > taxFreeIncome)
            {
                // Removing 1000 IDR from salary because 1000 and lower is tax free.
                this.tax.GrossIncome -= taxFreeIncome; // if gross income is 3400 what's left should be 2400
                this.tax.GrossIncome -= this.tax.TakeTax(this.selectedTaxRate); // Take the tax
                this.tax.GrossIncome += taxFreeIncome; // And return those 1000 back after subtracting the tax (it is easier to keep track)

                // If gross income is higher than 3000 after returning those 1000 back
                if (this.tax.GrossIncome > 3000)
                {
                    moneyOver3000 = this.tax.GrossIncome - 3000; // Get the money that is over 3000
                    this.tax.GrossIncome -= moneyOver3000; // Subtract those over 3000
                    this.tax.GrossIncome -= taxFreeIncome; // Subtract 1000 from those left
                    this.tax.TakeSocialContributions(); // Take 15% of social contributions
                    this.tax.GrossIncome += moneyOver3000; // Return that money over 3000 back
                    this.tax.GrossIncome += taxFreeIncome; // And return those 1000 bucks that we subtracted earlier
                }
                if (this.tax.GrossIncome > taxFreeIncome && this.tax.GrossIncome < 3000)
                {
                    this.tax.GrossIncome -= taxFreeIncome;
                    this.tax.TakeSocialContributions();
                    this.tax.GrossIncome += taxFreeIncome;
                }

                Console.WriteLine($"{this.tax.Name}'s net salary is: {this.tax.GrossIncome}");
            }
        }
    }
}