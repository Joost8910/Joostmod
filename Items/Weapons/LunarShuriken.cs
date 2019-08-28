using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class LunarShuriken : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Shuriken");
			Tooltip.SetDefault("'Unleash the power of the moon'");
		}
		public override void SetDefaults()
		{
			item.damage = 120;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 28;
			item.height = 28;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 5;
			item.value = 20000;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LunarShuriken");
			item.shootSpeed = 23f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 1); 
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 333);
			recipe.AddRecipe();
		}

	}
}

