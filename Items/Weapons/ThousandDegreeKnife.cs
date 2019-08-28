using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class ThousandDegreeKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("1000'C Degrees Knife");
			Tooltip.SetDefault("'That's 1832 degrees Fahrenheit");
		}
		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 20;
			item.height = 18;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 200000;
			item.rare = 10;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 8); 
			recipe.AddIngredient(null, "FireEssence", 30);
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

