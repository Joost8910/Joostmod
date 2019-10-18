using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class BowSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Bow");
			Tooltip.SetDefault("Shoots enemies behind you\n" + "5% increased ranged crit chance");
		}
		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 30;
			item.value = 20000;
			item.rare = 3;
            item.accessory = true;
            item.damage = 12;
            item.ranged = true;
            item.knockBack = 2;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedCrit += 5;
            player.GetModPlayer<JoostPlayer>().bowSapling = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria")
                {
                    if (line2.Name == "ItemName")
                    {
                        line2.overrideColor = new Color(230, 204, 128);
                    }
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.overrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("JoostMod:Saplings");
            recipe.AddIngredient(ItemID.CopperBow);
            //recipe.AddIngredient(ItemID.WoodenArrow, 300);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("JoostMod:Saplings");
            recipe.AddIngredient(ItemID.TinBow);
            //recipe.AddIngredient(ItemID.WoodenArrow, 300);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}