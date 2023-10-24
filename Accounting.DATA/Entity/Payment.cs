using Accounting.DATA.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }

        public Guid? InvoiceId { get; set; }
        public Guid? InstallmentId { get; set; }
        public Guid? CheckId { get; set; }
        public Guid? ExpenseId { get; set; }
        
        public double Amount { get; set; }
        public Guid? BankId { get; set; }
        public Guid? SafesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocNo { get; set; }
        public DateTime? Date { get; set; }
        public PaymentType Type { get; set; }

        public bool Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
