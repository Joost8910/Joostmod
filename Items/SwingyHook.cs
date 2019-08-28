using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items
{
    public class SwingyHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            item.CloneDefaults(ItemID.AmethystHook);
            item.shootSpeed = 15f; // how quickly the hook is shot.
            item.shoot = mod.ProjectileType("SwingyHook");
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Swingy Hook");
			Tooltip.SetDefault("Move left or right to swing\n" + "Move up or down to retract/extend");
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient(ItemID.Hook);
            recipe.AddIngredient(ItemID.RopeCoil); 
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}