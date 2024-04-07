using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.GrappleHooks
{
    public class EnchantedSwingyHook : ModItem
    {
        public override void SetDefaults()
        {
            //clone and modify the ones we want to copy
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 12.5f; // how quickly the hook is shot.
            Item.shoot = ModContent.ProjectileType<Projectiles.Grappling.EnchantedSwingyHook>();
        }
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Swingy Hook");
			Tooltip.SetDefault("Swings faster and extends longer than before!");
		}
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
                .AddIngredient<SwingyHook>()
                .AddIngredient(ItemID.FallenStar, 10)
                .AddIngredient(ItemID.SilkRopeCoil)
                .AddIngredient(ItemID.Diamond)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}