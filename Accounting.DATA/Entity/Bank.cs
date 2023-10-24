using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Bank
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string Iban { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public string StartBalance { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
