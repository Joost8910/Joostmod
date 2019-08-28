using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class Gravdistorter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gravity Distortion Core");
			Tooltip.SetDefault("Gravity gets weird");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 50000;
			item.rare = 3;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(164, 2);
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.InletPump, 2);
			recipe.AddIngredient(ItemID.OutletPump, 2);
			recipe.AddIngredient(ItemID.GravitationPotion, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

