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
            Tooltip.SetDefault("6% increased melee damage\n" +
                "10% reduced movement speed");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 22;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.06f;
            player.moveSpeed *= 0.9f;
            player.maxRunSpeed *= 0.9f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.9f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 150)
                .AddRecipeGroup("JoostMod:AnyCobalt", 6)
                .AddRecipeGroup("JoostMod:AnyMythril", 6)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 6)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}