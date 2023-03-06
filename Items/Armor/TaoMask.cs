//TODO: Gives this a cooler set bonus. This armor is boring currently.
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class TaoMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tao Mask");
			Tooltip.SetDefault("8% increased damage and crit chance");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 14400;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 10;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("TaoBreastplate").Type && legs.type == Mod.Find<ModItem>("TaoLeggings").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Life regen increased by 5";
			player.lifeRegen += 5;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.08f;
			player.GetCritChance(DamageClass.Generic) += 8;
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