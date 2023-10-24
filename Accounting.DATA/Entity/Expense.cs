using Accounting.DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Expense
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("ExpenseTitleId")]
        public Guid ExpenseTitleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public ExpenseType Type { get; set; }

        public Guid? SafeId { get; set; }
        public Guid? BankId { get; set; }

        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
        public string DocNo { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
