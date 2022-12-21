using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class VerifyOTPModel
    {
        public string GUID { get; set; }
        public int Code { get; set; }
        public string CountryCode { get; set; }
        public string MobileNo { get; set; }
    }
}
