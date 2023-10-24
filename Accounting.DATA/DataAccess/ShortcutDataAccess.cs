using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class ShortcutDataAccess
    {

        public static List<Shortcut> GetList()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcuts = db.Shortcuts.ToList();
                    return shortcuts;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Shortcut GetById(Guid ShortcutId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcut = db.Shortcuts.FirstOrDefault(s => s.Id == ShortcutId);
                    return shortcut;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Shortcut shortcut)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Shortcuts.Add(shortcut);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Başarıyla Eklendi",
                        Status = true
                    };
                }
            }
            catch (Exception)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu",
                    Status = false
                };
            }
        }

        public static ResponseMessage Update(Shortcut shortcut)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcutDB = db.Shortcuts.FirstOrDefault(S => S.Id == shortcut.Id);

                    shortcutDB.Name = shortcut.Name;
                    shortcutDB.Path = shortcut.Path;
                    shortcutDB.Icon = shortcut.Icon;

                    shortcutDB.UpdatedOn = DateTime.Now;

                    db.Shortcuts.Update(shortcutDB);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Başarıyla Güncellendi",
                        Status = true
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu",
                    Status = false
                };
            }
        }

        public static ResponseMessage Delete(Guid ShortcutId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcut = db.Shortcuts.FirstOrDefault(s => s.Id == ShortcutId);

                    db.Shortcuts.Remove(shortcut);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Başarıyla Silindi",
                        Status = true
                    };
                }
            }
            catch (Exception)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu",
                    Status = false
                };
            }
        }

    }
}
