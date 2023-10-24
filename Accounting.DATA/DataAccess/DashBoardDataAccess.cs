using Accounting.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounting.DATA.DataAccess
{
    public class DashBoardDataAccess
    {
        public static DashboardDailyReports DailyReports(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var DailyInvoiceList = db.Invoices.Where(i =>
                    i.UserId == UserId &&
                    i.IsInvoiced &&
                    i.Date > DateTime.Today).ToList();

                    var DailySales = DailyInvoiceList.Where(i => i.Type == Enums.InvoiceType.OrderReceived);
                    var CashSales = DailySales.Where(d => d.PaymentMethod == Enums.PaymentMethod.Cash).Sum(d => d.Total);
                    var CreditCardSales = DailySales.Where(d => d.PaymentMethod == Enums.PaymentMethod.CreditCard || d.PaymentMethod == Enums.PaymentMethod.Transfer).Sum(d => d.Total);
                    var OpenAccountSales = DailySales.Where(d => d.PaymentMethod == Enums.PaymentMethod.OpenAccount).Sum(d => d.Total);

                    var DailyPurchases = DailyInvoiceList.Where(i => i.Type == Enums.InvoiceType.OrderGiven);
                    var CashPurchases = DailyPurchases.Where(d => d.PaymentMethod == Enums.PaymentMethod.Cash).Sum(d => d.Total);
                    var CreditCardPurchases = DailyPurchases.Where(d => d.PaymentMethod == Enums.PaymentMethod.CreditCard || d.PaymentMethod == Enums.PaymentMethod.Transfer).Sum(d => d.Total);
                    var OpenAccountPurchases = DailyPurchases.Where(d => d.PaymentMethod == Enums.PaymentMethod.OpenAccount).Sum(d => d.Total);

                    var DailyPaymentList = db.Payments.Where(i =>
                    i.UserId == UserId &&
                    i.Date > DateTime.Today).ToList();

                    var DailyCollections = DailyPaymentList.Where(d =>
                    d.Type == Enums.PaymentType.Sales || d.Type == Enums.PaymentType.RefundPurchases || d.Type == Enums.PaymentType.InCome);
                    var CashCollections = DailyCollections.Where(d => d.SafesId != null).Sum(d => d.Amount);
                    var CreditCardCollections = DailyCollections.Where(d => d.BankId != null).Sum(d => d.Amount);
                    var CheckCollections = DailyCollections.Where(d => d.CheckId != null).Sum(d => d.Amount);

                    var DailyPayments = DailyPaymentList.Where(d =>
                    d.Type == Enums.PaymentType.Purchases || d.Type == Enums.PaymentType.RefundSales || d.Type == Enums.PaymentType.Expense);

                    var CashPayments = DailyPayments.Where(d => d.SafesId != null).Sum(d => d.Amount);
                    var CreditCardPayments = DailyPayments.Where(d => d.BankId != null).Sum(d => d.Amount);
                    var CheckPayments = DailyPayments.Where(d => d.CheckId != null).Sum(d => d.Amount);

                    return new DashboardDailyReports()
                    {
                        DailySales = new DailySalesAndPurchasesReports()
                        {
                            Cash = CashSales,
                            CreditCard = CreditCardSales,
                            OpenAccount = OpenAccountSales
                        },
                        DailyPurchases = new DailySalesAndPurchasesReports()
                        {
                            Cash = CashPurchases,
                            CreditCard = CreditCardPurchases,
                            OpenAccount = OpenAccountPurchases
                        },
                        DailyCollection = new DailyCollectionAndPaymentReports()
                        {
                            Cash = CashCollections,
                            CreditCard = CreditCardCollections,
                            Check = CheckCollections,
                        },
                        DailyPayment = new DailyCollectionAndPaymentReports()
                        {
                            Cash = CashPayments,
                            CreditCard = CreditCardPayments,
                            Check = CheckPayments,
                        }
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static DashboardSevenDaysProcessReports SevenDaysReports(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var SalesInvoiceList = db.Invoices.Where(i =>
                    i.UserId == UserId &&
                    i.IsInvoiced &&
                    i.Date > DateTime.Today.AddDays(-7) && i.Type == Enums.InvoiceType.OrderReceived).ToList();

                    var ExpirynvoiceList = db.Invoices.Where(i =>
                    i.UserId == UserId &&
                    i.IsInvoiced &&
                    i.ExpiryDate > DateTime.Today && i.ExpiryDate < DateTime.Today.AddDays(6)).ToList();

                    var dailySales = new List<DailySale>();

                    for (int i = -6; i < 1; i++)
                    {
                        var Date = DateTime.Today.AddDays(i);
                        var DailySale = SalesInvoiceList.Where(i => i.Date.Day == Date.Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = Date.ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                        if (DailySale == null)
                        {
                            DailySale = new DailySale()
                            {
                                Date = Date.ToString("dd/MM/yyyy"),
                                Amount = 0
                            };
                        }

                        dailySales.Add(DailySale);
                    }

                    var dailyCollections = new List<DailyCollectionAndPayment>();
                    var dailyPayments = new List<DailyCollectionAndPayment>();

                    for (int i = 0; i < 7; i++)
                    {
                        var Date = DateTime.Today.AddDays(i);
                        var DailyCollection = ExpirynvoiceList.Where(i => i.ExpiryDate.Day == Date.Day && i.Type == Enums.InvoiceType.OrderReceived).GroupBy(i => i.ExpiryDate.Day).Select(d => new DailyCollectionAndPayment() { Date = Date.ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                        if (DailyCollection == null)
                        {
                            DailyCollection = new DailyCollectionAndPayment()
                            {
                                Date = Date.ToString("dd/MM/yyyy"),
                                Amount = 0
                            };
                        }

                        dailyCollections.Add(DailyCollection);

                        var DailyPayment = ExpirynvoiceList.Where(i => i.ExpiryDate.Day == Date.Day && i.Type == Enums.InvoiceType.OrderGiven).GroupBy(i => i.ExpiryDate.Day).Select(d => new DailyCollectionAndPayment() { Date = Date.ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                        if (DailyPayment == null)
                        {
                            DailyPayment = new DailyCollectionAndPayment()
                            {
                                Date = Date.ToString("dd/MM/yyyy"),
                                Amount = 0
                            };
                        }

                        dailyPayments.Add(DailyPayment);
                    }

                    //if (DayOne == null)
                    //{
                    //    DayOne = new DailySale()
                    //    {
                    //        Date = DateList[6].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //var DayTwo = DailyInvoiceList.Where(i => i.Date.Day == DateList[5].Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = DateList[5].ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                    //if (DayTwo == null)
                    //{
                    //    DayTwo = new DailySale()
                    //    {
                    //        Date = DateList[5].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //var DayThree = DailyInvoiceList.Where(i => i.Date.Day == DateList[4].Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = DateList[4].ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                    //if (DayThree == null)
                    //{
                    //    DayThree = new DailySale()
                    //    {
                    //        Date = DateList[4].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //var DayFour = DailyInvoiceList.Where(i => i.Date.Day == DateList[3].Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = DateList[3].ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                    //if (DayFour == null)
                    //{
                    //    DayFour = new DailySale()
                    //    {
                    //        Date = DateList[3].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //var DayFive = DailyInvoiceList.Where(i => i.Date.Day == DateList[2].Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = DateList[2].ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                    //if (DayFive == null)
                    //{
                    //    DayFive = new DailySale()
                    //    {
                    //        Date = DateList[2].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //var DaySix = DailyInvoiceList.Where(i => i.Date.Day == DateList[1].Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = DateList[1].ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                    //if (DaySix == null)
                    //{
                    //    DaySix = new DailySale()
                    //    {
                    //        Date = DateList[1].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //var DaySeven = DailyInvoiceList.Where(i => i.Date.Day == DateList[0].Day).GroupBy(i => i.Date.Day).Select(d => new DailySale() { Date = DateList[0].ToString("dd/MM/yyyy"), Amount = d.Sum(a => a.Total) }).FirstOrDefault();
                    //if (DaySeven == null)
                    //{
                    //    DaySeven = new DailySale()
                    //    {
                    //        Date = DateList[0].ToString("dd/MM/yyyy"),
                    //        Amount = 0
                    //    };
                    //}

                    //{
                    //    DayOne,
                    //    DayTwo,
                    //    DayThree,
                    //    DayFour,
                    //    DayFive,
                    //    DaySix,
                    //    DaySeven
                    //};

                    return new DashboardSevenDaysProcessReports()
                    {
                        DailySales = new SevenDaysSales()
                        {
                            dailySales = dailySales
                        },
                        DailyExpiryProcess = new SevenDaysExpiryProcess()
                        {
                            DailyCollections = dailyCollections,
                            DailyPayments = dailyPayments
                        }
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static SevenDaysProcess SevenDailyBankProcess(Guid? BankId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var DailyPaymentList = db.Payments.Where(i =>
                        i.UserId == UserId && i.Date > DateTime.Today.AddDays(-7) && i.BankId != null).ToList();



                    if (BankId == null)
                    {
                        var DailyCollections = new List<SevenDaysProcessForDay>();
                        var DailyPayments = new List<SevenDaysProcessForDay>();
                        for (int i = -6; i < 1; i++)
                        {
                            var Date = DateTime.Today.AddDays(i);

                            var DailyCollection = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Sales || d.Type == Enums.PaymentType.RefundPurchases || d.Type == Enums.PaymentType.InCome) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = Date.ToString("dd/MM/yyyy"), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyCollection == null)
                            {
                                DailyCollection = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            var DailyPayment = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Purchases || d.Type == Enums.PaymentType.RefundSales || d.Type == Enums.PaymentType.Expense) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = Date.ToString("dd/MM/yyyy"), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyPayment == null)
                            {
                                DailyPayment = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            DailyCollections.Add(DailyCollection);
                            DailyPayments.Add(DailyPayment);
                        }


                        return new SevenDaysProcess()
                        {
                            In = DailyCollections,
                            Out = DailyPayments
                        };
                    }
                    else
                    {
                        DailyPaymentList = DailyPaymentList.Where(d => d.BankId == BankId).ToList();

                        var DailyCollections = new List<SevenDaysProcessForDay>();
                        var DailyPayments = new List<SevenDaysProcessForDay>();
                        for (int i = -6; i < 1; i++)
                        {
                            var Date = DateTime.Today.AddDays(i);

                            var DailyCollection = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Sales || d.Type == Enums.PaymentType.RefundPurchases || d.Type == Enums.PaymentType.InCome) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = Date.ToString("dd/MM/yyyy"), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyCollection == null)
                            {
                                DailyCollection = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            var DailyPayment = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Purchases || d.Type == Enums.PaymentType.RefundSales || d.Type == Enums.PaymentType.Expense) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = Date.ToString("dd/MM/yyyy"), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyPayment == null)
                            {
                                DailyPayment = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            DailyCollections.Add(DailyCollection);
                            DailyPayments.Add(DailyPayment);
                        }


                        return new SevenDaysProcess()
                        {
                            In = DailyCollections,
                            Out = DailyPayments
                        };
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }


        }

        public static SevenDaysProcess SevenDailySafeProcess(Guid? SafeId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var DailyPaymentList = db.Payments.Where(i =>
                        i.UserId == UserId && i.Date > DateTime.Today.AddDays(-7) && i.SafesId != null).ToList();



                    if (SafeId == null)
                    {
                        var DailyCollections = new List<SevenDaysProcessForDay>();
                        var DailyPayments = new List<SevenDaysProcessForDay>();
                        for (int i = -6; i < 1; i++)
                        {
                            var Date = DateTime.Today.AddDays(i);

                            var DailyCollection = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Sales || d.Type == Enums.PaymentType.RefundPurchases || d.Type == Enums.PaymentType.InCome) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = p.Key.ToString(), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyCollection == null)
                            {
                                DailyCollection = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            var DailyPayment = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Purchases || d.Type == Enums.PaymentType.RefundSales || d.Type == Enums.PaymentType.Expense) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = p.Key.ToString(), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyPayment == null)
                            {
                                DailyPayment = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            DailyCollections.Add(DailyCollection);
                            DailyPayments.Add(DailyPayment);
                        }


                        return new SevenDaysProcess()
                        {
                            In = DailyCollections,
                            Out = DailyPayments
                        };
                    }
                    else
                    {
                        DailyPaymentList = DailyPaymentList.Where(d => d.SafesId == SafeId).ToList();

                        var DailyCollections = new List<SevenDaysProcessForDay>();
                        var DailyPayments = new List<SevenDaysProcessForDay>();
                        for (int i = -6; i < 1; i++)
                        {
                            var Date = DateTime.Today.AddDays(i);

                            var DailyCollection = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Sales || d.Type == Enums.PaymentType.RefundPurchases || d.Type == Enums.PaymentType.InCome) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = p.Key.ToString(), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyCollection == null)
                            {
                                DailyCollection = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            var DailyPayment = DailyPaymentList.Where(d =>
                            (d.Type == Enums.PaymentType.Purchases || d.Type == Enums.PaymentType.RefundSales || d.Type == Enums.PaymentType.Expense) && d.Date.Value.Day == Date.Day).GroupBy(p => p.Date.Value.Day).Select(p => new SevenDaysProcessForDay() { Date = p.Key.ToString(), Amount = p.Sum(a => a.Amount) }).FirstOrDefault();
                            if (DailyPayment == null)
                            {
                                DailyPayment = new SevenDaysProcessForDay()
                                {
                                    Date = Date.ToString("dd/MM/yyyy"),
                                    Amount = 0
                                };
                            }

                            DailyCollections.Add(DailyCollection);
                            DailyPayments.Add(DailyPayment);
                        }


                        return new SevenDaysProcess()
                        {
                            In = DailyCollections,
                            Out = DailyPayments
                        };
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Balances Balances(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var Payments = db.Payments.Where(p => p.UserId == UserId).ToList();
                    var Banks = db.Banks.Where(p => p.UserId == UserId).ToList();
                    var Safes = db.Safes.Where(p => p.UserId == UserId).ToList();

                    var BankBalances = Payments.Where(p => p.BankId != null).GroupBy(p => p.BankId).Select(p => new BankBalance() { Balance = p.Sum(a => a.Amount), Name = Banks.Where(bank => bank.Id == p.Key).Select(bank => bank.Name).FirstOrDefault(), AccountNo = Banks.Where(bank => bank.Id == p.Key).Select(bank => bank.AccountNo).FirstOrDefault() }).ToList();
                    var SafeBalances = Payments.Where(p => p.SafesId != null).GroupBy(p => p.SafesId).Select(p => new SafeBalance() { Balance = p.Sum(a => a.Amount), Name = Safes.Where(safe => safe.Id == p.Key).Select(bank => bank.Name).FirstOrDefault() }).ToList();

                    return new Balances()
                    {
                        BankBalances = BankBalances,
                        SafeBalances = SafeBalances,
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
