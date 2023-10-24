using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int Type { get; set; }
        public double TotalAmount { get; set; }
        public double TotalVat { get; set; }
        public double TotalExpense { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalNet { get; set; }
        public double? CurrencyRate { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string CompanyName { get; set; }
        public string Authorized { get; set; }
    }
}
