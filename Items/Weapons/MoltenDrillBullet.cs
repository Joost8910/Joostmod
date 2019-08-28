using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class MoltenDrillBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Molten Drill Bullet");
			Tooltip.SetDefault("Breaks struck tiles\n" + 
                "Does little but rapid damage\n" + 
                "100% Pickaxe Power");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 10;
			item.width = 10;
			item.height = 10;
			item.consumable = true;
			item.knockBack = 0;
			item.value = 20;
			item.rare = 2;
			item.shoot = mod.ProjectileType("MoltenDrillBullet");
			item.shootSpeed = 3.5f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar);
			recipe.AddIngredient(null, "DrillBullet", 100);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}

