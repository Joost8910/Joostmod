using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SandstormHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Helmet");
			Tooltip.SetDefault("25% increased Throwing Damage\n" + "50% chance to not consume thrown items");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 15;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandstormBreastplate>() && legs.type == ModContent.ItemType<SandstormLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Throwing weapons buffet enemies with sand";
			player.GetModPlayer<JoostPlayer>().sandStorm = true;

		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Throwing) += 0.25f;
			player.ThrownCost50 = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.DesertCore>()
				.AddRecipeGroup(nameof(ItemID.AdamantiteBar), 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}