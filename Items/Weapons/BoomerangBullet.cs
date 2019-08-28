using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class BoomerangBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomerang Bullet");
			Tooltip.SetDefault("'It really works!'\n" + "Has a 10% chance of returning to you");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 8;
			item.width = 26;
			item.height = 26;
			item.consumable = true;
			item.knockBack = 3f;
			item.value = 10;
			item.rare = 2;
			item.shoot = mod.ProjectileType("BoomerangBullet");
			item.shootSpeed = 3f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleSpores);
			recipe.AddIngredient(ItemID.MusketBall, 50);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}

