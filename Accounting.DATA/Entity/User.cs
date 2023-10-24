using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string City { get; set; }
        public string CityCode { get; set; }
        public string District { get; set; }
        public string DistrictCode { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public bool? EInvoice { get; set; }
        public string MersisNo { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string TcNo { get; set; }
        public string CurrecyType { get; set; }
        public bool? IsIndividual { get; set; }
        public string CompanyName { get; set; }
        public string Authorized { get; set; }
        public string WebSite { get; set; }



        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ActivationDate { get; set; }
        public Guid RoleId { get; set; }

    }
}
