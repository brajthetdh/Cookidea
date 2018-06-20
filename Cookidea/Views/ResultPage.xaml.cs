using Cookidea.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace Cookidea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
    }
}
