using Cookidea.Services;
using Plugin.Connectivity;
using System;
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

            //BtnSearch.Clicked += ButtonClicked;
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

        //private void ButtonClicked(object o, EventArgs e)
        //{
        //    if (CrossConnectivity.Current.IsConnected)
        //    {
        //        var regex = new Regex(@"(?i)^[a-z,\s]+$");  //Letters and commas are valid
        //        if (!string.IsNullOrEmpty(EntryIngredients.Text) && !string.IsNullOrWhiteSpace(EntryIngredients.Text) && regex.IsMatch(EntryIngredients.Text))
        //        {
        //            App.ViewModel.SearchRecipesAsync(EntryIngredients.Text.Replace(" ", string.Empty));
        //            Navigation.PushAsync(new ResultPage());
        //        }
        //        else
        //        {
        //            DisplayAlert(AppResources.AlertInputTitle, AppResources.AlertInputDesc, AppResources.AlertOk);
        //        }
        //    }
        //    else
        //    {
        //        DisplayAlert(AppResources.AlertInternetTitle, AppResources.AlertInternetDesc, AppResources.AlertOk);
        //    }
        //}
    }
}
