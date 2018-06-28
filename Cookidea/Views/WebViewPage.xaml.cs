using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cookidea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebViewPage : ContentPage
	{
		public WebViewPage ()
		{
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = App.viewModel;

            await ProgressLoadingWeb.ProgressTo(0.9, 900, Easing.SpringIn);
        }

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            ProgressLoadingWeb.IsVisible = true;
        }

        private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            ProgressLoadingWeb.IsVisible = false;
        }
    }
}