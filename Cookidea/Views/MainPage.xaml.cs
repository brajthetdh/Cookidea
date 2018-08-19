using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cookidea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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
