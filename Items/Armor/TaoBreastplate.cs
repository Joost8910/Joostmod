using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class TaoBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tao Breastplate");
			Tooltip.SetDefault("4% increased damage and crit chance");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 14400;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.04f;
			player.GetCritChance(DamageClass.Generic) += 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LightShard)
				.AddIngredient(ItemID.DarkShard)
				.AddIngredient(ItemID.SoulofLight, 7)
				.AddIngredient(ItemID.SoulofNight, 7)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}