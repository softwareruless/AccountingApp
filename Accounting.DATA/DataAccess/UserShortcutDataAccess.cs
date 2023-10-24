using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting.DATA.DataAccess
{
    public class UserShortcutDataAccess
    {
        public static List<UserShortcut> GetList(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcuts = db.UserShortcuts.Include(us => us.Shortcut).Where(us => us.UserId == UserId).ToList();
                    return shortcuts;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Shortcut> GetListForAdd(Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcuts = db.Shortcuts.ToList();
                    var userShortcuts = db.UserShortcuts.Where(us => us.UserId == UserId).Select(s => s.ShortcutId).ToList();

                    var notExistShortcuts = shortcuts.Where(s => !userShortcuts.Contains(s.Id)).ToList();

                    return notExistShortcuts;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static UserShortcut GetById(Guid UserShortcutId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var shortcut = db.UserShortcuts.FirstOrDefault(us => us.Id == UserShortcutId && us.UserId == UserId);
                    return shortcut;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ResponseMessage Add(List<Guid> ShortcutIds, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var OldUserShortCuts = db.UserShortcuts.Where(us => us.UserId == UserId).Select(us => us.ShortcutId).ToList();

                    var UserShortcutList = new List<UserShortcut>();
                    var Date = DateTime.Now;
                    foreach (var item in ShortcutIds)
                    {
                        if (OldUserShortCuts.Contains(item))
                        {
                            continue;
                        }

                        var userShortcut = new UserShortcut()
                        {
                            UserId = UserId,
                            ShortcutId = item,
                            CreatedOn = Date,
                        };
                        UserShortcutList.Add(userShortcut);
                    }

                    db.UserShortcuts.AddRange(UserShortcutList);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Message = "Kısayollar Başarıyla Eklendi",
                        Status = true
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Message = "Bir Hata Oluştu",
                    Status = false,
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Delete(Guid UserShortcutId, Guid UserId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var userShortcut = db.UserShortcuts.FirstOrDefault(us => us.Id == UserShortcutId && us.UserId == UserId);

                    db.UserShortcuts.Remove(userShortcut);

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
