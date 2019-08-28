using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class DrillBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Drill Bullet");
			Tooltip.SetDefault("Breaks struck tiles\n" + 
                "Does little but rapid damage\n" + 
                "50% Pickaxe Power");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 8;
			item.width = 10;
			item.height = 10;
			item.consumable = true;
			item.knockBack = 0;
			item.value = 10;
			item.rare = 2;
			item.shoot = mod.ProjectileType("DrillBullet");
			item.shootSpeed = 3f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar");
            recipe.AddIngredient(ItemID.Wire);
			recipe.AddIngredient(ItemID.MusketBall, 100);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}

