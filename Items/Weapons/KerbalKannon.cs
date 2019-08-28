using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class KerbalKannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kerbal Kannon");
			Tooltip.SetDefault("Launches Kerbals at high velocity");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.ranged = true;
			item.width = 52;
			item.height = 40;
			item.noMelee = true;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 5;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = 1;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Kerbal");
			item.shootSpeed = 15f;
		}

		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}

