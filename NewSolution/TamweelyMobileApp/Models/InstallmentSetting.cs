using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class InstallmentSetting
    {
        public int Id { get; set; }
        public string SalaryRangeFrom { get; set; }
        public string SalaryFrom { get; set; }
        public string SalaryTo { get; set; }
        public double NoMorgage { get; set; }
        public double WithMirgage { get; set; }
        public DateTime UpdatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public object SystemAdmin { get; set; }
    }
}
