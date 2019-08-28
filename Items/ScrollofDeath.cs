using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class ScrollofDeath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scroll of Death");
			Tooltip.SetDefault("Summon multiple skulls when below half health\n" + 
			"Increases max number of minions by 2\n" + 
			"Increases summon damage by 15%\n" + 
			"Increases minion knockback");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.rare = 9;
			item.value = 400000;
			item.accessory = true;
			item.damage = 125;
			item.summon = true;
			item.knockBack = 5.5f;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions += 2;
			player.minionKB += 3f;
			player.minionDamage += 0.15f;
			player.GetModPlayer<JoostPlayer>(mod).SkullSigil = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PapyrusScarab);
			recipe.AddIngredient(null, "SigilofSkulls");
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}