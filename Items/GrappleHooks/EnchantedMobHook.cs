using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.GrappleHooks
{
    public class EnchantedMobHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 12.5f; // how quickly the hook is shot.
            Item.shoot = Mod.Find<ModProjectile>("EnchantedMobHook").Type;
        }
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Grabby Swingy Hook");
			Tooltip.SetDefault("Swings faster and extends longer than before!\n" + 
                "Grabs onto enemies!");
		}
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
                .AddIngredient<EnchantedSwingyHook>()
                .AddIngredient<MobHook>()
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}