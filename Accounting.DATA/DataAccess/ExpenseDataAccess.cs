using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class ExpenseDataAccess
    {
        public static Expense GetById(Guid expenseId, Guid userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var expense = db.Expenses.FirstOrDefault(e => e.Id == expenseId && e.UserId == userId);
                    return expense;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Expense> GetList(Guid userId, Guid ExpenseTitleId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var ExpenseList = db.Expenses.Where(e => e.UserId == userId && e.ExpenseTitleId == ExpenseTitleId).ToList();
                    return ExpenseList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Expense expense)
        {
            try
            {
                using (var db = new DataContext())
                {
                    expense.CreatedOn = DateTime.Now;
                    db.Expenses.Add(expense);
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

        public static ResponseMessage Update(Expense expense)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var expenseDB = db.Expenses.FirstOrDefault(e => e.Id == expense.Id && e.UserId == expense.UserId);
                    expenseDB.Name = expense.Name;
                    expenseDB.Description = expense.Description;
                    expenseDB.Amount = expense.Amount;
                    expenseDB.SafeId = expense.SafeId;
                    expenseDB.BankId = expense.BankId;
                    expenseDB.IsPaid = expense.IsPaid;
                    expenseDB.Date = expense.Date;
                    expenseDB.DocNo = expense.DocNo;

                    db.Expenses.Update(expenseDB);
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

        public static ResponseMessage Delete(Guid expenseId, Guid userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var expense = db.Expenses.FirstOrDefault(e => e.Id == expenseId && e.UserId == userId);
                    db.Expenses.Remove(expense);
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

        public static ResponseMessage Pay(Payment payment)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Payments.Add(payment);

                    var expense = db.Expenses.FirstOrDefault(e => e.Id == payment.ExpenseId && e.UserId == payment.UserId);
                    expense.IsPaid = true;
                    db.Expenses.Update(expense);

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

        public static ExpenseBalanceResponseModel CalculateBalance(Guid ExpenseTitleId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {

                    var expenses = db.Expenses.Where(e => e.ExpenseTitleId == ExpenseTitleId && e.UserId == UserId).ToList();

                    var inExpenses = expenses.Where(e => e.Type == Enums.ExpenseType.In).Sum(e => e.Amount);
                    var outExpenses = expenses.Where(e => e.Type == Enums.ExpenseType.Out).Sum(e => e.Amount);
                    var total = inExpenses - outExpenses;

                    return new ExpenseBalanceResponseModel()
                    {
                        In = inExpenses,
                        Out = outExpenses,
                        Total = total,
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



    }
}
