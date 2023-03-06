using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessRichoshot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Richoshot Pouch");
			Tooltip.SetDefault("'Bouncy!'");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 1;
			Item.width = 26;
			Item.height = 32;
			Item.consumable = false;
			Item.knockBack = 5f;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("Richoshot").Type;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Richoshot>(3996)
				.AddTile(TileID.CrystalBall)
				.Register();
		}
	}
}

