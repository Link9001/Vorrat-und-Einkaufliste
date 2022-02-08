using Database_Models.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database_Models.DBModels.RecipeModels;

internal record Recipe(ObservableCollection<Ingredient> Ingredients, OvenSettings OvenSettings, string RecipeName, string Duration, string Preparation) : ListViewItem, IDataBaseModel
{
    public ObservableCollection<Ingredient> Ingredients { get; set; } = Ingredients;
    public string RecipeName { get; set; } = RecipeName;
    public OvenSettings OvenSettings { get; set; } = OvenSettings;
    public string Duration { get; set; } = Duration;
    public string Preparation { get; set; } = Preparation;

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