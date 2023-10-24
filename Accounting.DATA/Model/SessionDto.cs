using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CompanyId { get; set; }
        public DateTime? ActivationDate { get; set; }
        public Guid RoleId { get; set; }
    }
}
