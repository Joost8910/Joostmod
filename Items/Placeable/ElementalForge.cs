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
            Item.width = 48;    
            Item.height = 34;  
            Item.maxStack = 99; 
            Item.useTurn = true;
            Item.autoReuse = true; 
            Item.useAnimation = 15;  
            Item.useTime = 10;  
            Item.useStyle = ItemUseStyleID.Swing;  
            Item.consumable = true;  
            Item.rare = ItemRarityID.Orange; 
            Item.value = Item.buyPrice(0, 10, 0, 0);  
            Item.createTile = Mod.Find<ModTile>("ElementalForge").Type;  
            Item.placeStyle = 0;
        }
        public override void AddRecipes()
		{
				CreateRecipe()
.AddIngredient<Materials.WaterEssence>(25)
.AddIngredient<Materials.EarthEssence>(), 25)
.AddIngredient<FireEssence", 25)
.AddIngredient<Materials.TinyTwister>(), 25)
.AddIngredient(ItemID.MythrilAnvil)
.AddIngredient(ItemID.AdamantiteForge)
.AddTile(TileID.WorkBenches)
.Register();
CreateRecipe()
.AddIngredient<Materials.WaterEssence>(25)
.AddIngredient<Materials.EarthEssence>(), 25)
.AddIngredient<FireEssence", 25)
.AddIngredient<Materials.TinyTwister>(), 25)
.AddIngredient(ItemID.OrichalcumAnvil)
.AddIngredient(ItemID.AdamantiteForge)
.AddTile(TileID.WorkBenches)
.Register();
CreateRecipe()
.AddIngredient<Materials.WaterEssence>(25)
.AddIngredient<Materials.EarthEssence>(), 25)
.AddIngredient<FireEssence", 25)
.AddIngredient<Materials.TinyTwister>(), 25)
.AddIngredient(ItemID.MythrilAnvil)
.AddIngredient(ItemID.TitaniumForge)
.AddTile(TileID.WorkBenches)
.Register();
CreateRecipe()
.AddIngredient<Materials.WaterEssence>(25)
.AddIngredient<Materials.EarthEssence>(), 25)
.AddIngredient<FireEssence", 25)
.AddIngredient<Materials.TinyTwister>(), 25)
.AddIngredient(ItemID.OrichalcumAnvil)
.AddIngredient(ItemID.TitaniumForge)
.AddTile(TileID.WorkBenches)
.Register();

		}
    }
}
