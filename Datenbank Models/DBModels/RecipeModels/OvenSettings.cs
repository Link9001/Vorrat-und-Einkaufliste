using System.Collections.Generic;
using Database_Models.Interfaces;

namespace Database_Models.DBModels.RecipeModels
{
    public class OvenSettings : IDataBaseModel, IName
    {
        public static readonly OvenSettings EmptyOvenSettings = new(string.Empty);
        public string Name { get; set; }

        public OvenSettings(string name)
        {
            Name = name;
        }

        public List<string> Validate()
        {
            var errorList = new List<string>();
            if (string.IsNullOrWhiteSpace(Name))
            {
                errorList.Add("Wie heisst die Einstellung im Ofen?");
            }

            return errorList;
        }

        public string GetName()
        {
            return "Ofeneinstellungen";
        }
    }
}