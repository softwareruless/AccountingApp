using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class ExpenseTitle
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SubNo { get; set; }
        public string Description { get; set; }

        public virtual List<Expense> Expenses { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
