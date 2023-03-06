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
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.20f;
			player.ThrownVelocity *= 1.20f;
			player.buffImmune[BuffID.WindPushed] = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.DesertCore>()
				.AddRecipeGroup("JoostMod:AnyAdamantite", 16)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}