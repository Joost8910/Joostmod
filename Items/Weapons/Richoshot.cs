using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Richoshot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Richoshot");
			Tooltip.SetDefault("'Bouncy!'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 1;
			item.width = 26;
			item.height = 26;
			item.consumable = true;
			item.knockBack = 5f;
			item.value = 5;
			item.rare = 2;
			item.shoot = mod.ProjectileType("Richoshot");
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PinkGel);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}

