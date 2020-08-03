using System;
using System.Collections.Generic;
using System.Text;

namespace ImportIOClient.Models.Exceptions
{
    public class ImportClientException : Exception
    {
        public string ErrorMessage { get; set; }
        public int Code { get; set; }
    }
}
