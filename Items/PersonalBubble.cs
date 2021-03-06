using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class PersonalBubble : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Personal Bubble");
			Tooltip.SetDefault("You always count as being in water\n" +
                "Your magic attacks deal 15% more damage to submerged enemies\n" +
                "Pushes back enemies; hide visual to disable");
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
            player.GetModPlayer<JoostPlayer>().waterBubble = true;
            player.GetModPlayer<JoostPlayer>().hideBubble = hideVisual;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BubbleShield");
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 3);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 3);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 3);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}