using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class UserDataAccess
    {
        public static User GetById(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<User> GetList()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static User Login(string email, string password)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Where(i => i.Email == email && i.Password == password).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(User model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Users.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kullanıcı Başarıyla Eklendi",
                        Status = true,
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

        public static ResponseMessage Update(User User)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Where(u => u.Id == User.Id).FirstOrDefault();

                    data.Name = User.Name;
                    data.Surname = User.Surname;
                    data.CompanyName = User.CompanyName;
                    data.Authorized = User.Authorized;
                    data.City = User.City;
                    data.District = User.District;
                    data.PostCode = User.PostCode;
                    data.Address = User.Address;
                    data.Email = User.Email;
                    data.Phone = User.Phone;
                    data.MersisNo = User.MersisNo;
                    data.TaxNo = User.TaxNo;
                    data.TaxOffice = User.TaxOffice;
                    data.TcNo = User.TcNo;
                    data.WebSite = User.WebSite;

                    db.Users.Update(data);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kullanıcı Başarıyla Güncellendi",
                        Status = true,
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

        public static ResponseMessage Delete(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Find(id);
                    db.Users.Remove(data);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kullanıcı Başarıyla Silindi",
                        Status = true,
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
