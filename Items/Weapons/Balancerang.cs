using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Balancerang : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Balancerang");
			Tooltip.SetDefault("'Find your inner pieces'");
		}
		public override void SetDefaults()
		{
			item.damage = 51;
			item.thrown = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 39;
			item.useAnimation = 39;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = 144000;
			item.rare = 5;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Balancerang");
			item.shootSpeed = 16f;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LightShard, 1);
			recipe.AddIngredient(ItemID.DarkShard, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe.AddIngredient(ItemID.SoulofNight, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

