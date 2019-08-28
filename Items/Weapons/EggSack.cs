using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class EggSack : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Egg Sack");
			Tooltip.SetDefault("'SPIDERS!'");
		}
		public override void SetDefaults()
		{
			item.damage = 40;
			item.melee = true;
			item.width = 30;
			item.height = 26;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.knockBack = 2;
			item.channel = true;
			item.value = 50000;
			item.rare = 5;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("EggSack");
			item.shootSpeed = 16f;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpiderFang, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

