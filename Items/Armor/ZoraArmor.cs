using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ZoraArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zora Armor");
            Tooltip.SetDefault("20% increased magic damage");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 300000;
            item.rare = 5;
            item.defense = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 8);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 8);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}