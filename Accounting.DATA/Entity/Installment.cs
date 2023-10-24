using Accounting.DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Installment
    {
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        [ForeignKey("InvoiceId")]
        public Guid InvoiceId { get; set; }

        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }

        public DateTime Date { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public double TotalPrice { get; set; }
        public int InstallmentType { get; set; }
        public int InstallmentAmount { get; set; }
        public double? CurrencyRate { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool PaidAll { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual User User { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<InstallmentPayment> InstallmentPayments { get; set; }
    }
}
