using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class HavelLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havel's Leggings");
            Tooltip.SetDefault("5% increased melee damage\n" +
                "10% reduced movement speed");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 50000;
            item.rare = 5;
            item.defense = 22;
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.05f;
            player.moveSpeed *= 0.9f;
            player.maxRunSpeed *= 0.9f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.9f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthEssence", 50);
            recipe.AddIngredient(ItemID.StoneBlock, 250);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 6);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 6);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 6);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}