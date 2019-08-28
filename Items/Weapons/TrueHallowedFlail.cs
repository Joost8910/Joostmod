using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TrueHallowedFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Incandescence");
		}
		public override void SetDefaults()
		{
			item.damage = 76;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1.1f;
			item.noUseGraphic = true;
			item.width = 42;
			item.height = 42;
			item.useTime = 44;
			item.useAnimation = 44;
			item.useStyle = 5;
			item.knockBack = 9;
			item.value = 100000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.channel = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("TrueHallowedFlail");
			item.shootSpeed = 21f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "HallowedFlail");
			recipe.AddIngredient(null, "BrokenHeroFlail");
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

