using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class BloodBreaker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Breaker");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 12;
			item.useAnimation = 36;
			item.knockBack = 7;
			item.value = 54000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.hammer = 80;
			item.tileBoost = 1;
			item.useStyle = 1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MoltenHamaxe);
			recipe.AddIngredient(null, "AquaHammer");
			recipe.AddIngredient(null, "JungleHammer");
			recipe.AddIngredient(ItemID.FleshGrinder);
			recipe.AddTile(TileID.DemonAltar); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


