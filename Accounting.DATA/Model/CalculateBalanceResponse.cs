using Accounting.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class CalculateBalanceResponse
    {
        public double In { get; set; }
        public double Out { get; set; }
        public double Total { get; set; }
        public double TotalNotFiltered { get; set; }
        

    }
}
