using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class StaffSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Staff");
			Tooltip.SetDefault("Shoots enemies behind you\n" + "5% reduced mana usage");
		}
		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 30;
			item.value = 20000;
			item.rare = 3;
            item.accessory = true;
            item.damage = 15;
            item.magic = true;
            item.knockBack = 3.5f;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.05f;
            player.GetModPlayer<JoostPlayer>().staffSapling = true;
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
            recipe.AddIngredient(ItemID.AmethystStaff);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("JoostMod:Saplings");
            recipe.AddIngredient(ItemID.TopazStaff);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}