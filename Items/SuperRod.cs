using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class SuperRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Rod");
		}
		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 32;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 1;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("SuperFishHook2");
			item.shootSpeed = 18f;
			item.fishingPole = 60;
		}

		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "WaterEssence", 25);
			recipe.AddIngredient(ItemID.GoldenFishingRod, 1);
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();



		}
 
	}
}

