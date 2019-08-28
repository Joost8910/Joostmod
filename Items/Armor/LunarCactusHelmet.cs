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
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 26;
			item.value = 100000;
			item.rare = 9;
			item.defense = 20;
		}

        public override bool DrawHead()
        {
            return false;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("LunarCactusChestpiece") && legs.type == mod.ItemType("LunarCactusLeggings");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownVelocity *= 1.35f;
			player.thrownCost50 = true;
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Throwing Damage increased by 50% and returns 250% enemy contact damage back to the attacker";
			player.thrownDamage += 0.5f;
			player.thorns += 2.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus, 30);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}