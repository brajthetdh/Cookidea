using Cookidea.Services;
using Plugin.Connectivity;
using System.Text.RegularExpressions;
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
            
            btn_search.Clicked += (o, s) => {
                if(CrossConnectivity.Current.IsConnected)
                {
                    var regex = new Regex(@"(?i)^[a-z,\s]+$");
                    if (!string.IsNullOrEmpty(et_ingredients.Text) && !string.IsNullOrWhiteSpace(et_ingredients.Text) && regex.IsMatch(et_ingredients.Text))
                    {
                        App.ViewModel.SearchRecipesAsync(et_ingredients.Text.Replace(" ", string.Empty));
                        Navigation.PushAsync(new ResultPage());
                    }
                    else
                    {
                        DisplayAlert(AppResources.da_input_title, AppResources.da_input_desc, AppResources.da_ok);
                    }
                }
                else
                {
                    DisplayAlert(AppResources.da_internet_title, AppResources.da_internet_desc, AppResources.da_ok);
                }
            };              
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
