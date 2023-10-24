using Accounting.DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Invoice
    {
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public InvoiceType Type { get; set; }
        public SalesDiscountType? SalesDiscountType { get; set; }
        public double? SalesDiscount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Banks Bank { get; set; }
        public Guid InstallmentId { get; set; }
        public bool IsInvoiced { get; set; }
        public bool IsWayBilled { get; set; }
        public bool IsPaid { get; set; }
        public bool IsRefund { get; set; }
        public Guid? RefundInvoiceId { get; set; }
        public int WithHoldingRate { get; set; }
        public double SubTotal { get; set; }
        public double TotalVat { get; set; }
        public double TotalDiscount { get; set; }
        public double WithHolding { get; set; }
        public double Total { get; set; }
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

        public virtual List<Transaction> Transactions { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
}
