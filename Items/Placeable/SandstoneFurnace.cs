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
            item.width = 30;    
            item.height = 26; 
            item.maxStack = 99; 
            item.useTurn = true;
            item.autoReuse = true; 
            item.useAnimation = 15;  
            item.useTime = 10;  
            item.useStyle = 1;  
            item.consumable = true;  
            item.rare = 1; 
            item.value = Item.buyPrice(0, 0, 3, 0);  
            item.createTile = mod.TileType("SandstoneFurnace");  
            item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sandstone, 20);
            recipe.AddRecipeGroup("Wood", 4);
            recipe.AddIngredient(ItemID.Torch, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
