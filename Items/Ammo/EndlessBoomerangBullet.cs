using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessBoomerangBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Boomerang Bullet Pouch");
			Tooltip.SetDefault("'It really works!'\n" + "Has a 10% chance of returning to you");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 8;
			Item.width = 26;
			Item.height = 32;
			Item.consumable = false;
			Item.knockBack = 3f;
			Item.value = 40000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.EndlessBoomerangBullet>();
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<BoomerangBullet>(3996)
				.AddTile(TileID.CrystalBall)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.EndlessMusketPouch)
				.AddIngredient(ItemID.JungleSpores, 80)
				.Register();
		}
	}
}

