using Cookidea.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<Recipe>> GetItemsAsync()
        {
            List<Recipe> fav_recipes = await database.Table<Recipe>().ToListAsync();
            if (fav_recipes.Count == 0)
            {
                App.viewModel.IsFavRecipesEmpty = true;
            }
            return fav_recipes;
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
