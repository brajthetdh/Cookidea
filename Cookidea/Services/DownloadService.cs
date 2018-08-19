using QuickType;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cookidea.Services
{
    static class DownloadService
    {
        /// <summary>
        /// Change apiKey with your API key obtained from food2fork.com
        /// </summary>
        public async static Task<Query> GetRecipesAsync(string param)
        {
            string apiKey = "WRITE-HERE-YOUR-API-KEY";
            string url = "http://food2fork.com/api/search?key="+ apiKey +"&q=" + param;
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
