using Accounting.DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("InvoiceId")]
        public Guid? InvoiceId { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public string UnitCode { get; set; }
        public double Price { get; set; }
        public double TLPrice { get; set; }
        public string CurrencyCode { get; set; }
        public PaymentType Type { get; set; }
        public string LineExp { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int Tax { get; set; }
        public int TaxType { get; set; }
        public double Discount { get; set; }
        public int DiscountType { get; set; }

        public virtual Product Product { get; set; }
        public virtual Invoice Invoice{ get; set; }
    }
}
