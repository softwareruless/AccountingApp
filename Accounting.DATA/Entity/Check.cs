using Accounting.DATA.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Check
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CheckNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNumber { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public CheckStatus Status { get; set; }
        public CheckDocType DocType { get; set; }
        public CheckType Type { get; set; }
        public CheckKind Kind { get; set; }
        public BankFirm Bank { get; set; }
        public bool IsPaid { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
