using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace DataModel.Recipes
{
    public class Recipe
    {
        public string RecipeName { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new();
        public int Oven_Settings { get; set; }
        public string Duration { get; set; }
        public string Preparation { get; set; }

        public Recipe(string resipeName, ObservableCollection<Ingredient> ingredients, Oven_Settings oven_Settings, string duration, string preparation)
        {
            RecipeName = resipeName;
            Oven_Settings = (int)oven_Settings;
            Ingredients = ingredients;
            Duration = duration;
            Preparation = preparation;
        }
    }
}