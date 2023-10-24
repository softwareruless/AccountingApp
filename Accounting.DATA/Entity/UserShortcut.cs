using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class UserShortcut
    {
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [ForeignKey("ShortcutId")]
        public Guid ShortcutId { get; set; }

        public bool Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Shortcut Shortcut { get; set; }
    }
}
