using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable         
{
    public class TreasureGoblinBanner : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Goblin Banner");
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
            Item.value = Item.buyPrice(20, 0, 0, 0);  
            Item.createTile = ModContent.TileType<Tiles.Banners.TreasureGoblinBanner>();  
            Item.placeStyle = 0;
        }
    }
}
////then add this to the custom npc you want to drop the banner and in public override void SetDefaults()
/*  banner = npc.type;
			bannerItem = mod.ItemType("TreasureGoblinBanner"); //this defines what banner this npc will drop       */