using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Enums
{
    public enum PaymentMethod
    {
        /// <summary>
        /// Nakit
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Açık Hesap
        /// </summary>
        OpenAccount = 2,

        /// <summary>
        /// Kredi Kartı
        /// </summary>
        CreditCard = 3,

        /// <summary>
        /// Havale
        /// </summary>
        Transfer = 4,

        /// <summary>
        /// Taksit
        /// </summary>
        Installment = 5,



    }
}
