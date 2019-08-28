using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class CactuarPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Pants");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.vanity = true;
			item.value = 10000;
			item.rare = 2;
		}
		public override bool DrawLegs()
		{
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}