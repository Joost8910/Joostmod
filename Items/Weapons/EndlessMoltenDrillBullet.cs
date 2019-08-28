using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessMoltenDrillBullet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Molten Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" + 
                "100% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.ranged = true;
            item.damage = 10;
            item.width = 26;
            item.height = 32;
            item.consumable = false;
            item.knockBack = 0;
            item.value = 80000;
            item.rare = 2;
            item.shoot = mod.ProjectileType("MoltenDrillBullet");
            item.shootSpeed = 3.5f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MoltenDrillBullet", 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EndlessDrillBullet");
            recipe.AddIngredient(ItemID.HellstoneBar, 40);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}

