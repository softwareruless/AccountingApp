using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class ExpenseTitleDataAccess
    {
        public static ExpenseTitle GetById(Guid expenseTitleId, Guid userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var expenseTitle = db.ExpenseTitles.FirstOrDefault(e => e.Id == expenseTitleId && e.UserId == userId);
                    return expenseTitle;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<ExpenseTitle> GetList(Guid userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var ExpenseTitleList = db.ExpenseTitles.Where(e => e.UserId == userId).ToList();
                    return ExpenseTitleList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(ExpenseTitle expenseTitle)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.ExpenseTitles.Add(expenseTitle);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Başarıyla Kaydedildi"
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

        public static ResponseMessage Update(ExpenseTitle expenseTitle)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var expenseTitleDB = db.ExpenseTitles.FirstOrDefault(e => e.Id == expenseTitle.Id && e.UserId == expenseTitle.UserId);
                    expenseTitleDB.Name = expenseTitle.Name;
                    expenseTitleDB.Code = expenseTitle.Code;
                    expenseTitleDB.SubNo = expenseTitle.SubNo;
                    expenseTitleDB.Description = expenseTitle.Description;

                    expenseTitleDB.UpdatedOn = DateTime.Now;

                    db.ExpenseTitles.Update(expenseTitleDB);
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
                    Message = "Bir Hata Oluştu",
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Delete(Guid expenseTitleId, Guid userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var expenseTitle = db.ExpenseTitles.Include(e => e.Expenses).FirstOrDefault(e => e.Id == expenseTitleId && e.UserId == userId);
                    db.ExpenseTitles.Remove(expenseTitle);
                    db.Expenses.RemoveRange(expenseTitle.Expenses);
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
    }
}
