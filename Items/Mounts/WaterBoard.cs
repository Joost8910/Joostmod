using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Mounts
{
	public class WaterBoard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Board");
			Tooltip.SetDefault("Summons a rideable board that floats");
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
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = Mod.Find<ModMount>("WaterBoard").Type;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient<Materials.WaterEssence>(50)
				.AddRecipeGroup("JoostMod:AnyCobalt", 5)
				.AddRecipeGroup("JoostMod:AnyMythril", 5)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 5)
				.AddTile<Tiles.ElementalForge>()
				.Register();
        }
    }
}