using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 15;
			Item.width = 10;
			Item.height = 10;
			Item.consumable = true;
			Item.knockBack = 0;
			Item.value = 40;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.LaserDrillBullet>();
			Item.shootSpeed = 5f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.MartianConduitPlating)
				.AddIngredient<HallowedDrillBullet>(50)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

