using Accounting.DATA.Entity;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Accounting.DATA.Model
{
    public class DataContext : DbContext
    {
        public DataContext() : base(GetOptions())
        {

        }
        private static DbContextOptions GetOptions()
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(),
                "Server=(localdb)\\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;").Options;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Safe> Safes { get; set; }
        public DbSet<Shortcut> Shortcuts { get; set; }
        public DbSet<UserShortcut> UserShortcuts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseTitle> ExpenseTitles { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<InstallmentPayment> InstallmentPayments { get; set; }
        public DbSet<Check> Checks { get; set; }
    }
}
