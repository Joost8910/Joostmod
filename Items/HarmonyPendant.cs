using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class HarmonyPendant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harmony Pendant");
			Tooltip.SetDefault("Reduces spawnrates by 80%");
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
		player.GetModPlayer<JoostPlayer>(mod).HarmonyPendant = true;
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 8);
			recipe.AddIngredient(ItemID.HallowedBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}