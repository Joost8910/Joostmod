using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class SpaceJump : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Space Jump");
			Tooltip.SetDefault("Allows you to jump infinitely");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = 52000;
			item.rare = 8;
			item.accessory = true;
		}

			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 8);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddIngredient(ItemID.BundleofBalloons);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<JoostPlayer>().spaceJump = true;
		}

	}
}