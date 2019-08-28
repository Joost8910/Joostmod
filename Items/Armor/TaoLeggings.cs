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
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 5;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
            player.allDamage += 0.03f;
			player.meleeCrit += 3;
			player.magicCrit += 3;
			player.rangedCrit += 3;
			player.thrownCrit += 3;
			player.moveSpeed += 0.15f;
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