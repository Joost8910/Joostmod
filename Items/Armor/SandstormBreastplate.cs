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
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 4;
			item.defense = 20;
		}


		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 20;
			player.resistCold = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DesertCore", 1);
			recipe.AddIngredient(ItemID.AdamantiteBar, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DesertCore", 1);
			recipe.AddIngredient(ItemID.TitaniumBar, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}