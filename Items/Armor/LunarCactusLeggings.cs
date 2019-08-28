using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class LunarCactusLeggings : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Cactus Leggings");
			Tooltip.SetDefault("Allows the wearer to run super fast");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 100000;
			item.rare = 9;
			item.defense = 20;
		}
		public override bool DrawLegs()
		{
			return false;
		}
		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.50f;
			player.accRunSpeed *= 1.5f;
			player.maxRunSpeed *= 1.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus, 30);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}