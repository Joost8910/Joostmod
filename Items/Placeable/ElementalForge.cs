using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.Placeable         
{
    public class ElementalForge : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elemental Forge");
            Tooltip.SetDefault("Used to make items from Elemental Essences\n" + "Functions as both an Adamantite forge and a Mythril Anvil");
		}
        public override void SetDefaults()
        {
            item.width = 48;    
            item.height = 34;  
            item.maxStack = 99; 
            item.useTurn = true;
            item.autoReuse = true; 
            item.useAnimation = 15;  
            item.useTime = 10;  
            item.useStyle = 1;  
            item.consumable = true;  
            item.rare = 3; 
            item.value = Item.buyPrice(0, 10, 0, 0);  
            item.createTile = mod.TileType("ElementalForge");  
            item.placeStyle = 0;
        }
        public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "WaterEssence", 25);
			recipe.AddIngredient(null, "EarthEssence", 25);
            recipe.AddIngredient(null, "FireEssence", 25);
            recipe.AddIngredient(null, "TinyTwister", 25);
            recipe.AddIngredient(ItemID.MythrilAnvil);
            recipe.AddIngredient(ItemID.AdamantiteForge);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
				recipe.AddRecipe();
                recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "WaterEssence", 25);
			recipe.AddIngredient(null, "EarthEssence", 25);
            recipe.AddIngredient(null, "FireEssence", 25);
            recipe.AddIngredient(null, "TinyTwister", 25);
            recipe.AddIngredient(ItemID.OrichalcumAnvil);
            recipe.AddIngredient(ItemID.AdamantiteForge);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
				recipe.AddRecipe();
                recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "WaterEssence", 25);
			recipe.AddIngredient(null, "EarthEssence", 25);
            recipe.AddIngredient(null, "FireEssence", 25);
            recipe.AddIngredient(null, "TinyTwister", 25);
            recipe.AddIngredient(ItemID.MythrilAnvil);
            recipe.AddIngredient(ItemID.TitaniumForge);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
				recipe.AddRecipe();
                recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "WaterEssence", 25);
			recipe.AddIngredient(null, "EarthEssence", 25);
            recipe.AddIngredient(null, "FireEssence", 25);
            recipe.AddIngredient(null, "TinyTwister", 25);
            recipe.AddIngredient(ItemID.OrichalcumAnvil);
            recipe.AddIngredient(ItemID.TitaniumForge);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
				recipe.AddRecipe();

		}
    }
}
