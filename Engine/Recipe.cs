using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Item OutputItem { get; set; }
        public List<CraftingIngredient> Ingredients { get; set; } = new List<CraftingIngredient>();

        public Recipe(int id, string name, Item outputItem)
        {
            ID = id;
            Name = name;
            OutputItem = outputItem;
        }

        public void AddIngredient(Item item, int quantity)
        {
            Ingredients.Add(new CraftingIngredient(item, quantity));
        }
    }
}
