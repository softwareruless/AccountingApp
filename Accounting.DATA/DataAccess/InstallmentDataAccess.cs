using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class InstallmentDataAccess
    {
        public static Installment Add(Installment installment)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Installments.Add(installment);
                    db.SaveChanges();

                    return installment;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Installment GetById(Guid InstallmentId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var installment = db.Installments
                        .Include(i => i.InstallmentPayments)
                        .Include(i => i.Customer)
                        .FirstOrDefault(i => i.Id == InstallmentId && i.UserId == UserId);
                    return installment;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Installment> GetList(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Installments
                        .Include(i=>i.Customer)
                        .Where(i => i.UserId == UserId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage MakeInstallment(Guid InvoiceId, Guid UserId, int InstallmentType, int InstallmentAmount)
        {
            using (var db = new DataContext())
            {
                var invoice = db.Invoices.FirstOrDefault(i => i.Id == InvoiceId /*&& i.UserId == UserId*/);

                var PaymentList = new List<InstallmentPayment>();

                var installment = new Installment()
                {
                    UserId = invoice.UserId,
                    CustomerId = invoice.CustomerId,
                    InvoiceId = invoice.Id,
                    Date = DateTime.Now,
                    LastPaymentDate = DateTime.Now.AddMonths(InstallmentType * InstallmentAmount), //Burayı Sor
                    TotalPrice = invoice.Total,
                    InstallmentType = InstallmentType,
                    InstallmentAmount = InstallmentAmount,
                    Description = invoice.Description1,
                    Status = true,
                    CreatedOn = DateTime.Now,
                };

                var AddInstallmentResult = Add(installment);

                if (AddInstallmentResult != null)
                {
                    for (int i = 1; i < InstallmentAmount+1; i++)
                    {
                        var Payment = new InstallmentPayment()
                        {
                            UserId = invoice.UserId,
                            InstallmentId = AddInstallmentResult.Id,
                            PaymentDate = DateTime.Now.AddMonths(i),
                            Price = AddInstallmentResult.TotalPrice / InstallmentAmount,
                            NumberOfRows = i,
                            Description = AddInstallmentResult.Description,
                            Status = true,
                            CreatedOn = DateTime.Now,
                        };

                        PaymentList.Add(Payment);
                    }

                    db.InstallmentPayments.AddRange(PaymentList);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Taksitlendirme Başarıyla Tamamlandı"
                    };
                }
                else
                {
                    return new ResponseMessage()
                    {
                        Status = false,
                        Message = "Bir Hata Oluştu"
                    };
                }

            }

        }
    }
}
