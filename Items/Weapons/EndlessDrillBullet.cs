using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessDrillBullet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" + 
                "50% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.ranged = true;
            item.damage = 8;
            item.width = 26;
            item.height = 32;
            item.consumable = false;
            item.knockBack = 0;
            item.value = 40000;
            item.rare = 2;
            item.shoot = mod.ProjectileType("DrillBullet");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Bullet;
        }
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DrillBullet", 3996);
			recipe.AddTile(TileID.CrystalBall); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

