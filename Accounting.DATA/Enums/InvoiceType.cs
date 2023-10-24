using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Accounting.DATA.Enums
{
    public enum InvoiceType
    {
        /// <summary>
        /// Alınan Teklif
        /// </summary>
        OfferReceived = 0,

        /// <summary>
        /// Verilen Teklif
        /// </summary>
        OfferGiven = 1,

        /// <summary>
        /// Alınan Sipariş
        /// </summary>
        OrderReceived = 2,

        /// <summary>
        /// Verilen Sipariş
        /// </summary>
        OrderGiven = 3,

        /// <summary>
        /// Satış
        /// </summary>
        Sales = 4,

        /// <summary>
        /// Alış
        /// </summary>
        Buy = 5,

        /// <summary>
        /// Satış İrsaliyesi
        /// </summary>
        SalesWayBill = 6,

        /// <summary>
        /// Alış İrsaliyesi
        /// </summary>
        BuyWayBill = 7,

        /// <summary>
        /// Satış İadesi
        /// </summary>
        SalesRefund = 8,

        /// <summary>
        /// Alış İadesi
        /// </summary>
        BuyRefund = 9,

    }
}
