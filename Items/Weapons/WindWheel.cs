using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class WindWheel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hurricane Windwheel");
			Tooltip.SetDefault("Creates a swirling current of wind that damages enemies");
		}
		public override void SetDefaults()
		{
			item.damage = 25;
			item.summon = true;
			item.mana = 16;
			item.width = 54;
			item.height = 56;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
			item.knockBack = 0f;
            item.channel = true;
            item.noUseGraphic = true;
            item.value = 225000;
            item.rare = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Swirlwind");
			item.shootSpeed = 1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TinyTwister", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

