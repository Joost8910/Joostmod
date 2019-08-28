using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessBoomerangBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Boomerang Bullet Pouch");
			Tooltip.SetDefault("'It really works!'\n" + "Has a 10% chance of returning to you");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.ranged = true;
			item.damage = 8;
			item.width = 26;
			item.height = 32;
			item.consumable = false;
			item.knockBack = 3f;
			item.value = 40000;
			item.rare = 3;
			item.shoot = mod.ProjectileType("EndlessBoomerangBullet");
			item.shootSpeed = 3f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "BoomerangBullet", 3996);
			recipe.AddTile(TileID.CrystalBall); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

