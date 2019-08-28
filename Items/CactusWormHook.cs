using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items
{
    public class CactusWormHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            item.CloneDefaults(ItemID.AmethystHook);
            item.shootSpeed = 12f; // how quickly the hook is shot.
            item.shoot = mod.ProjectileType("CactusHook");
            item.rare = -12;
            item.expert = true;
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