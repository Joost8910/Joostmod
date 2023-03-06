using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HallowedSickle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Sickle");
		}
		public override void SetDefaults()
		{
			Item.damage = 54;
			Item.DamageType = DamageClass.Throwing;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = 200;
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item1;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("HallowedSickle").Type;
			Item.shootSpeed = 0.3f;
		}
		public override void AddRecipes()
		{
			CreateRecipe(111)
				.AddIngredient(ItemID.HallowedBar)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

