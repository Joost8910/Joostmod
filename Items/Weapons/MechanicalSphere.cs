using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class MechanicalSphere : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Sphere");
			Tooltip.SetDefault("'Unleash mechanical power'");
		}
		public override void SetDefaults()
		{
			item.damage = 40;
			item.thrown = true;
			item.width = 34;
			item.height = 36;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 0;
			item.rare = 5;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MechanicalSphere");
			item.shootSpeed = 6f;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.SoulofSight, 7);
			recipe.AddIngredient(ItemID.SoulofMight, 7);
			recipe.AddIngredient(ItemID.SoulofFright, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

