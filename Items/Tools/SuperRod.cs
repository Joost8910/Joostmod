using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools
{
	public class SuperRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Rod");
		}
		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 32;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("SuperFishHook2").Type;
			Item.shootSpeed = 18f;
			Item.fishingPole = 60;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.WaterEssence>(25)
				.AddIngredient(ItemID.GoldenFishingRod, 1)
				.AddTile<Tiles.ElementalForge>()
				.Register();
		}
	}
}

