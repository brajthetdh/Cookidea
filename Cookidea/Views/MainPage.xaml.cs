using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cookidea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            InitializeAppAsync();
            this.BindingContext = App.ViewModel;
            base.OnAppearing();
        }

        private void InitializeAppAsync()
        {
            if (App.ViewModel == null)
            {
                App.ViewModel = new MainViewModel();
            }
        }
    }
}
