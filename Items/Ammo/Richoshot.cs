using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
{
	public class Richoshot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Richoshot");
			Tooltip.SetDefault("'Bouncy!'");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 1;
			Item.width = 26;
			Item.height = 26;
			Item.consumable = true;
			Item.knockBack = 5f;
			Item.value = 5;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Richoshot>();
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.PinkGel)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}

