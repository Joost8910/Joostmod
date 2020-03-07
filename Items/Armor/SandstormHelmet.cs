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
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 4;
			item.defense = 15;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("SandstormBreastplate") && legs.type == mod.ItemType("SandstormLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Throwing weapons buffet enemies with sand";
			player.GetModPlayer<JoostPlayer>().sandStorm = true;

		}
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.25f;
			player.thrownCost50 = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DesertCore", 1);
			recipe.AddIngredient(ItemID.AdamantiteBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DesertCore", 1);
			recipe.AddIngredient(ItemID.TitaniumBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}