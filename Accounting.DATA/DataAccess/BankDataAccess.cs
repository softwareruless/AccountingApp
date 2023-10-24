using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class BankDataAccess
    {
        public static Bank GetById(Guid BankId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Banks.FirstOrDefault(b => b.Id == BankId && b.UserId == UserId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Bank> GetList(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Banks.Where(i => i.UserId == id).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Bank model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Banks.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Banka Başarıyla Eklendi",
                        Status = true
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu Tekrar Deneyiniz",
                    Status = false,
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Delete(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Banks.Where(i => i.Id == id).FirstOrDefault();
                    db.Banks.Remove(data);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Banka Başarıyla Eklendi",
                        Status = true
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu Tekrar Deneyiniz",
                    Status = false,
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Update(Bank model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Banks.Find(model.Id);
                    data.Name = model.Name;
                    data.BankName = model.BankName;
                    data.BankBranch = model.BankBranch;
                    data.AccountNo = model.AccountNo;
                    data.Iban = model.Iban;
                    data.UpdatedOn = model.UpdatedOn;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Banka Başarıyla Güncellendi",
                        Status = true
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu Tekrar Deneyiniz",
                    Status = false,
                    Code = e.StackTrace
                };
            }
        }

        public static CalculateBalanceResponse Calculate(Guid BankId, Guid UserId, DateFilter dateFilter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var Payments = db.Payments.Where(p => p.BankId == BankId && p.UserId == UserId).ToList();
                    var FilteredPayments = Payments.Where(p =>
                    p.BankId == BankId &&
                    p.UserId == UserId &&
                    p.Date >= dateFilter.StartDate &&
                    p.Date <= dateFilter.EndDate).ToList();

                    var In = Payments.Where(p => p.Type == Enums.PaymentType.Sales || p.Type == Enums.PaymentType.RefundPurchases).Sum(p => p.Amount);
                    var Out = Payments.Where(p => p.Type == Enums.PaymentType.Purchases || p.Type == Enums.PaymentType.RefundSales).Sum(p => p.Amount);
                    var Total = In - Out;

                    var InFiltered = FilteredPayments.Where(p => p.Type == Enums.PaymentType.Sales || p.Type == Enums.PaymentType.RefundPurchases).Sum(p => p.Amount);
                    var Outfiltered = FilteredPayments.Where(p => p.Type == Enums.PaymentType.Purchases || p.Type == Enums.PaymentType.RefundSales).Sum(p => p.Amount);
                    var TotalFiltered = InFiltered - Outfiltered;

                    return new CalculateBalanceResponse()
                    {
                        In = InFiltered,
                        Out = Outfiltered,
                        Total = TotalFiltered,
                        TotalNotFiltered = Total,
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }


        }

        public static List<Payment> GetListDetail(Guid BankId, Guid UserId, DateFilter dateFilter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var Payments = db.Payments.Where(p =>
                    p.BankId == BankId &&
                    p.UserId == UserId &&
                    p.Date >= dateFilter.StartDate &&
                    p.Date <= dateFilter.EndDate).ToList();

                    return Payments;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
