using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class AdModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string ImageName { get; set; }
        public string URL { get; set; }
        public bool ShowOnHome { get; set; }
        public bool IsArchive { get; set; }
    }
}
