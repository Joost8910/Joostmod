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
            Item.width = 30;
            Item.height = 18;
            Item.value = 300000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.20f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 8)
                .AddRecipeGroup("JoostMod:AnyMythril", 8)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 8)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}