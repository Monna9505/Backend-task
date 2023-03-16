using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    interface ITax
    {
        string Name { get; }
        double GrossIncome { get; }
        double TakeTax(int taxRate);
        double TakeSocialContributions();
    }
}
