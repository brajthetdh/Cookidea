using Acr.UserDialogs;
using Cookidea.Models;
using Cookidea.Services;
using Cookidea.Views;
using Plugin.Connectivity;
using QuickType;
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
        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get { if (this._recipes == null) this._recipes = new ObservableCollection<Recipe>(); return this._recipes; }
            set { this.SetField(ref this._recipes, value); }
        }

        private ObservableCollection<Recipe> _favRecipes;
        public ObservableCollection<Recipe> FavRecipes
        {
            get { if (this._favRecipes == null) this._favRecipes = new ObservableCollection<Recipe>(); return this._favRecipes; }
            set { this.SetField(ref this._favRecipes, value); }
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

        private Recipe _touchedRecipe;
        public Recipe TouchedRecipe
        {
            get { return this._touchedRecipe; }
            set { this.SetField(ref this._touchedRecipe, value); }
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

        private bool _isFavRecipesEmpty;
        public bool IsFavRecipesEmpty
        {
            get { return this._isFavRecipesEmpty; }
            set { this.SetField(ref this._isFavRecipesEmpty, value); }
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

        public ICommand CmdFavMainTapped
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
            this.Recipes.Clear();

            this.Query = await Services.DownloadService.GetRecipesAsync(param);
            if(this.Query != null)
            {
                this.Recipes = new ObservableCollection<Recipe>(this.Query.Recipes);
                if (this.Recipes.Count == 0)
                {
                    await App.Current.MainPage.DisplayAlert(AppResources.AlertNoResultsTitle, AppResources.AlertNoResultsDesc, AppResources.AlertOk);
                }
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
                    new NavigationService().NavigateTo(new WebViewPage());

                    //if the recipe already is in favorites
                    if(await App.DatabaseService.GetItemAsync(recipe.RecipeId) != null)
                    {
                        App.Current.MainPage.ToolbarItems.Add(new ToolbarItem("", "fav_filled.png", () => DeleteFavorite(recipe)));
                    }
                    else
                    {
                        App.Current.MainPage.ToolbarItems.Add(new ToolbarItem("", "fav_empty.png", () => SaveFavorite(recipe)));
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppResources.AlertInternetTitle, AppResources.AlertInternetDesc, AppResources.AlertOk);
            }
        }

        private async void SaveFavorite(Recipe recipe)
        {
            await App.DatabaseService.SaveItemAsync(recipe);

            UserDialogs.Instance.Toast(new ToastConfig(AppResources.AlertFavAddedDesc)
                                            .SetDuration(3000)
                                            .SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193)));

            this.IsFavRecipesEmpty = false;

            App.Current.MainPage.ToolbarItems.Clear();
            App.Current.MainPage.ToolbarItems.Add(new ToolbarItem("", "fav_filled.png", () => DeleteFavorite(recipe)));
        }

        private async void DeleteFavorite(Recipe recipe)
        {
            await App.DatabaseService.DeleteItemAsync(recipe);

            UserDialogs.Instance.Toast(new ToastConfig(AppResources.AlertFavDeletedDesc)
                                            .SetDuration(3000)
                                            .SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193)));

            this.FavRecipes = new ObservableCollection<Recipe>(await App.DatabaseService.GetItemsAsync());

            App.Current.MainPage.ToolbarItems.Clear();
            App.Current.MainPage.ToolbarItems.Add(new ToolbarItem("", "fav_empty.png", () => SaveFavorite(recipe)));
        }

        private async void BtnSearchClicked()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var regex = new Regex(@"(?i)^[a-z,\s]+$");  //Only letters and commas are valid
                if (!string.IsNullOrEmpty(EntryIngredientsText) && !string.IsNullOrWhiteSpace(EntryIngredientsText) && regex.IsMatch(EntryIngredientsText))
                {
                    SearchRecipesAsync(EntryIngredientsText.Replace(" ", string.Empty));

                    new NavigationService().NavigateTo(new ResultPage());
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
