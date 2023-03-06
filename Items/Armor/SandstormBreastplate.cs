using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class SandstormBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Breastplate");
			Tooltip.SetDefault("20% increased Throwing Crit chance\n" +
			"Reduced damage from cold sources");
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 20;
		}


		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Throwing) += 20;
			player.resistCold = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.DesertCore>()
				.AddRecipeGroup("JoostMod:AnyAdamantite", 20)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}