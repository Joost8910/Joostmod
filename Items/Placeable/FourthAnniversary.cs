using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
    public class FourthAnniversary : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Joostmod's Fourth Anniversary");
            Tooltip.SetDefault("'The Journey may End, but the legend never dies'");
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
            item.createTile = mod.TileType("FourthAnniversary");
            item.placeStyle = 0;
        }
    }
}