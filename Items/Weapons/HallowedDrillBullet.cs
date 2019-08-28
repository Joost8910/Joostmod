using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HallowedDrillBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Drill Bullet");
			Tooltip.SetDefault("Breaks struck tiles\n" + 
                "Does little but rapid damage\n" + 
                "200% Pickaxe Power");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.ranged = true;
			item.damage = 12;
			item.width = 10;
			item.height = 10;
			item.consumable = true;
			item.knockBack = 0;
			item.value = 30;
			item.rare = 2;
			item.shoot = mod.ProjectileType("HallowedDrillBullet");
			item.shootSpeed = 4f;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar);
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofSight);
            recipe.AddIngredient(ItemID.SoulofFright);
            recipe.AddIngredient(null, "MoltenDrillBullet", 400);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this, 400);
			recipe.AddRecipe();
		}
	}
}

