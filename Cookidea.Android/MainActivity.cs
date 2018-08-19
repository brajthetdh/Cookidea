using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Cookidea
{
    [Activity(Label = "Cookidea", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Droid.Resource.Layout.Tabbar;
            ToolbarResource = Droid.Resource.Layout.Toolbar;

            UserDialogs.Init(this);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

