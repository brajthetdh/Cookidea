using Cookidea.Services;
using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Cookidea.TranslateExtension;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Cookidea
{
    public partial class App : Application
    {
        public static MainViewModel ViewModel;
        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                LocalizeService localizeService = new LocalizeService();
                var ci = localizeService.GetCurrentCultureInfo();
                AppResources.Culture = ci; // set the RESX for resource localization
                localizeService.SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
