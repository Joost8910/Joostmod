using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TrueNightsFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Night's Fury");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1.1f;
			item.noUseGraphic = true;
			item.width = 54;
			item.height = 54;
			item.useTime = 44;
			item.useAnimation = 44;
			item.useStyle = 5;
			item.knockBack = 10;
			item.value = 100000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.channel = true;
            item.useTurn = true;
			item.shoot = mod.ProjectileType("TrueNightsFury");
			item.shootSpeed = 16f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "NightsFury");
			recipe.AddIngredient(null, "BrokenHeroFlail");
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

