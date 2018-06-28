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

        protected override void OnAppearing()
        {
            this.BindingContext = App.viewModel;
            App.Current.MainPage.ToolbarItems.Clear();
            base.OnAppearing();
        }
    }
}