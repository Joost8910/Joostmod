using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class EarthMount : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Platforms");
			Tooltip.SetDefault("Summons rideable stone platforms");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 225000;
			item.rare = 2;
			item.UseSound = SoundID.DD2_MonkStaffGroundMiss;
			item.noMelee = true;
			item.mountType = mod.MountType("EarthMount");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthEssence", 50);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 6);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 6);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 6);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}