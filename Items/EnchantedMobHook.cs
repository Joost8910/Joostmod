using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items
{
    public class EnchantedMobHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            item.CloneDefaults(ItemID.AmethystHook);
            item.rare = 2;
            item.shootSpeed = 18f; // how quickly the hook is shot.
            item.shoot = mod.ProjectileType("EnchantedMobHook");
        }
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Grabby Swingy Hook");
			Tooltip.SetDefault("Swings faster and extends longer than before!\n" + 
                "Grabs onto enemies!");
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient(null, "EnchantedSwingyHook");
            recipe.AddIngredient(null, "MobHook");
            recipe.AddTile(TileID.TinkerersWorkbench);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}