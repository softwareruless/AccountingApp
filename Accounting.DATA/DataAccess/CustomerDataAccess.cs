using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class CustomerDataAccess
    {
        public static Customer GetById(Guid id, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Customers.FirstOrDefault(i => i.Id == id && i.UserId == UserId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Customer> GetList(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Customers.Where(i => i.UserId == UserId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<CustomerResponseModel> GetListModel(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Customers.Where(i => i.UserId == id).Select(x => new CustomerResponseModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Name2 = x.Name2,
                        Phone = x.Phone,
                        Email = x.Email,
                        Authorized = x.Authorized
                    }).ToList();

                    var Balances = PaymentDataAccess.CalculateCurrentByMultipleCustomer(data.Select(c => c.Id).ToList(), id);

                    data.ForEach(c => c.Balance = Balances.Where(b => b.CustomerId == c.Id).Select(b => b.Amount).FirstOrDefault());

                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Customer model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Customers.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Müşteri Başarıyla Eklendi",
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
                    var data = db.Customers.Where(i => i.Id == id).FirstOrDefault();
                    db.Customers.Remove(data);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Müşteri Başarıyla Silindi",
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

        public static ResponseMessage Update(Customer model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Customers.Find(model.Id);
                    data.Name = model.Name;
                    data.City = model.City;
                    data.District = model.District;
                    data.PostCode = model.PostCode;
                    data.Address = model.Address;
                    data.Email = model.Email;
                    data.Phone = model.Phone;
                    data.TaxNo = model.TaxNo;
                    data.TaxOffice = model.TaxOffice;
                    data.TcNo = model.TcNo;
                    data.CompanyName = model.CompanyName;
                    data.Authorized = model.Authorized;

                    data.UpdatedOn = DateTime.Now;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Müşteri Başarıyla Güncellendi",
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
    }
}
