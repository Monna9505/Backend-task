using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Backend.Services
{
    public class ApplicationService : IApplicationService
    {
        private string[] ChooseMenuItem = { "View Movie Stars List", "Calculate Net Salary", "Exit"  };

        public ApplicationService()
        {
        }

        public void Run()
        {
            this.MovieActorsJson();

            try
            {
                var userChoice = this.DisplayMenuAndChoose();

                if (userChoice == 1)
                {
                    var movieActors = this.MovieActorsJson();

                    for (int i = 0; i < movieActors.Count; i++)
                    {
                        Console.WriteLine(movieActors[i]);
                    }
                }
                else if (userChoice == 2)
                {
                    NetSalaryService netSalaryCalculation = new NetSalaryService();

                    netSalaryCalculation.RunSalaryService();
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid number!!");
            }
        }

        private int DisplayMenuAndChoose()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("-----------");
            string[] menuItems = this.ChooseMenuItem;
            int count = 0;

            foreach (var item in menuItems)
            {
                count++;
                Console.WriteLine(count + ". " + item);
            }

            Console.WriteLine("Choose Item (number)");
            Console.WriteLine();
            int chosenMenuItem = int.Parse(Console.ReadLine());

            return chosenMenuItem;
        }

        private List<string> MovieActorsJson() {
            var textFilePath = @"C:\Users\Simona\OneDrive\Desktop\Backend Task\input.txt";
            List<string> listOfMovieActors = new List<string>();

            if (File.Exists(textFilePath)) {
                var jsonTextFile = File.ReadAllText(textFilePath);
                var moviesAndActors = JsonConvert.DeserializeObject<MovieActors[]>(jsonTextFile);

                foreach (var item in moviesAndActors)
                {
                    var name = item.Name;
                    var surname = item.Surname;
                    var sex = item.Sex;
                    var nationality = item.Nationality;

                    var now = DateTime.Now;
                    var dob = DateTime.Parse(DateTime.ParseExact(item.DateOfBirth, "dd-MMM-yyyy", CultureInfo.InvariantCulture).Date.ToString("MM/dd/yyyy"));
                    var age = now.Year - dob.Year;

                    string infoActor = $"{name} {surname}\n{sex}\n{nationality}\n{age} years old \n";

                    listOfMovieActors.Add(infoActor);
                }
            }

            return listOfMovieActors;
        }
    }
}