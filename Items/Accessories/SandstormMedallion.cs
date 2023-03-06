using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class SandstormMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Medallion");
			Tooltip.SetDefault("15% Increased throwing damage\n" +
                "While in a sandstorm gain 15% increased movement speed,\n" +
                " throwing velocity, and chance to not consume thrown items");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) += 0.15f;
            if (player.ZoneSandstorm)
            {
                player.moveSpeed *= 1.15f;
                player.maxRunSpeed *= 1.15f;
                player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.15f;
                player.ThrownVelocity += 0.15f;
                player.GetModPlayer<JoostModPlayer>().throwConsume *= 0.85f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.DesertCore>()
                .AddRecipeGroup("JoostMod:AnyAdamantite", 9)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}