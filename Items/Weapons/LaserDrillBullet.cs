using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class LaserDrillBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Drill Bullet");
			Tooltip.SetDefault("Breaks struck tiles\n" + 
                "Does little but rapid damage\n" + 
                "Always shoots straight at your cursor\n" +
                "230% Pickaxe Power");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 15;
			item.width = 10;
			item.height = 10;
			item.consumable = true;
			item.knockBack = 0;
			item.value = 40;
			item.rare = 2;
			item.shoot = mod.ProjectileType("LaserDrillBullet");
			item.shootSpeed = 5f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MartianConduitPlating);
            recipe.AddIngredient(null, "HallowedDrillBullet", 50);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}

