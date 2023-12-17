using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class LunarCactusHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Cactus Helmet");
			Tooltip.SetDefault("Throwing velocity increased by 35%\n" + "Reduces thrown item consumption by 50%");
			ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 26;
			Item.value = 100000;
			Item.rare = ItemRarityID.Cyan;
			Item.defense = 20;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<LunarCactusChestpiece>() && legs.type == ModContent.ItemType<LunarCactusLeggings>();
		}

		public override void UpdateEquip(Player player)
		{
			player.ThrownVelocity *= 1.35f;
			player.ThrownCost50 = true;
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Throwing Damage increased by 50% and returns 250% enemy contact damage back to the attacker";
			player.GetDamage(DamageClass.Throwing) += 0.5f;
			player.thorns += 2.5f;
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