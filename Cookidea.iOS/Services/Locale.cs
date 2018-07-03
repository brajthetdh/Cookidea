using Cookidea.iOS;
using Cookidea.Services;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(Locale))]
namespace Cookidea.iOS
{
    public class Locale : ILocaleService
    {
        public object GetLocale()
        {
            return NSLocale.PreferredLanguages;
        }
    }
}