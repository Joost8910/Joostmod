using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class AirScooter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Whirlwind Sphere");
			Tooltip.SetDefault("Summons a rideable ball of air");
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
			item.UseSound = SoundID.DD2_BookStaffCast;
			item.noMelee = true;
			item.mountType = mod.MountType("AirScooter");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TinyTwister", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 6);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 6);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 6);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}