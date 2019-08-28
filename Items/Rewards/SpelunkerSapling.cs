using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class SpelunkerSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Spelunker Glowstick");
			Tooltip.SetDefault("Provides light\n" + 
            "Exposes nearby treasure");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 30;
			item.value = 20000;
			item.rare = 3;
            item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.position.Y / 16f), 1.05f, 0.95f, 0.55f);
            player.GetModPlayer<JoostPlayer>(mod).spelunky = 30;
            player.GetModPlayer<JoostPlayer>(mod).spelunkGlow = true;
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
            recipe.AddIngredient(ItemID.SpelunkerGlowstick);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}