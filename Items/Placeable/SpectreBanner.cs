using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable         
{
    public class SpectreBanner : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spectre Banner");
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
            Item.value = Item.buyPrice(2, 0, 0, 0);  
            Item.createTile = Mod.Find<ModTile>("SpectreBanner").Type;  
            Item.placeStyle = 0;
        }
    }
}
////then add this to the custom npc you want to drop the banner and in public override void SetDefaults()
/*  banner = npc.type;
			bannerItem = mod.ItemType("SpectreBanner"); //this defines what banner this npc will drop       */