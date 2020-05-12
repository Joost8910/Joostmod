using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    [AutoloadEquip(EquipType.Neck)]
    public class AirMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gust Amulet");
			Tooltip.SetDefault("Increases movement speed by 10%\n" +
                "Your summon attacks have a 10% chance to create a gust of wind");
		}
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 24;
            item.value = 225000;
            item.rare = 5;
			item.accessory = true;
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.moveSpeed *= 1.1f;
            player.maxRunSpeed *= 1.1f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.1f;
            player.GetModPlayer<JoostPlayer>().airMedallion = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TinyTwister", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 3);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 3);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 3);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}