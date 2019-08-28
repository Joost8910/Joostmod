using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class LunarCactusChestpiece : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Cactus Chestpiece");
			Tooltip.SetDefault("Throwing crit chance increased by 35%\n" + "Life Regeneration increased by 8");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 30;
			item.value = 100000;
			item.rare = 9;
			item.defense = 25;
			item.lifeRegen = 8;
		}
		public override bool DrawBody()
		{
			return false;
		}
		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 35;
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