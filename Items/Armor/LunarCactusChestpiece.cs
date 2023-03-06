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
			ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 30;
			Item.value = 100000;
			Item.rare = ItemRarityID.Cyan;
			Item.defense = 25;
			Item.lifeRegen = 8;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Throwing) += 35;
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