using System;
using System.Collections.Generic;
using TamweelyMobileApp.Models;

namespace TamweelyMobileApp.Helpers
{
    public class GlobalSetting
    {
        //live
        //public const string DefaultEndpoint = "http://staging-admin-portal-env.us-east-2.elasticbeanstalk.com/api/User/";
        //staging
        public const string DefaultEndpoint = "http://stagingsecondadminportal-env.eba-mx2vu4qd.us-east-2.elasticbeanstalk.com/api/User/";

        public GlobalSetting()
        {
            AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBhc3BuZXRib2lsZXJwbGF0ZS5jb20iLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6IjcyZmY1NWZlLTVmNjgtZjI5YS00NmQyLTNhMDE0N2IxZDI5NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwic3ViIjoiMSIsImp0aSI6ImYzYWNhODA1LTJmOGYtNGI2MC04NjNkLTc1OWY4ZTQ1NTA0ZSIsImlhdCI6MTY0MzM2MjUwMywibmJmIjoxNjQzMzYyNTAzLCJleHAiOjE2NDM0NDg5MDMsImlzcyI6IkFQSSIsImF1ZCI6IkFQSSJ9.M2YAlOWFD6ZYe7rhjjXJVK0A96zkofV7qIBMZd4yND4";
            Token = "CfDJ8A3IjFOFhJxDmTO6ftHAvJVYxQ0SIz4729wTKu_LvjVsjwQMGlu4AXgeC9V7XgTBZxnfsmSmBdQ8VxnWcw7yubXXrMdZi9wntbrbKZ7UYTQ1VQAfVqj8xFTFFfDQ4d7PMvizMoIC4uq-k_q3TXwx5itA0dg4x97OqOsaBhnxnatDjSFroruDnU8Y0ut0DjVO1A";
        }
        
        public UserModel userModel { get; set; }
        public TokenModel tokenModel { get; set; }
        public List<string> Links;
        public string VehicleLink { get { return "https://tamweely.s3.us-east-2.amazonaws.com/Vehicles/"; } }
        public string AdLink { get { return "https://tamweely.s3.us-east-2.amazonaws.com/Advertisement/"; } }
        public static GlobalSetting Instance { get; } = new GlobalSetting();
        public string ClientId { get { return "xamarin"; } }
        public string ClientSecret { get { return "secret"; } }
        public string AuthToken { get; set; }
        public string Token { get; set; }
        public string RegisterWebsite { get; set; }
        public string AuthorizeEndpoint { get; set; }
        public string UserInfoEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string LogoutEndpoint { get; set; }
        public string Callback { get; set; }
        public string LogoutCallback { get; set; }
    }
}