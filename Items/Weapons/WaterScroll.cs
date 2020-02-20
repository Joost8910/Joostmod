using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class WaterScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scroll of Water");
            Tooltip.SetDefault("Creates a controllable ball of water\n" + "Collects nearby water to grow");
        }
        public override void SetDefaults()
        {
            item.damage = 36;
            item.magic = true;
            item.width = 36;
            item.height = 36;
            item.mana = 20;
            item.channel = true;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.reuseDelay = 2;
            item.value = 225000;
            item.rare = 5;
            item.knockBack = 3;
            item.UseSound = SoundID.Item21;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WaterBall");
            item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}


