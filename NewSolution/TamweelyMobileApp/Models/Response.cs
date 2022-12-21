using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class BaseResponseModel<T>
    {
        public string Status;
        public T Data;
        public string Message;
    }

    public class BaseListResponseModel<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public string Next { get; set; }
        public string Pervious { get; set; }
        public int CurrentPage { get; set; }
    }
}
