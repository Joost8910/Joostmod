using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class GenjiLeggings : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Genji Leggings");
			Tooltip.SetDefault("Allows the wearer to run incredibly fast\n" + "Life regeneration increased by 8");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000000;
			item.rare = 11;
			item.defense = 20;
			item.lifeRegen = 8;
		}
		    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.overrideColor = new Color(0, 255, 0);
            }
        }
    }
		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.85f;
			player.accRunSpeed *= 1.85f;
			player.maxRunSpeed *= 1.85f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GenjiToken", 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}