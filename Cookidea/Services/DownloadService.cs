using QuickType;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cookidea.Services
{
    static class DownloadService
    {
        public async static Task<Query> GetRecipesAsync(string param)
        {
            string url = "http://food2fork.com/api/search?key=08656c67b2bde6f54954f384203cc940&q=" + param;
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(url);
                var parsedJSON = Query.FromJson(response);
                return parsedJSON;
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.AlertInternetTitle, AppResources.AlertInternetDesc, AppResources.AlertOk);
                return null;
            }
            
        }
    }
}
