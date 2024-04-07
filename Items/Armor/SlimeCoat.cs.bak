using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class SlimeCoat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Coat");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 90, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 5;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SlimeBlock, 30)
                .AddTile(TileID.Solidifier)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Gel, 30)
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
}