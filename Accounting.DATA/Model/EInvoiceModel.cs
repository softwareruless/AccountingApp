using Accounting.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class EInvoiceModel
    {
        public Guid InvoiceId { get; set; }
        public Guid UserId { get; set; }
    }
}
