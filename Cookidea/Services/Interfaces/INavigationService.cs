using Xamarin.Forms;

namespace Cookidea.Services
{
    public interface INavigationService
    {
        void NavigateTo(Page nextPage);
        void NavigateBack();
    }
}
