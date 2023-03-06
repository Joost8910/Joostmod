using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.GrappleHooks
{
    public class CactusWormHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.shootSpeed = 10f; // how quickly the hook is shot.
            Item.shoot = Mod.Find<ModProjectile>("CactusHook").Type;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
        }
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm Hook");
			Tooltip.SetDefault("Hold the grapple button to have the hook go through tiles\n" + 
                "Let go of the grapple button to grip tile\n" +
                "Pulls you through tiles");
		}
    }
}