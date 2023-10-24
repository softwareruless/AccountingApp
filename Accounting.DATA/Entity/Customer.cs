using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Entity
{
    public class Customer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string SpeCode { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string District { get; set; }
        public string DistrictCode { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool? EInvoice { get; set; }
        public string Phone { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string TcNo { get; set; }
        public string CurrecyType { get; set; }
        public bool? IsIndividual { get; set; }
        public string CompanyName { get; set; }
        public string Authorized { get; set; }

        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
