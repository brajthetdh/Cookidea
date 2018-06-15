using Plugin.Connectivity;
using QuickType;
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
            var response = await client.GetStringAsync(url);
            var parsedJSON = Query.FromJson(response);
            return parsedJSON;
        }

        public static bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected ? true : false;
        }
    }
}
