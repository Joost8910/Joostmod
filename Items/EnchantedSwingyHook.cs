using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items
{
    public class EnchantedSwingyHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            item.CloneDefaults(ItemID.AmethystHook);
            item.rare = 2;
            item.shootSpeed = 18f; // how quickly the hook is shot.
            item.shoot = mod.ProjectileType("EnchantedSwingyHook");
        }
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Swingy Hook");
			Tooltip.SetDefault("Swings faster and extends longer than before!");
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient(null, "SwingyHook");
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.SilkRopeCoil); 
            recipe.AddIngredient(ItemID.Diamond);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}