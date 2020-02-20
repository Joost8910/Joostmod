using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	[AutoloadEquip(EquipType.Shoes)]
	public class BlazingAnklet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Anklets");
			Tooltip.SetDefault("Increases running speed by 25%\n" +
                "For every 10mph you're running at, ranged crit chance increases by 1%\n" +
                "Greatly increases running speed while on fire blocks\n" + 
                "Grants immunity to fire blocks");
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
            player.fireWalk = true;
            player.moveSpeed *= 1.25f;
            player.maxRunSpeed *= 1.25f;
            if (!player.mount.Active || player.mount._type == mod.MountType("FierySoles"))
                player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.25f;
            player.GetModPlayer<JoostPlayer>().blazeAnklet = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}