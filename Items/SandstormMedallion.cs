using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
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
			item.width = 32;
			item.height = 32;
            item.value = 225000;
            item.rare = 5;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thrownDamage += 0.15f;
            if (player.ZoneSandstorm)
            {
                player.moveSpeed *= 1.15f;
                player.maxRunSpeed *= 1.15f;
                player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.15f;
                player.thrownVelocity += 0.15f;
                player.GetModPlayer<JoostModPlayer>().throwConsume *= 0.85f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}