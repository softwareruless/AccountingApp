using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class CheckDataAccess
    {
        public static List<Check> GetList(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Checks.Where(c => c.UserId == UserId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Check GetById(Guid CheckId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Checks.FirstOrDefault(c => c.UserId == UserId && c.Id == CheckId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Check check)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Checks.Add(check);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Çek Başarıyla Eklendi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu...",
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Update(Check check)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var checkDB = db.Checks.FirstOrDefault(c => c.Id == check.Id && c.UserId == check.UserId);
                    checkDB.Name = check.Name;
                    checkDB.Amount = check.Amount;
                    checkDB.Description = check.Description;
                    checkDB.CheckNo = check.CheckNo;
                    checkDB.BankName = check.BankName;
                    checkDB.BankBranch = check.BankBranch;
                    checkDB.AccountNumber = check.AccountNumber;
                    checkDB.Title = check.Title;
                    checkDB.Date = check.Date;
                    checkDB.ExpiryDate = check.ExpiryDate;
                    checkDB.Status = check.Status;
                    checkDB.DocType = check.DocType;
                    checkDB.Type = check.Type;
                    checkDB.Kind = check.Kind;

                    checkDB.UpdatedOn= DateTime.Now;

                    db.Checks.Update(checkDB);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Başarıyla Güncellendi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu..."
                };
            }
        }

        public static ResponseMessage Delete(Guid CheckId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var check = db.Checks.FirstOrDefault(c => c.Id == CheckId && c.UserId == UserId);
                    db.Checks.Remove(check);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Çek Başarıyla Silindi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu...",
                    Code = e.StackTrace
                };
            }
        }
    
        public static ResponseMessage MakeEndorsed(Check check)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var checkDB = db.Checks.FirstOrDefault(c => c.Id == check.Id && c.UserId == check.UserId);
                    
                    checkDB.Date = check.Date;
                    checkDB.Status = Enums.CheckStatus.Endorsed;
                    checkDB.CustomerId = check.CustomerId;
                    checkDB.UpdatedOn = DateTime.Now;

                    db.Checks.Update(checkDB);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Çek Başarıyla Cirolandı"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu..."
                };
            }
        }

        public static ResponseMessage SendBank(Check check)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var checkDB = db.Checks.FirstOrDefault(c => c.Id == check.Id && c.UserId == check.UserId);

                    checkDB.Date = check.Date;
                    checkDB.Status = Enums.CheckStatus.InBank;
                    checkDB.Bank = check.Bank;
                    checkDB.UpdatedOn = DateTime.Now;

                    db.Checks.Update(checkDB);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Çek Başarıyla Cirolandı"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu..."
                };
            }
        }
    }
}
