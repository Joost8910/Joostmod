using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 12;
			Item.width = 10;
			Item.height = 10;
			Item.consumable = true;
			Item.knockBack = 0;
			Item.value = 30;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("HallowedDrillBullet").Type;
			Item.shootSpeed = 4f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(400)
				.AddIngredient(ItemID.HallowedBar)
				.AddIngredient(ItemID.SoulofMight)
				.AddIngredient(ItemID.SoulofSight)
				.AddIngredient(ItemID.SoulofFright)
				.AddIngredient<MoltenDrillBullet>(400)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

