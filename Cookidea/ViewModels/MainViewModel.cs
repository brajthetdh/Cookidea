﻿using Cookidea.ViewModels;
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

        public ICommand ItemTappedCommand
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
            this.IsBusy = false;

            ItemTappedCommand = new BaseCommand((param) =>
            {
                Recipe recipe = LastTappedItem as Recipe;
                if (recipe != null)
                {
                    this.TouchedRecipeUrl = recipe.SourceUrl;
                }
            });
        }
        #endregion

        #region Methods
        public async void SearchRecipesAsync(string param)
        {
            this.IsBusy = true;
            this.Recipe.Clear();

            this.Query = await Services.DownloadService.GetRecipesAsync(param);
            this.RecipesCount = this.Query.Count;
            this.Recipe = new ObservableCollection<Recipe>(this.Query.Recipes);

            this.IsBusy = false;
        }
        #endregion
    }
}
