using Terraria;
using Terraria.ModLoader;
 
namespace JoostMod.Items.Placeable         
{
    public class DeathKnightBanner : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death Knight Banner");
		}
        public override void SetDefaults()
        {  
            item.width = 12;    
            item.height = 28;  
            item.maxStack = 99; 
            item.useTurn = true;
            item.autoReuse = true; 
            item.useAnimation = 15;  
            item.useTime = 10;  
            item.useStyle = 1;  
            item.consumable = true;  
            item.rare = 2; 
            item.value = Item.buyPrice(1, 0, 0, 0);  
            item.createTile = mod.TileType("DeathKnightBanner");  
            item.placeStyle = 0;
        }
    }
}
////then add this to the custom npc you want to drop the banner and in public override void SetDefaults()
/*  banner = npc.type;
			bannerItem = mod.ItemType("DeathKnightBanner"); //this defines what banner this npc will drop       */