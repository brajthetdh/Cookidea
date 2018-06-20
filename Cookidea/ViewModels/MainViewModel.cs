using Cookidea.Models;
using Cookidea.Services;
using Plugin.Connectivity;
using QuickType;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;

namespace Cookidea
{
    public class MainViewModel : BasePageModel
    {
        #region Properties
        private ObservableCollection<Recipe> _recipe;
        public ObservableCollection<Recipe> Recipe
        {
            get { if (this._recipe == null) this._recipe = new ObservableCollection<Recipe>(); return this._recipe; }
            set { this.SetField(ref this._recipe, value); }
        }

        private string _title;
        public string Title
        {
            get { return this._title; }
            set { this.SetField(ref this._title, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return this._imageUrl; }
            set { this.SetField(ref this._imageUrl, value); }
        }

        private string _recipeUrl;
        public string RecipeUrl
        {
            get { return this._recipeUrl; }
            set { this.SetField(ref this._recipeUrl, value); }
        }

        private string _touchedRecipeUrl;
        public string TouchedRecipeUrl
        {
            get { return this._touchedRecipeUrl; }
            set { this.SetField(ref this._touchedRecipeUrl, value); }
        }

        private Query _query;
        public Query Query
        {
            get { return this._query; }
            set { this.SetField(ref this._query, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetField(ref this._isBusy, value); }
        }

        private string _entryIngredientsText;
        public string EntryIngredientsText
        {
            get { return this._entryIngredientsText; }
            set { this.SetField(ref this._entryIngredientsText, value); }
        }

        public ICommand ItemTappedCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand CmdBtnSearchClicked
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public object LastTappedItem
        {
            get { return GetField<object>(); }
            set { SetField(value); }
        }

        
        #endregion

        #region Constructor
        public MainViewModel()
        {
            IsBusy = false;

            CmdBtnSearchClicked = new BaseCommand(param => BtnSearchClicked());

            ItemTappedCommand = new BaseCommand(param => ItemTapped());
        }
        #endregion

        #region Methods
        public async void SearchRecipesAsync(string param)
        {
            this.IsBusy = true;
            this.Recipe.Clear();

            this.Query = await Services.DownloadService.GetRecipesAsync(param);
            this.Recipe = new ObservableCollection<Recipe>(this.Query.Recipes);
            if(this.Recipe.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.AlertNoResultsTitle, AppResources.AlertNoResultsDesc, AppResources.AlertOk);
            }

            this.IsBusy = false;
        }

        private async void ItemTapped()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Recipe recipe = LastTappedItem as Recipe;
                if (recipe != null)
                {
                    this.TouchedRecipeUrl = recipe.SourceUrl;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppResources.AlertInternetTitle, AppResources.AlertInternetDesc, AppResources.AlertOk);
            }
        }

        private async void BtnSearchClicked()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var regex = new Regex(@"(?i)^[a-z,\s]+$");  //Only letters and commas are valid
                if (!string.IsNullOrEmpty(EntryIngredientsText) && !string.IsNullOrWhiteSpace(EntryIngredientsText) && regex.IsMatch(EntryIngredientsText))
                {
                    SearchRecipesAsync(EntryIngredientsText.Replace(" ", string.Empty));
                    //Navigation.PushAsync(new ResultPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(AppResources.AlertInputTitle, AppResources.AlertInputDesc, AppResources.AlertOk);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppResources.AlertInternetTitle, AppResources.AlertInternetDesc, AppResources.AlertOk);
            }
        }
        #endregion
    }
}
