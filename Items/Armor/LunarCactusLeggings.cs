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
			ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;

		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 100000;
			Item.rare = ItemRarityID.Cyan;
			Item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.50f;
			player.accRunSpeed *= 1.5f;
			player.maxRunSpeed *= 1.5f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Cactus, 30)
				.AddIngredient(ItemID.LunarBar, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}