using Accounting.DATA.Entity;
using Accounting.DATA.Enums;
using Accounting.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class PaymentDataAccess
    {
        public static ResponseMessage Add(Payment payment)
        {
            try
            {
                using (var db = new DataContext())
                {
                    payment.CreatedOn = DateTime.Now;
                    db.Payments.Add(payment);

                    db.SaveChanges();

                    if (payment.InstallmentId != null)
                    {
                        var installment = db.InstallmentPayments.FirstOrDefault(i => i.Id == payment.InstallmentId);
                        var Totalpayment = db.Payments.Where(p => p.InstallmentId == payment.InstallmentId).Sum(p => p.Amount);
                        if (installment.Price <= Totalpayment)
                        {
                            installment.IsPaid = true;

                            db.InstallmentPayments.Update(installment);
                        }
                    }

                    if (payment.CheckId != null)
                    {
                        var check = db.Checks.FirstOrDefault(i => i.Id == payment.CheckId);
                        var Totalpayment = db.Payments.Where(p => p.CheckId == payment.CheckId).Sum(p => p.Amount);
                        if (check.Amount <= Totalpayment)
                        {
                            check.IsPaid = true;

                            db.Checks.Update(check);
                        }
                    }

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Ödeme Başarıyla Tamamlandı"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu"
                };
            }
        }

        public static Payment GetById(Guid PaymentId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Payments.FirstOrDefault(p => p.Id == PaymentId && p.UserId == UserId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Payment> GetByCustomerId(Guid CustomerId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var payments = db.Payments.Where(i => i.CustomerId == CustomerId && i.UserId == UserId).ToList();
                    return payments;
                }
            }
            catch (Exception e)
            {
                return null;
            }



        }

        public static List<Payment> GetList(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Payments.Where(p => p.UserId == UserId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Update(Payment payment)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Payments.FirstOrDefault(p => p.Id == payment.Id && p.UserId == payment.UserId);

                    data.Date = payment.Date;
                    data.Description = payment.Description;
                    data.DocNo = payment.DocNo;
                    data.Amount = payment.Amount;

                    db.Payments.Update(data);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Başarıyla Güncelleştirildi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu",
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Delete(Guid PaymentId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var payment = db.Payments.FirstOrDefault(p => p.Id == PaymentId && p.UserId == UserId);

                    db.Payments.Remove(payment);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Başarıyla Silindi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu",
                    Code = e.StackTrace
                };
            }
        }

        public static double CalculateCurrentByCustomer(Guid CustomerId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var TotalMoneyOutInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.CustomerId == CustomerId && i.Type == InvoiceType.OrderGiven).Sum(i => i.Total);
                    var TotalMoneyInflowInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.CustomerId == CustomerId && i.Type == InvoiceType.OrderReceived).Sum(i => i.Total);

                    var TotalMoneyOut = db.Payments.Where(i => i.UserId == UserId && i.CustomerId == CustomerId && (i.Type == PaymentType.Purchases || i.Type == PaymentType.RefundSales)).Sum(i => i.Amount);
                    var TotalMoneyInflow = db.Payments.Where(i => i.UserId == UserId && i.CustomerId == CustomerId && (i.Type == PaymentType.Sales || i.Type == PaymentType.RefundPurchases)).Sum(i => i.Amount);

                    var Balance = (TotalMoneyInflow - TotalMoneyOut) - (TotalMoneyInflowInvoice - TotalMoneyOutInvoice);

                    return Balance;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static List<CustomersBalance> CalculateCurrentByMultipleCustomer(List<Guid> CustomerIds, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var Balances = new List<CustomersBalance>();

                    var TotalMoneyOutInvoices = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderGiven).ToList();
                    var TotalMoneyInflowInvoices = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderReceived).ToList();

                    var TotalMoneyOuts = db.Payments.Where(i => i.UserId == UserId && (i.Type == PaymentType.Purchases || i.Type == PaymentType.RefundSales)).ToList();
                    var TotalMoneyInflows = db.Payments.Where(i => i.UserId == UserId && (i.Type == PaymentType.Sales || i.Type == PaymentType.RefundPurchases)).ToList();

                    foreach (var CustomerId in CustomerIds)
                    {
                        var TotalMoneyOutInvoice = TotalMoneyOutInvoices.Where(i => i.CustomerId == CustomerId).Sum(i => i.Total);
                        var TotalMoneyInflowInvoice = TotalMoneyInflowInvoices.Where(i => i.CustomerId == CustomerId).Sum(i => i.Total);

                        var TotalMoneyOut = TotalMoneyOuts.Where(i => i.CustomerId == CustomerId).Sum(i => i.Amount);
                        var TotalMoneyInflow = TotalMoneyInflows.Where(i => i.CustomerId == CustomerId).Sum(i => i.Amount);

                        var Balance = (TotalMoneyInflow - TotalMoneyOut) - (TotalMoneyInflowInvoice - TotalMoneyOutInvoice);

                        Balances.Add(new CustomersBalance()
                        {
                            CustomerId = CustomerId,
                            Amount = Balance
                        });

                    }

                    return Balances;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        //public static List<Invoice> CalculateCurrent(Guid UserId)
        //{
        //    try
        //    {
        //        using (var db = new DataContext())
        //        {
        //            var TotalMoneyOutInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderGiven).ToList();
        //            var TotalMoneyInflowInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderReceived).ToList();

        //            var TotalMoneyOut = db.Payments.Where(i => i.UserId == UserId && (i.Type == PaymentType.Purchases || i.Type == PaymentType.RefundSales)).ToList();
        //            var TotalMoneyInflow = db.Payments.Where(i => i.UserId == UserId && (i.Type == PaymentType.Sales || i.Type == PaymentType.RefundPurchases)).ToList();

        //            var IdList = db.Customers.Where(c => c.UserId == UserId).Select(c => c.Id).ToList();


        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }

        //    return null;

        //}

        //Tarihe Göre Cari Hesaplama
        public static double CalculateCurrentByDate(Guid UserId, DateTime Date, Guid CustomerId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var TotalMoneyOutInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderGiven && i.Date <= Date && i.CustomerId == CustomerId).Sum(i => i.Total);
                    var TotalMoneyInflowInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderReceived && i.Date <= Date && i.CustomerId == CustomerId).Sum(i => i.Total);

                    var TotalMoneyOut = db.Payments.Where(i => i.UserId == UserId && (i.Type == PaymentType.Purchases || i.Type == PaymentType.RefundSales) && i.Date <= Date && i.CustomerId == CustomerId).Sum(i => i.Amount);
                    var TotalMoneyInflow = db.Payments.Where(i => i.UserId == UserId && (i.Type == PaymentType.Sales || i.Type == PaymentType.RefundPurchases) && i.Date <= Date && i.CustomerId == CustomerId).Sum(i => i.Amount);

                    var IdList = db.Customers.Where(c => c.UserId == UserId).Select(c => c.Id).ToList();

                    var Balance = (TotalMoneyInflow - TotalMoneyOut) - (TotalMoneyInflowInvoice - TotalMoneyOutInvoice);

                    return Balance;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //Her Payment'ın tarihine göre cari hesaplama
        public static List<GetCurrentByDateForCustomerModel> CalculateCurrentByDateForCustomer(Guid CustomerId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    //var TotalMoneyOutInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderGiven).ToList();
                    //var TotalMoneyInflowInvoice = db.Invoices.Where(i => i.UserId == UserId && i.IsInvoiced && i.Type == InvoiceType.OrderReceived).ToList();

                    var Payments = db.Payments.Where(i => i.UserId == UserId && i.CustomerId == CustomerId).Select(x => new GetCurrentByDateForCustomerModel()
                    {
                        Id = x.Id,
                        Amount = CalculateCurrentByDate(UserId, x.Date.Value, CustomerId)
                    }).ToList();

                    return Payments;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public class CustomersBalance
        {
            public Guid CustomerId { get; set; }
            public double Amount { get; set; }
        }
    }
}
