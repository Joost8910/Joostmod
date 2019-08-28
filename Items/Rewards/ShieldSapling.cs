using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class ShieldSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Shield");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 30;
			item.value = 20000;
			item.rare = 3;
            item.accessory = true;
            item.defense = 3;
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(230, 204, 128);
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("JoostMod:Saplings");
            recipe.AddIngredient(ItemID.CopperBar, 7);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("JoostMod:Saplings");
            recipe.AddIngredient(ItemID.TinBar, 7);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}