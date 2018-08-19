using Cookidea.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cookidea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavPage : ContentPage
	{
		public FavPage ()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            this.BindingContext = App.viewModel;
            App.Current.MainPage.ToolbarItems.Clear();
            App.viewModel.FavRecipes = new ObservableCollection<Recipe>(await App.DatabaseService.GetItemsAsync());
            base.OnAppearing();
        }
    }
}