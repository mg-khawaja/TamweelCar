using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class InstallmentConfig
    {
        public int Id { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double DefaultValue { get; set; }
        public DateTime UpdatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public string Description { get; set; }
        public string UpdateByName { get; set; }
    }
}
