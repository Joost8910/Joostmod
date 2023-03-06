using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 8;
			Item.width = 10;
			Item.height = 10;
			Item.consumable = true;
			Item.knockBack = 0;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("DrillBullet").Type;
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(100)
				.AddRecipeGroup("IronBar")
				.AddIngredient(ItemID.Wire)
				.AddIngredient(ItemID.MusketBall, 100)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}

