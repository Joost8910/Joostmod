using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
    public class FrostFurnace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Furnace");
            Tooltip.SetDefault("Used for smelting ore");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 3, 0);
            Item.createTile = ModContent.TileType<Tiles.FrostFurnace>();
            Item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
.AddIngredient(ItemID.IceBrick, 10)
.AddRecipeGroup("Wood", 4)
.AddIngredient(ItemID.IceTorch, 3)
.AddTile(TileID.WorkBenches)
.Register();
        }
    }
}
