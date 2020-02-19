using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ZoraGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zora Greaves");
            Tooltip.SetDefault("Increases maximum mana by 80");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 250000;
            item.rare = 5;
            item.defense = 10;
        }
        public override bool DrawLegs()
        {
            return false;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 80;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 6);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 6);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 6);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}