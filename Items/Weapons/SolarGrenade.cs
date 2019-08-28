using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SolarGrenade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Grenade");
			Tooltip.SetDefault("Launches daybreaks upon impact");
		}
		public override void SetDefaults()
		{
			item.damage = 60;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 22;
			item.height = 26;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 5;
			item.value = 2000;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SolarGrenade");
			item.shootSpeed = 11f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentSolar, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 111);
			recipe.AddRecipe();
		}

	}
}

