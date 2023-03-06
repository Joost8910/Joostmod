using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 10;
			Item.width = 10;
			Item.height = 10;
			Item.consumable = true;
			Item.knockBack = 0;
			Item.value = 20;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("MoltenDrillBullet").Type;
			Item.shootSpeed = 3.5f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(100)
				.AddIngredient(ItemID.HellstoneBar)
				.AddIngredient<DrillBullet>(100)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}

