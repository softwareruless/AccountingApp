using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Enums
{
    public enum PaymentType
    {
        /// <summary>
        /// Satis
        /// </summary>
        Sales = 0,

        /// <summary>
        /// Alis
        /// </summary>
        Purchases = 1,

        /// <summary>
        /// Satın Iade
        /// </summary>
        RefundSales = 2,

        /// <summary>
        /// Alış Iade 
        /// </summary>
        RefundPurchases = 3,

        /// <summary>
        /// Gelir
        /// </summary>
        InCome = 4,

        /// <summary>
        /// Gider
        /// </summary>
        Expense = 5,

    }
}
