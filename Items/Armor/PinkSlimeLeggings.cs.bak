using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class PinkSlimeLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Slime Leggings");
            Tooltip.SetDefault("Slightly increased jump speed");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 5;
        }
        public override void UpdateEquip(Player player)
        {
            player.jumpSpeedBoost += 1.8f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PinkSlimeBlock, 25)
                .AddTile(TileID.Solidifier)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.PinkGel, 25)
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
}