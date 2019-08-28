using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessRichoshot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Richoshot Pouch");
			Tooltip.SetDefault("'Bouncy!'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.ranged = true;
			item.damage = 1;
			item.width = 26;
			item.height = 32;
			item.consumable = false;
			item.knockBack = 5f;
			item.value = 20000;
			item.rare = 3;
			item.shoot = mod.ProjectileType("Richoshot");
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Richoshot", 3996);
			recipe.AddTile(TileID.CrystalBall); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

