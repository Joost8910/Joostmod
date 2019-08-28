using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class CorruptPommel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corrupt Pommel");
			Tooltip.SetDefault("Striking an enemy with a melee weapon inflicts Corrupted Soul, dealing damage over time\n" +
                               "Enemies that die with Corrupted Soul damages a nearby enemy for 25% \n" + 
                               "of the corrupted enemy's max life (capping at 9999) and inflicts Corrupted Soul\n");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.value = 45000;
			item.rare = 3;
            item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostModPlayer>(mod).corruptPommel = true;
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
            recipe.AddIngredient(null, "CrimsonPommel");
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}