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
            ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 10;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 80;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 6)
                .AddRecipeGroup("JoostMod:AnyMythril", 6)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 6)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}