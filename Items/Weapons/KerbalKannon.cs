using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class KerbalKannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kerbal Kannon");
			Tooltip.SetDefault("Launches Kerbals at high velocity");
		}
		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 40;
			Item.noMelee = true;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Kerbal").Type;
			Item.shootSpeed = 15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MeteoriteBar, 10)
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}

