using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
    public class ThirdAnniversary : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Joostmod's Third Anniversary");
            Tooltip.SetDefault("'Over 1000 days of Joost'");
        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 500000;
            item.rare = 8;
            item.createTile = mod.TileType("ThirdAnniversary");
            item.placeStyle = 0;
        }
    }
}