using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class ProductDataAccess
    {
        public static Product GetById(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Products.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Product> GetList(Guid id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Products.Where(i => i.UserId == id).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Product model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Products.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Ürün Başarıyla Eklendi",
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
                    var data = db.Products.Where(i => i.Id == id).FirstOrDefault();
                    db.Products.Remove(data);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Ürün Başarıyla Silindi",
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

        public static ResponseMessage Update(Product model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Products.Find(model.Id);
                    data.Name = model.Name;
                    data.Description = model.Description;
                    data.Description1 = model.Description1;
                    data.Description2 = model.Description2;
                    data.Description3 = model.Description3;
                    data.Description4 = model.Description4;
                    data.Stock = model.Stock;
                    data.Price= model.Price;

                    data.UpdatedOn = DateTime.Now;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Ürün Başarıyla Güncellendi",
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
