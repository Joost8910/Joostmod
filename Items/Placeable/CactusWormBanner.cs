using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.Placeable         
{
    public class CactusWormBanner : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm Banner");
		}
        public override void SetDefaults()
        {
            Item.width = 10;    
            Item.height = 24;  
            Item.maxStack = 99; 
            Item.useTurn = true;
            Item.autoReuse = true; 
            Item.useAnimation = 15;  
            Item.useTime = 10;  
            Item.useStyle = ItemUseStyleID.Swing;  
            Item.consumable = true;  
            Item.rare = ItemRarityID.Green; 
            Item.value = Item.buyPrice(0, 0, 50, 0);  
            Item.createTile = ModContent.TileType<Tiles.Banners.CactusWormBanner>();  
            Item.placeStyle = 0;
        }
    }
}