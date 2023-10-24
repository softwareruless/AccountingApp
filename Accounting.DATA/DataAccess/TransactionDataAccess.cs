using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class TransactionDataAccess
    {

        public static Invoice CalculateLine(List<Transaction> Transactions, Invoice invoice)
        {
            try
            {
                if (!Transactions.Any())
                {
                    return invoice;
                }

                var SubTotal = 0.0;
                var TotalVat = 0.0;
                var TotalDiscount = 0.0;
                var Total = 0.0;
                var Tevkifat = 0.0;

                foreach (var transaction in Transactions)
                {
                    var RowPrice = transaction.Price * transaction.Amount;
                    var RowTax = 0.0;
                    var RowTotal = 0.0;
                    var RowDiscount = 0.0;

                    if (transaction.DiscountType == 0)
                    {
                        RowDiscount = transaction.Discount;//Önyüzden sadece int geliyor bunu düzelt
                        RowTotal = RowPrice - RowDiscount;
                    }
                    else
                    {
                        RowDiscount = RowPrice * transaction.Discount / 100;
                        RowTotal = RowPrice - RowDiscount;
                    }

                    if (transaction.TaxType == 0)
                    {
                        RowTax = (RowTotal / 100) * transaction.Tax;
                        RowTotal += RowTax;
                    }
                    else
                    {
                        RowTax = (RowTotal / 100) * transaction.Tax;
                        RowPrice -= RowTax;
                    }

                    SubTotal += RowPrice;
                    TotalVat += RowTax;
                    Total += RowTotal;
                    TotalDiscount += RowDiscount;

                }

                if (invoice.Type == Enums.InvoiceType.Sales)
                {
                    invoice.IsInvoiced = true;

                    switch (invoice.SalesDiscountType)
                    {
                        case Enums.SalesDiscountType.PercentSubTotal:
                            var PercentSubTotal = SubTotal * invoice.SalesDiscount.Value / 100;
                            SubTotal -= PercentSubTotal;
                            TotalDiscount += PercentSubTotal;
                            break;
                        case Enums.SalesDiscountType.PercentTotal:
                            var PercentTotal = Total * invoice.SalesDiscount.Value / 100;
                            Total -= Total * PercentTotal;
                            TotalDiscount += PercentTotal;
                            break;
                        case Enums.SalesDiscountType.TLSubTotal:
                            var TLSubTotal = invoice.SalesDiscount.Value;
                            SubTotal -= TLSubTotal;
                            TotalDiscount += TLSubTotal;
                            break;
                        case Enums.SalesDiscountType.TLTotal:
                            var TLTotal = invoice.SalesDiscount.Value;
                            Total -= TLTotal;
                            TotalDiscount += TLTotal;
                            break;
                    }
                }

                if (invoice.WithHoldingRate != 0)
                {
                    Tevkifat = TotalVat * (Convert.ToDouble(invoice.WithHoldingRate) / 10);
                }
                Total -= Tevkifat;

                invoice.Total = Math.Round(Total, 2);
                invoice.TotalVat = Math.Round(TotalVat, 2);
                invoice.TotalDiscount = Math.Round(TotalDiscount, 2);
                invoice.WithHolding = Math.Round(Tevkifat, 2);
                invoice.SubTotal = Math.Round(SubTotal, 2);

                return invoice;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Transaction> GetList(Guid InvoiceId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Transactions.Include(p => p.Product).Where(t => t.InvoiceId == InvoiceId /*&& t.UserId == UserId*/).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }


        }
    }
}
