using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessShroomiteBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Shroomite Pouch");
			Tooltip.SetDefault("Leaves a trail of damaging mushrooms");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 16;
			Item.width = 26;
			Item.height = 32;
			Item.consumable = false;
			Item.knockBack = 5f;
			Item.value = 3000000;
			Item.rare = ItemRarityID.Lime;
			Item.shoot = Mod.Find<ModProjectile>("ShroomBullet").Type;
			Item.shootSpeed = 5f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ShroomiteBullet>(3996)
				.AddTile(TileID.CrystalBall)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.EndlessMusketPouch)
				.AddIngredient(ItemID.ShroomiteBar, 57)
				.AddTile(TileID.Autohammer)
				.Register();
		}
	}
}

