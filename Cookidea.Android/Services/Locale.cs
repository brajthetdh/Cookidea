using Cookidea.Droid;
using Cookidea.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Locale))]
namespace Cookidea.Droid
{
    public class Locale : ILocaleService
    {
        public object GetLocale()
        {
            return Java.Util.Locale.Default;
        }
    }
}