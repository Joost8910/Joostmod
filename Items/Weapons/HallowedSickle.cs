using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HallowedSickle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Sickle");
			Tooltip.SetDefault("'Swish and flick!'");
		}
		public override void SetDefaults()
		{
			item.damage = 54;
			item.thrown = true;
			item.consumable = true;
			item.maxStack = 999;
			item.width = 50;
			item.height = 50;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 200;
			item.rare = 6;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("HallowedSickle");
			item.shootSpeed = 0.3f;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 111);
			recipe.AddRecipe();
		}
	}
}

