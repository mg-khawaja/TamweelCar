using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class Language
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string DisplayName
        {
            get;
            set;
        }
        public string ShortName
        {
            get;
            set;
        }
        public string Flag
        {
            get;
            set;
        }
    }
}
