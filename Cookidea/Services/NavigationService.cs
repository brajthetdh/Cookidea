using System.Linq;
using Xamarin.Forms;

namespace Cookidea.Services
{
    public class NavigationService : INavigationService
    {
        public async void NavigateTo(Page nextPage)
        {
            await GetCurrentPage().Navigation.PushAsync(nextPage);
        }

        public async void NavigateBack()
        {
            await GetCurrentPage().Navigation.PopAsync();
        }

        private Page GetCurrentPage()
        {
            return Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();


        }
    }
}
