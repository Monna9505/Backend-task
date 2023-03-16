using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    class MovieActors
    {
        public string Name;
        public string Surname;
        public string Sex;
        public string Nationality;
        public string DateOfBirth;

        public MovieActors(string Name, string Surname, string Sex, string Nationality, string DateOfBirth) {
            this.Name = Name;
            this.Surname = Surname;
            this.Sex = Sex;
            this.Nationality = Nationality;
            this.DateOfBirth = DateOfBirth;
        }
    }
}