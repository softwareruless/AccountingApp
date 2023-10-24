using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Safe
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string StartBalance { get; set; }
    }
}
