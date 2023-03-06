using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
{
	public class BoomerangBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomerang Bullet");
			Tooltip.SetDefault("'It really works!'\n" + "Has a 10% chance of returning to you");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 8;
			Item.width = 26;
			Item.height = 26;
			Item.consumable = true;
			Item.knockBack = 3f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("BoomerangBullet").Type;
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.JungleSpores)
				.AddIngredient(ItemID.MusketBall, 50)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}

