using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftRPG
{
    public partial class CraftingTable : Form
    {
        private Player _player;
        private MinecraftRPG _mainForm;

        public CraftingTable(Player player, MinecraftRPG mainForm)
        {
            InitializeComponent();

            _player = player;
            _mainForm = mainForm;
            lbRecipes.DataSource = World.Recipes;
            lbRecipes.DisplayMember = "Name";
            lbRecipes.ValueMember = "ID";
        }

        private void lbRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recipe selectedRecipe = (Recipe)lbRecipes.SelectedItem;

            if (selectedRecipe == null)
            {
                return;
            }

            rtbRecipeDetails.Clear();
            rtbRecipeDetails.Text = selectedRecipe.Name + Environment.NewLine;
            rtbRecipeDetails.Text += Environment.NewLine;
            rtbRecipeDetails.Text += "Materials Required:" + Environment.NewLine;

            foreach(CraftingIngredient ingredient in selectedRecipe.Ingredients)
            {
                if (ingredient.Quantity > 1)
                {
                    rtbRecipeDetails.Text += "  - " + ingredient.Quantity + " " + ingredient.Details.NamePlural + Environment.NewLine;
                }
                else
                {
                    rtbRecipeDetails.Text += "  - " + ingredient.Quantity + " " + ingredient.Details.Name + Environment.NewLine;
                }
            }

            bool hasAllItems = true;

            foreach(CraftingIngredient craftingIngredient in selectedRecipe.Ingredients)
            {
                InventoryItem playerItem = _player.Inventory.SingleOrDefault(ii => ii.Details.ID == craftingIngredient.Details.ID);
                if (playerItem == null || playerItem.Quantity < craftingIngredient.Quantity)
                {
                    hasAllItems = false;
                    break;
                }
            }

            if (!hasAllItems)
            {
                btnConfirmCraft.Enabled = false;
                btnConfirmCraft.Text = "Missing Materials";
            }
            else
            {
                btnConfirmCraft.Enabled = true;
                btnConfirmCraft.Text = "Craft";
            }
        }
        private void btnCraft_Click(object sender, EventArgs e)
        {
            Recipe selectedRecipe = (Recipe)lbRecipes.SelectedItem;

            if (selectedRecipe == null)
            {
                return;
            }

            bool success = _player.CraftRecipe(selectedRecipe);

            if (success)
            {
                _mainForm.AddMessage("Successfully crafted " + selectedRecipe.Name + "!");
            }
            else
            {
                _mainForm.AddMessage("Missing materials to craft " + selectedRecipe.Name + ".");
            }

            _mainForm.UpdateInventoryListInUI();
            _mainForm.UpdateWeaponListInUI();
            _mainForm.UpdateConsumableListInUI();
            lbRecipes_SelectedIndexChanged(sender, e);
        }
    }
}
    