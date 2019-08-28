using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class UnicornLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Unicorn Lance");
			Tooltip.SetDefault("Does more damage the faster you are moving");
		}
		public override void SetDefaults()
		{
			item.damage = 42;
			item.melee = true;
			item.width = 50;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.autoReuse = true;
			item.reuseDelay = 10;
			item.knockBack = 7;
			item.value = 100000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("UnicornLance");
			item.shootSpeed = 18f;
		}


		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.UnicornHorn, 1);
			recipe.AddIngredient(ItemID.PixieDust, 10);
			recipe.AddIngredient(ItemID.Pearlwood, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

