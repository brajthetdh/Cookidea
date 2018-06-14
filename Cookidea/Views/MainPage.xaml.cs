using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                if (!string.IsNullOrEmpty(et_ingredients.Text) && !string.IsNullOrWhiteSpace(et_ingredients.Text))
                {
                    App.ViewModel.SearchRecipesAsync(et_ingredients.Text.Replace(" ", string.Empty));
                    Navigation.PushAsync(new ResultPage());
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
            if (App.ViewModel == null) App.ViewModel = new MainViewModel();
        }
    }
}
