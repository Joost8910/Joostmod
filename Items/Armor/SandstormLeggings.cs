using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class SandstormLeggings : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Leggings");
			Tooltip.SetDefault("20% increased Throwing Velocity and movement speed\n" 
			+ "Sandstorms no longer blow you around");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 4;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.20f;
			player.thrownVelocity *= 1.20f;
			player.buffImmune[BuffID.WindPushed] = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DesertCore", 1);
			recipe.AddIngredient(ItemID.AdamantiteBar, 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DesertCore", 1);
			recipe.AddIngredient(ItemID.TitaniumBar, 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}