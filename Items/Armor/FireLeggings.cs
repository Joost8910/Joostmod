using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FireLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoldering Leggings");
            Tooltip.SetDefault("10% increased movement speed");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 250000;
            item.rare = 5;
            item.defense = 12;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 6);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 6);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 6);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}