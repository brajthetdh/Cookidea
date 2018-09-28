using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Cookidea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        protected override void OnAppearing()
        {
            InitializeAppAsync();
            this.BindingContext = App.viewModel;
            base.OnAppearing();
        }

        private void InitializeAppAsync()
        {
            if (App.viewModel == null)
            {
                App.viewModel = new MainViewModel();
            }
        }
    }
}
