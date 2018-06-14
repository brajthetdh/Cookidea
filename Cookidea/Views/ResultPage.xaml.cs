using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using static Cookidea.MainViewModel;

namespace Cookidea
{
    public partial class ResultPage : ContentPage, IBasePage<MainViewModel>
    {
        public ResultPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            this.BindingContext = App.ViewModel;
            base.OnAppearing();
        }
            //DisplayAlert("Alert", App.ViewModel.LastTappedItem.SourceUrl, "OK");
            //Device.OpenUri();
    }
}
