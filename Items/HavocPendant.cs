using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class HavocPendant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havoc Pendant");
			Tooltip.SetDefault("Multiplies spawnrates by 5");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 36;
			item.value = 10000;
			item.rare = 3;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
		player.GetModPlayer<JoostPlayer>(mod).HavocPendant = true;
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofNight, 8);
			recipe.AddIngredient(ItemID.DemoniteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofNight, 8);
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}