using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Balancerang : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Balancerang");
			Tooltip.SetDefault("'Find your inner pieces'");
		}
		public override void SetDefaults()
		{
			Item.damage = 51;
			Item.DamageType = DamageClass.Throwing;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 39;
			Item.useAnimation = 39;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 144000;
			Item.rare = ItemRarityID.Pink;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Balancerang").Type;
			Item.shootSpeed = 16f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LightShard, 1)
				.AddIngredient(ItemID.DarkShard, 1)
				.AddIngredient(ItemID.SoulofLight, 7)
				.AddIngredient(ItemID.SoulofNight, 7)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

