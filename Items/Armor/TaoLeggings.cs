using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class TaoLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tao Leggings");
			Tooltip.SetDefault("3% increased damage and crit chance\n" + "15% increased movement speed");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 14400;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 3;
			player.moveSpeed += 0.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LightShard, 1)
				.AddIngredient(ItemID.DarkShard, 1)
				.AddIngredient(ItemID.SoulofLight, 7)
				.AddIngredient(ItemID.SoulofNight, 7)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}