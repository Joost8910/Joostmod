using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class WaterBalloon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Balloon");
			Tooltip.SetDefault("'Splash!'");
		}
		public override void SetDefaults()
		{
			item.damage = 44;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 22;
			item.height = 28;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 6;
			item.value = 1000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("WaterBalloon");
			item.shootSpeed = 11f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SillyBalloonPink, 50);
			recipe.AddIngredient(null, "WaterEssence");
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}

	}
}

