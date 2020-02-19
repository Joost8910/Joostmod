using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AirArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornado Plate");
            Tooltip.SetDefault("Increases your max number of minions");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 300000;
            item.rare = 5;
            item.defense = 10;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawArms = false;
            drawHands = false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TinyTwister", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 8);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 8);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}