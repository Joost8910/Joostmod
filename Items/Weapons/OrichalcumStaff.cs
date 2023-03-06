using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class OrichalcumStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Staff");
			Tooltip.SetDefault("Spins around you firing bolts of light and night");
		}
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Magic;
			Item.width = 54;
			Item.height = 54;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.reuseDelay = 5;
			Item.autoReuse = true;
			Item.mana = 5;
			Item.channel = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3;
			Item.value = 60000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item8;
			Item.shoot = Mod.Find<ModProjectile>("OrichalcumStaff").Type;
			Item.shootSpeed = 0;
			Item.useTurn = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.OrichalcumBar, 10)
				.AddIngredient(ItemID.SoulofLight, 5)
				.AddIngredient(ItemID.SoulofNight, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

	}
}

