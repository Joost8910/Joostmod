using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.GrappleHooks
{
    public class SwingyHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.shootSpeed = 11f; // how quickly the hook is shot.
            Item.shoot = Mod.Find<ModProjectile>("SwingyHook").Type;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Swingy Hook");
			Tooltip.SetDefault("Move left or right to swing\n" + "Move up or down to retract/extend");
		}
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
                .AddIngredient(ItemID.Hook)
                .AddIngredient(ItemID.RopeCoil)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}