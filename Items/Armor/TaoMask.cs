using System.Collections.Generic;
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
			item.width = 18;
			item.height = 18;
			item.value = 14400;
			item.rare = 5;
			item.defense = 10;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TaoBreastplate") && legs.type == mod.ItemType("TaoLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Life regen increased by 5";
			player.lifeRegen += 5;
		}
		public override void UpdateEquip(Player player)
		{
			player.allDamage += 0.08f;
			player.meleeCrit += 8;
			player.magicCrit += 8;
			player.rangedCrit += 8;
			player.thrownCrit += 8;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LightShard, 1);
			recipe.AddIngredient(ItemID.DarkShard, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe.AddIngredient(ItemID.SoulofNight, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}