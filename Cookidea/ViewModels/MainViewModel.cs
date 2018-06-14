using Cookidea.ViewModels;
using QuickType;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        private int _recipesCount;
        public int RecipesCount
        {
            get { return this._recipesCount; }
            set { this.SetField(ref this._recipesCount, value); }
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
        #endregion

        #region Constructor
        public MainViewModel()
        {
            ItemTappedCommand = new BaseCommand((param) =>
            {

                var item = LastTappedItem as SimpleItem;
                if (item != null)
                    System.Diagnostics.Debug.WriteLine("Tapped {0}", item.Title);

            });

            this.IsBusy = false;
            this.Query = new Query();
            this.RecipesCount = 0;
            this.Title = "";
            this.ImageUrl = "";
        }
        #endregion

        #region Methods
        public async void SearchRecipesAsync(string param)
        {
            this.IsBusy = true;
            this.Recipe.Clear();
            Query results = await Services.DownloadService.GetRecipesAsync(param);

            this.Query = results;
            this.RecipesCount = results.Count;
            this.Recipe = new ObservableCollection<Recipe>(results.Recipes);

            this.IsBusy = false;
        }

        #endregion

        public ICommand ItemTappedCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand AddCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand RemoveCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public object LastTappedItem
        {
            get { return GetField<object>(); }
            set { SetField(value); }
        }

        public class SimpleItem : BaseModel
        {
            string title;
            public string Title
            {
                get { return title; }
                set { SetField(ref title, value); }
            }

            public Color Color { get; private set; } = Color.Blue;
        }
    }
}
