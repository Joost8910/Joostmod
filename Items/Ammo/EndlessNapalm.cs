using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessNapalm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Napalm Pouch");
			Tooltip.SetDefault("'Fiery'");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 10;
			Item.width = 26;
			Item.height = 32;
			Item.consumable = false;
			Item.knockBack = 1f;
			Item.value = 480000;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("Napalm").Type;
			Item.ammo = Mod.Find<ModItem>("Napalm").Type;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Napalm>(3996)
				.AddTile(TileID.CrystalBall)
				.Register();
		}
	}
}

