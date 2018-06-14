using Cookidea.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using static Cookidea.MainViewModel;

namespace Cookidea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage, IBasePage<MainViewModel>
    {
        public ResultPage()
        {
            InitializeComponent();
            App.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void OnAppearing()
        {
            this.BindingContext = App.ViewModel;
            base.OnAppearing();
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TouchedRecipeUrl")
            {
                Navigation.PushAsync(new WebViewPage());
            }
        }
    }
}
