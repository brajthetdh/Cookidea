using Cookidea.Models;
using Cookidea.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cookidea.Services
{
    public class DatabaseService
    {
        #region Properties
        static object locker = new object();
        SQLiteAsyncConnection database;
        #endregion

        #region Constructor
        public DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Recipe>().Wait();
        }
        #endregion

        #region Methods
        public Task<List<Recipe>> GetItemsAsync()
        {
            return database.Table<Recipe>().ToListAsync();
        }

        public Task<List<Recipe>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Recipe>("SELECT * FROM [Recipe] WHERE [Done] = 0");
        }

        public Task<Recipe> GetItemAsync(string id)
        {
            return database.Table<Recipe>().Where(i => i.RecipeId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Recipe item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Recipe item)
        {
            return database.DeleteAsync(item);
        }
        #endregion
    }
}
