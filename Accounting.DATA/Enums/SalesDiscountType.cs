using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Enums
{
    public enum SalesDiscountType
    {
        /// <summary>
        /// Yuzde AraToplam
        /// </summary>
        PercentSubTotal = 0,

        /// <summary>
        /// Yuzde Toplam
        /// </summary>
        PercentTotal = 1,

        /// <summary>
        /// TL Ara Toplam
        /// </summary>
        TLSubTotal = 2,

        /// <summary>
        /// TL toplam
        /// </summary>
        TLTotal = 3,
    }
}
