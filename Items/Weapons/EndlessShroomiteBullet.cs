using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessShroomiteBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Shroomite Pouch");
			Tooltip.SetDefault("Leaves a trail of damaging mushrooms");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.ranged = true;
			item.damage = 16;
			item.width = 26;
			item.height = 32;
			item.consumable = false;
			item.knockBack = 5f;
			item.value = 3000000;
			item.rare = 7;
			item.shoot = mod.ProjectileType("ShroomBullet");
			item.shootSpeed = 5f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ShroomiteBullet", 3996);
			recipe.AddTile(TileID.CrystalBall); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

