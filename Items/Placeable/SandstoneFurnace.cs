using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.Placeable         
{
    public class SandstoneFurnace : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstone Furnace");
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
            Item.createTile = Mod.Find<ModTile>("SandstoneFurnace").Type;  
            Item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
.AddIngredient(ItemID.Sandstone, 20)
.AddRecipeGroup("Wood", 4)
.AddIngredient(ItemID.Torch, 3)
.AddTile(TileID.WorkBenches)
.Register();
        }
    }
}
