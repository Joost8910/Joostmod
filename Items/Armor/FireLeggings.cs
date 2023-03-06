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
            Item.width = 22;
            Item.height = 18;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 12;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.FireEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 6)
                .AddRecipeGroup("JoostMod:AnyMythril", 6)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 6)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}