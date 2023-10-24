using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Model
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public object Data { get; set; }
    }
}
