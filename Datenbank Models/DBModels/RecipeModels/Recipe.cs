using Database_Models.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database_Models.DBModels.RecipeModels;

internal class Recipe : ListViewItem, IDataBaseModel
{
    public ObservableCollection<Ingredient> Ingredients { get; set; }
    public string RecipeName { get; set; }
    public OvenSettings OvenSettings { get; set; }
    public string Duration { get; set; }
    public string Preparation { get; set; }

    public Recipe(ObservableCollection<Ingredient> ingredients, OvenSettings ovenSettings, string resipeName, string duration, string preparation)
    {
        RecipeName = resipeName;
        OvenSettings = ovenSettings;
        Ingredients = ingredients;
        Duration = duration;
        Preparation = preparation;
    }

    public List<string> Validate()
    {
        List<string> errorMessageList = new();
        if (string.IsNullOrWhiteSpace(RecipeName))
        {
            errorMessageList.Add("Kein Titel?");
        }
        if (string.IsNullOrEmpty(OvenSettings.Name))
        {
            errorMessageList.Add("Braucht es den Ofen?");
        }
        if (string.IsNullOrWhiteSpace(Duration))
        {
            errorMessageList.Add("Wie lange braucht du für das Gericht?");
        }
        if (string.IsNullOrWhiteSpace(Preparation))
        {
            errorMessageList.Add("Wie geht das Gericht?");
        }

        return errorMessageList;
    }

    public Recipe CreateCopy()
    {
        Recipe copyRecipe = new(
            Ingredients,
            OvenSettings,
            $"{RecipeName} Copy",
            Duration,
            Preparation
        );
        copyRecipe.Status = Status;
        return copyRecipe;
    }
}