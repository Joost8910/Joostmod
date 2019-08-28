using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Tornade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornade");
			Tooltip.SetDefault("Releases a miniature tornado on impact");
		}
		public override void SetDefaults()
		{
			item.damage = 42;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 16;
			item.height = 22;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 4;
			item.value = 500;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Tornade");
			item.shootSpeed = 10f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Grenade, 50);
			recipe.AddIngredient(null, "TinyTwister");
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}

	}
}

