using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Mustache : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Mustache");
			Tooltip.SetDefault("'From the face of the Jumbo Cactuar'");
		}
		public override void SetDefaults()
		{
			item.damage = 350;
			item.melee = true;
			item.width = 128;
			item.height = 128;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = 10000000;
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Splitend");
			item.shootSpeed = 32f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 2; 
			float rotation = MathHelper.ToRadians(12);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
        public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Cactustoken", 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

