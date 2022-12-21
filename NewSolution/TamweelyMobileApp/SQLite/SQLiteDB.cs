using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;

namespace TamweelyMobileApp.SQL
{
    public class SQLiteDB
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<SQLiteDB> Instance = new AsyncLazy<SQLiteDB>(async () =>
        {
            var instance = new SQLiteDB();
            await Database.CreateTableAsync<UserModel>();
            await Database.CreateTableAsync<TokenModel>();
            await Database.CreateTableAsync<Language>();
            return instance;
        });

        public SQLiteDB()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
        public Task<Language> GetLanguageAsync()
        {
            return Database.Table<Language>().FirstOrDefaultAsync();
        }
        public Task<int> SaveLanguageAsync(Language item)
        {
            Database.DeleteAllAsync<Language>();
            return Database.InsertAsync(item);
        }
        public Task<UserModel> GetUserAsync()
        {
            return Database.Table<UserModel>().FirstOrDefaultAsync();
        }
        public Task<TokenModel> GetTokenAsync()
        {
            return Database.Table<TokenModel>().FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(UserModel item)
        {
            return Database.InsertAsync(item);
        }
        public Task<int> DeleteAllUsersAsync()
        {
            return Database.DeleteAllAsync<UserModel>();
        }
        public Task<int> DeleteAllTokensAsync()
        {
            return Database.DeleteAllAsync<TokenModel>();
        }
        public Task<int> SaveTokenAsync(TokenModel item)
        {
            return Database.InsertAsync(item);
        }
    }
    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
