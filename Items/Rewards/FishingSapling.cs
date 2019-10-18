using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class FishingSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Fishing Rod");
			Tooltip.SetDefault("Fishes automatically when you hold a fishing pole\n" + "+5 Fishing Power");
		}
		public override void SetDefaults()
		{
			item.width = 52;
			item.height = 36;
			item.value = 20000;
			item.rare = 3;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.fishingSkill += 5;
            player.GetModPlayer<JoostPlayer>().fishingSapling = true;
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
            recipe.AddIngredient(ItemID.WoodFishingPole);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}