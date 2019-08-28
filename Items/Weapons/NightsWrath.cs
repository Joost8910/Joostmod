using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class NightsWrath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night's Wrath");
		}
		public override void SetDefaults()
		{
			item.damage = 45;
			item.melee = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 10;
			item.useAnimation = 30;
			item.knockBack = 7;
			item.value = 54000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.hammer = 80;
			item.useStyle = 1;
			item.tileBoost = 1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MoltenHamaxe);
			recipe.AddIngredient(null, "AquaHammer");
			recipe.AddIngredient(null, "JungleHammer");
			recipe.AddIngredient(ItemID.TheBreaker);
			recipe.AddTile(TileID.DemonAltar); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


