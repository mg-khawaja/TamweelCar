using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TamweelyMobileApp.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int SQLId { get; set; }
        public int Id { get; set; }
        public string GUID { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string CountryCode { get; set; }
        public string Code { get; set; }
        public int? OTP { get; set; }
        public bool? IsAccountVerified { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsArchive { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        [Ignore]
        public TokenModel OauthToken { get; set; }
    }
}
