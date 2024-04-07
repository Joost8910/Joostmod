using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class PinkSlimeCoat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Slime Coat");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 7;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PinkSlimeBlock, 30)
                .AddTile(TileID.Solidifier)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.PinkGel, 30)
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
}