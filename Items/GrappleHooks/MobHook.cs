using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.Items.GrappleHooks
{
    public class MobHook : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 11f;
            Item.shoot = Mod.Find<ModProjectile>("MobHook").Type;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grabby Swingy Hook");
			Tooltip.SetDefault("Grabs onto enemies!");
		}
        public override void AddRecipes() 
        {
            CreateRecipe()
                .AddIngredient<SwingyHook>()
                .AddIngredient(ItemID.Ruby)
                .AddIngredient(ItemID.Sapphire)
                .AddIngredient(ItemID.Emerald)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}