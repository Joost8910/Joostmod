using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class FierySoles : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Soles");
			Tooltip.SetDefault("Summons fire from your feet");
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
			Item.UseSound = SoundID.Item20;
			Item.noMelee = true;
			Item.mountType = Mod.Find<ModMount>("FierySoles").Type;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient<Materials.FireEssence>(50)
				.AddRecipeGroup("JoostMod:AnyCobalt", 5)
				.AddRecipeGroup("JoostMod:AnyMythril", 5)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 5)
				.AddTile<Tiles.ElementalForge>()
				.Register();
        }
    }
}