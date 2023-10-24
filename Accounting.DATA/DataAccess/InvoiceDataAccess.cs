using Accounting.DATA.Entity;
using Accounting.DATA.Enums;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class InvoiceDataAccess
    {
        public static List<Invoice> GetList(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Invoices.Where(i => i.UserId == UserId && !i.IsInvoiced).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Invoice> GetInvoices(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Invoices
                        .Include(i => i.Customer)
                        .Where(i => i.UserId == UserId && i.IsInvoiced).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Invoice> GetCurrent(Guid UserId, Guid CustomerId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Invoices
                        .Where(i => i.UserId == UserId && i.IsInvoiced && i.CustomerId == CustomerId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Invoice GetById(Guid Id, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Invoices
                        .Include(i => i.Customer)
                        .Include(i => i.User)
                        .Include(i => i.Transactions).ThenInclude(it => it.Product)
                        .FirstOrDefault(i => i.Id == Id && i.UserId == UserId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Create(Invoice invoice, List<Transaction> Transactions)
        {
            try
            {
                var newInvoice = TransactionDataAccess.CalculateLine(Transactions, invoice);

                if (newInvoice == null)
                {
                    return new ResponseMessage()
                    {
                        Status = false,
                        Message = "Bir Hata Oluştu Tekrar Deneyiniz...",
                    };
                }

                if (invoice.Type.GetHashCode() > 3)
                {
                    switch (invoice.Type)
                    {
                        case InvoiceType.Sales:
                            invoice.Type = InvoiceType.OrderReceived;
                            break;
                        case InvoiceType.Buy:
                            invoice.Type = InvoiceType.OrderGiven;
                            break;
                        case InvoiceType.SalesWayBill:
                            invoice.Type = InvoiceType.OrderReceived;
                            break;
                        case InvoiceType.BuyWayBill:
                            invoice.Type = InvoiceType.OrderGiven;
                            break;
                        case InvoiceType.SalesRefund:
                            invoice.Type = InvoiceType.OrderReceived;
                            invoice.IsRefund = true;
                            break;
                        case InvoiceType.BuyRefund:
                            invoice.Type = InvoiceType.OrderGiven;
                            invoice.IsRefund = true;
                            break;
                        default:

                            break;
                    }

                    invoice.IsInvoiced = true;
                }

                var data = Create(newInvoice);

                if (data == null)
                {
                    return new ResponseMessage()
                    {
                        Status = false,
                        Message = "Bir Hata Oluştu Tekrar Deneyiniz...",
                    };
                }

                var TransactionList = new List<Transaction>();

                foreach (var transaction in Transactions)
                {
                    var addT = new Transaction()
                    {
                        InvoiceId = data.Id,
                        Amount = transaction.Amount,
                        CreatedOn = DateTime.Now,
                        Price = transaction.Price,
                        ProductId = transaction.ProductId,
                        Type = transaction.Type,
                        Tax = transaction.Tax,
                        TaxType = transaction.TaxType,
                        Discount = transaction.Discount,
                        DiscountType = transaction.DiscountType,
                        UserId = newInvoice.UserId
                    };

                    TransactionList.Add(addT);
                }

                using (var db = new DataContext())
                {
                    db.Transactions.AddRange(TransactionList);
                    db.SaveChanges();
                }

                return new ResponseMessage
                {
                    Status = true,
                    Message = "Başarıyla Kaydedildi",
                };
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu Tekrar Deneyiniz...",
                    Code = e.StackTrace
                };
            }
        }

        public static Invoice Create(Invoice invoice)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Invoices.Add(invoice);

                    db.SaveChanges();

                    return invoice;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Update(Invoice invoice)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Invoices.Find(invoice.Id);

                    data.Date = invoice.Date;
                    data.PaymentMethod = invoice.PaymentMethod;
                    data.WithHoldingRate = invoice.WithHoldingRate;
                    data.SubTotal = invoice.SubTotal;
                    data.TotalVat = invoice.TotalVat;
                    data.TotalDiscount = invoice.TotalDiscount;
                    data.WithHolding = invoice.WithHolding;
                    data.Total = invoice.Total;
                    data.CustomerId = invoice.CustomerId;
                    data.Description1 = invoice.Description1;
                    data.UpdatedOn = DateTime.Now;
                    data.City = invoice.City;
                    data.District = invoice.District;
                    data.Address = invoice.Address;
                    data.TaxNo = invoice.TaxNo;
                    data.TaxOffice = invoice.TaxOffice;
                    data.CompanyName = invoice.CompanyName;
                    data.Authorized = invoice.Authorized;
                    data.Type = invoice.Type;

                    db.Invoices.Update(data);
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

        public static ResponseMessage MakeInvoice(Guid invoiceId, List<Transaction> TList)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Invoices.Find(invoiceId);

                    data.IsInvoiced = true;

                    var products = db.Products.Where(p => TList.Select(t => t.ProductId).Contains(p.Id)).ToList();

                    if (data.Type == InvoiceType.OfferReceived || data.Type == InvoiceType.OrderReceived)
                    {
                        products.ForEach(p => p.Stock -= TList.Where(t => t.ProductId == p.Id).Sum(t => t.Amount));
                    }
                    if (data.Type == InvoiceType.OfferGiven || data.Type == InvoiceType.OrderGiven)
                    {
                        products.ForEach(p => p.Stock += TList.Where(t => t.ProductId == p.Id).Sum(t => t.Amount));
                    }

                    db.Products.UpdateRange(products);
                    db.Invoices.Update(data);
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

        public static ResponseMessage AddTransactions(List<Transaction> TransactionList, Guid invoiceId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Transactions.Where(t => t.InvoiceId == invoiceId).ToList();
                    if (data.Any())
                    {
                        db.Transactions.RemoveRange(data);
                    }
                    db.Transactions.AddRange(TransactionList);
                    db.SaveChanges();
                }

                return new ResponseMessage()
                {
                    Status = true,
                    Message = "Ürünler Başarıyla Eklendi"
                };
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

        public static ResponseMessage UpdateInvoice(Invoice invoice, List<Transaction> Transactions, int? InstallmentType, int? InstallmentAmount)
        {
            var newInvoice = TransactionDataAccess.CalculateLine(Transactions, invoice);

            var type = newInvoice.Type;

            if (type == InvoiceType.OrderGiven || type == InvoiceType.OrderReceived)
            {
                var MakeInvoiceResult = MakeInvoice(newInvoice.Id, Transactions);
                if (!MakeInvoiceResult.Status)
                {
                    return new ResponseMessage()
                    {
                        Status = false,
                        Message = "Bir Hata Oluştu"
                    };
                }
            }
            if (type == InvoiceType.OfferGiven)
            {
                type = InvoiceType.OrderGiven;
            }
            if (type == InvoiceType.OfferReceived)
            {
                type = InvoiceType.OrderReceived;
            }

            newInvoice.Type = type;

            var UpdateInvoiceResponse = Update(newInvoice);
            if (!UpdateInvoiceResponse.Status)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu Tekrar Deneyiniz...",
                };
            }

            var TransactionList = new List<Transaction>();

            foreach (var transaction in Transactions)
            {
                var addT = new Transaction()
                {
                    InvoiceId = newInvoice.Id,
                    Amount = transaction.Amount,
                    CreatedOn = DateTime.Now,
                    Price = transaction.Price,
                    ProductId = transaction.ProductId,
                    Type = transaction.Type,
                    Tax = transaction.Tax,
                    TaxType = transaction.TaxType,
                    Discount = transaction.Discount,
                    DiscountType = transaction.DiscountType,
                    UserId = newInvoice.UserId
                };

                TransactionList.Add(addT);
            }

            var AddTransactionsResult = AddTransactions(TransactionList, newInvoice.Id);

            if (!AddTransactionsResult.Status)
            {
                return new ResponseMessage
                {
                    Status = false,
                    Message = "Taksit Ödemeleri Oluşturulurken Bir Hata Oluştu",
                    Code = AddTransactionsResult.Code
                };
            }

            if (newInvoice.PaymentMethod == PaymentMethod.Installment)
            {
                var AddInstallmentResult = InstallmentDataAccess.MakeInstallment(newInvoice.Id, newInvoice.UserId, InstallmentType.GetValueOrDefault(), InstallmentAmount.GetValueOrDefault());

                if (!AddInstallmentResult.Status)
                {
                    return new ResponseMessage
                    {
                        Status = false,
                        Message = "Taksit Oluşturulurken Bir Hata Oluştu",
                    };
                }
            }

            return new ResponseMessage
            {
                Status = true,
                Message = "Başarıyla Kaydedildi",
            };

        }

    }
}
