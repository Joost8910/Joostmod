using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class GnomeHat : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome Hat");
			Tooltip.SetDefault("'To show your loyalty to the Gnome God'");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 10000;
			item.rare = 4;
			item.vanity = true;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.RedDye);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}