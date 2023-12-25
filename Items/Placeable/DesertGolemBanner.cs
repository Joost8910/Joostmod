using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.Placeable         
{
    public class DesertGolemBanner : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Golem Banner");
		}
        public override void SetDefaults()
        {  
            Item.width = 12;    
            Item.height = 28;  
            Item.maxStack = 99; 
            Item.useTurn = true;
            Item.autoReuse = true; 
            Item.useAnimation = 15;  
            Item.useTime = 10;  
            Item.useStyle = ItemUseStyleID.Swing;  
            Item.consumable = true;  
            Item.rare = ItemRarityID.Green; 
            Item.value = Item.buyPrice(1, 0, 0, 0);  
            Item.createTile = ModContent.TileType<Tiles.Banners.DesertGolemBanner>();  
            Item.placeStyle = 0;
        }
    }
}
////then add this to the custom npc you want to drop the banner and in public override void SetDefaults()
/*  banner = npc.type;
			bannerItem = mod.ItemType("DesertGolemBanner"); //this defines what banner this npc will drop       */