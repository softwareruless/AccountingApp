using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class InvoiceTotalDto
    {
        public double SubTotal { get; set; }
        public double TotalVat { get; set; }
        public double TotalDiscount { get; set; }
        public double WithHolding { get; set; }
        public double Total { get; set; }
    }
}
