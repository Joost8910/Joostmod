using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessLaserDrillBullet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Laser Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" +
                "Always shoots straight towards your cursor\n" +
                "230% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.ranged = true;
            item.damage = 15;
            item.width = 26;
            item.height = 32;
            item.consumable = false;
            item.knockBack = 0;
            item.value = 160000;
            item.rare = 2;
            item.shoot = mod.ProjectileType("LaserDrillBullet");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LaserDrillBullet", 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EndlessHallowedDrillBullet");
            recipe.AddIngredient(ItemID.MartianConduitPlating, 80);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}

