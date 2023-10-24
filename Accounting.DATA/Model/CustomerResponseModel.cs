using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class CustomerResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string SpeCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Authorized { get; set; }
        public double Balance { get; set; }
    }
}
