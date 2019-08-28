using Terraria;
using Terraria.ModLoader;
 
namespace JoostMod.Items.Placeable         
{
    public class CactoidBanner : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactoid Banner");
		}
        public override void SetDefaults()
        {
            item.width = 10;    
            item.height = 24;  
            item.maxStack = 99; 
            item.useTurn = true;
            item.autoReuse = true; 
            item.useAnimation = 15;  
            item.useTime = 10;  
            item.useStyle = 1;  
            item.consumable = true;  
            item.rare = 2; 
            item.value = Item.buyPrice(0, 0, 50, 0);  
            item.createTile = mod.TileType("CactoidBanner");  
            item.placeStyle = 0;
        }
    }
}
////then add this to the custom npc you want to drop the banner and in public override void SetDefaults()
/*  banner = npc.type;
  bannerItem = mod.ItemType("CustomBannerItem"); //this defines what banner this npc will drop       */