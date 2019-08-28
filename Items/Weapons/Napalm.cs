using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Napalm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Napalm");
			Tooltip.SetDefault("'Fiery'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 10;
			item.width = 26;
			item.height = 26;
			item.consumable = true;
			item.knockBack = 1f;
			item.value = 120;
			item.rare = 3;
			item.shoot = mod.ProjectileType("Napalm");
			item.ammo = item.type;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FireEssence", 1);
			recipe.AddIngredient(ItemID.Bomb, 11);
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this, 111);
			recipe.AddRecipe();
		}
	}
}

