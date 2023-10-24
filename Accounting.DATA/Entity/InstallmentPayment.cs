using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class InstallmentPayment
    {
        public Guid Id { get; set; }
        
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        
        [ForeignKey("InstallmentId")]
        public Guid InstallmentId { get; set; }
        public DateTime PaymentDate{ get; set; }
        public double Price { get; set; }
        public int NumberOfRows { get; set; }
        public double? CurrencyRate { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Installment Installment { get; set; }
    }
}
