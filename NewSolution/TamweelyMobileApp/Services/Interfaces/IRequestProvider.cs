using System.Net.Http;
using System.Threading.Tasks;

namespace TamweelyMobileApp.Services.Interfaces
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");
        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");
        Task<TResult> PostAsync<TResult>(string uri, StringContent stringContent, string token = "", string header = "");
        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);
        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");
        Task<TResult> PutAsync<TResult>(string uri, StringContent stringContent, string token = "", string header = "");
        Task DeleteAsync(string uri, string token = "");
    }
}
