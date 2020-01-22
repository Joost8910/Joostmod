using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
	public class ElementalSet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Elemental Weapon Set");
			Tooltip.SetDefault("'Unleash the elements!'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 16));
		}
		public override void SetDefaults()
		{
			item.damage = 48;
			item.thrown = true;
			item.width = 46;
			item.height = 64;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 7;
			item.value = 900000;
			item.rare = 5;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Boomerain");
			item.shootSpeed = 7.5f;

		}
		public override bool CanUseItem(Player player)      
        {
            return ((player.ownedProjectileCounts[mod.ProjectileType("EarthenHammer")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave1")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave2")] <= 0) || (player.ownedProjectileCounts[mod.ProjectileType("GaleBoomerang")] <= 0) || (player.ownedProjectileCounts[mod.ProjectileType("Boomerain")] <= 0) || (player.ownedProjectileCounts[mod.ProjectileType("InfernalChakram")] + player.ownedProjectileCounts[mod.ProjectileType("DousedChakram")] <= 0));
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			 if (player.ownedProjectileCounts[mod.ProjectileType("EarthenHammer")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave1")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave2")] <= 0)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 1.2f), (speedY * 1.2f), mod.ProjectileType("EarthenHammer"), (int)(damage * 1.44f), (knockBack * 2.16f), player.whoAmI);
			}
			else if (player.ownedProjectileCounts[mod.ProjectileType("GaleBoomerang")] <= 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("GaleBoomerang"), damage, knockBack, player.whoAmI);
			}
			else if (player.ownedProjectileCounts[mod.ProjectileType("Boomerain")] <= 0)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 2), (speedY * 2), mod.ProjectileType("Boomerain"), (int)(damage * 0.65f), knockBack * 1.16f, player.whoAmI);
			}
			else if (player.ownedProjectileCounts[mod.ProjectileType("InfernalChakram")] + player.ownedProjectileCounts[mod.ProjectileType("DousedChakram")] <= 0)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 1.4f), (speedY * 1.4f), mod.ProjectileType("InfernalChakram"), (int)(damage * 0.75f), knockBack, player.whoAmI);
			}
			return false;
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EarthenHammer");
			recipe.AddIngredient(null, "InfernalChakram");
			recipe.AddIngredient(null, "GaleBoomerang");
			recipe.AddIngredient(null, "Boomerain");
			recipe.AddTile(null, "ElementalForge"); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}


