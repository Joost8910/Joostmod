using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Rock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rock");
			Tooltip.SetDefault("'Sticks and stones may break my bones and give me a concussion'");
		}
		public override void SetDefaults()
		{
			item.damage = 165;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 22;
			item.height = 22;
			item.useTime = 33;
			item.useAnimation = 33;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 10;
			item.value = 300;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Rock");
			item.shootSpeed = 10f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 50);
			recipe.AddIngredient(null, "EarthEssence");
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}

	}
}

