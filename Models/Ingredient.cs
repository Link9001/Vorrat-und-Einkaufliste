using Newtonsoft.Json;

namespace DataModel.Recipes
{
    public class Ingredient
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public bool IsInStockList { get; set; }

        public Ingredient(string name, string quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        [JsonConstructor]
        public Ingredient(string name, string quantity, bool isInStocklist)
        {
            Name = name;
            Quantity = quantity;
            IsInStockList = isInStocklist;
        }
    }
}