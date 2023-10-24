using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Accounting.DATA.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Accounting.DATA.DataAccess
{
    public class SafeDataAccess
    {
        public static Safe GetById(Guid SafeId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Safes.FirstOrDefault(s => s.Id == SafeId && s.UserId == UserId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Safe> GetList(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Safes.Where(i => i.UserId == id).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Safe model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Safes.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kasa Başarıyla Eklendi",
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
                    var data = db.Safes.Find(id);
                    db.Safes.Remove(data);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kasa Başarıyla Silindi",
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

        public static ResponseMessage Update(Safe model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Safes.Find(model.Id);
                    data.Name = model.Name;
                    data.Description = model.Description;
                    data.IsActive = model.IsActive;
                    data.UpdatedOn = DateTime.Now;
                    data.IsActive = model.IsActive;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kasa Başarıyla Güncellendi",
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

        public static CalculateBalanceResponse Calculate(Guid SafeId, Guid UserId, DateFilter dateFilter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var Payments = db.Payments.Where(p => p.SafesId == SafeId && p.UserId == UserId).ToList();
                    var FilteredPayments = Payments.Where(p =>
                    p.SafesId == SafeId &&
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

        public static List<Payment> GetListDetail(Guid SafeId, Guid UserId, DateFilter dateFilter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var Payments = db.Payments.Where(p =>
                    p.SafesId == SafeId &&
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
