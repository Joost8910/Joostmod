using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class GlowSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Glowstick");
			Tooltip.SetDefault("Provides light");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 30;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.position.Y / 16f), 0.7f, 1f, 0.8f);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(230, 204, 128);
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
.AddRecipeGroup("JoostMod:Saplings")
.AddIngredient(ItemID.Glowstick)
.Register();
        }
    }
}