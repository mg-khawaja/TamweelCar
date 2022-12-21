using System.Collections.Generic;
using System.Threading.Tasks;
namespace TamweelyMobileApp.Services.Interfaces
{
    public interface IUserAuthenticate
    {
        Task<Models.BaseResponseModel<Models.UserModel>> SignIn(string phone, string phoneCode);
        Task<Models.BaseResponseModel<Models.UserModel>> SignUp(Models.UserModel user);
        Task<Models.BaseResponseModel<Models.UserModel>> VerifyOTP(Models.UserModel user);
        Task<Models.BaseResponseModel<Models.UserModel>> EditProfile(Models.UserModel user);
        Task<Models.BaseResponseModel<Models.UserModel>> GetProfile();
        Task<List<Models.Make>> GetMakeList();
    }
}
