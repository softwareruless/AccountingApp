using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class DashboardDailyReports
    {
        public DailySalesAndPurchasesReports DailySales { get; set; }
        public DailySalesAndPurchasesReports DailyPurchases { get; set; }
        public DailyCollectionAndPaymentReports DailyCollection { get; set; }
        public DailyCollectionAndPaymentReports DailyPayment { get; set; }
    }

    public class DailySalesAndPurchasesReports
    {
        public double Cash { get; set; }
        public double CreditCard { get; set; }
        public double OpenAccount { get; set; }
    }

    public class DailyCollectionAndPaymentReports
    {
        public double Check { get; set; }
        public double Cash { get; set; }
        public double CreditCard { get; set; }
    }

    public class DashboardSevenDaysProcessReports
    {
        public SevenDaysSales DailySales { get; set; }
        public SevenDaysExpiryProcess DailyExpiryProcess { get; set; }
    }

    public class SevenDaysSales
    {
        public List<DailySale> dailySales { get; set; }
    }

    public class SevenDaysExpiryProcess
    {
        public List<DailyCollectionAndPayment> DailyCollections { get; set; }
        public List<DailyCollectionAndPayment> DailyPayments { get; set; }

    }

    public class DailySale
    {
        public string Date { get; set; }
        public double Amount { get; set; }
    }

    public class DailyCollectionAndPayment
    {
        public string Date { get; set; }
        public double Amount { get; set; }
    }

    public class SevenDaysProcess
    {
        public List<SevenDaysProcessForDay> In { get; set; }
        public List<SevenDaysProcessForDay> Out { get; set; }
    }

    public class SevenDaysProcessForDay
    {
        public string Date { get; set; }
        public double Amount { get; set; }

    }

    public class Balances
    {
        public List<BankBalance> BankBalances { get; set; }
        public List<SafeBalance> SafeBalances { get; set; }
    }

    public class BankBalance
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public double Balance { get; set; }
    }
    
    public class SafeBalance
    {
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}
