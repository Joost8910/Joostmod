using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
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
			item.width = 30;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 225000;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.noMelee = true;
			item.mountType = mod.MountType("FierySoles");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 5);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 5);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}