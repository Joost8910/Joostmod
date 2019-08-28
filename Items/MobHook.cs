using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items
{
    public class MobHook : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AmethystHook);
            item.rare = 2;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("MobHook");
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grabby Swingy Hook");
			Tooltip.SetDefault("Grabs onto enemies!");
		}
        public override void AddRecipes() 
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient(null, "SwingyHook");
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.Sapphire);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}