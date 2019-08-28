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
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 5;
			item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.allDamage += 0.04f;
			player.meleeCrit += 4;
			player.magicCrit += 4;
			player.rangedCrit += 4;
			player.thrownCrit += 4;
			
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