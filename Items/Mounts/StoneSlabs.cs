using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Mounts;

namespace JoostMod.Items.Mounts
{
	public class StoneSlabs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slabs of Stone");
			Tooltip.SetDefault("Summons rideable stone slabs");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 225000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.DD2_MonkStaffGroundMiss;
			Item.noMelee = true;
			Item.mountType = ModContent.MountType<EarthMount>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient<Materials.EarthEssence>(50)
				.AddIngredient(ItemID.StoneBlock, 50)
				.AddRecipeGroup("JoostMod:AnyCobalt", 5)
				.AddRecipeGroup("JoostMod:AnyMythril", 5)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 5)
				.AddTile<Tiles.ElementalForge>()
				.Register();
        }
    }
}