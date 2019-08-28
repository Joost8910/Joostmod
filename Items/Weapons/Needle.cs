using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Needle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Needle");
			Tooltip.SetDefault("'Better than a sharp stick in the eye.'\n" + "'Oh wait...'");
		}
		public override void SetDefaults()
		{
			item.damage = 1;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 2;
			item.height = 12;
			item.useTime = 4;
			item.useAnimation = 5;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 0;
			item.value = 0;
			item.rare = 0;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Needle");
			item.shootSpeed = 8f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus);
			recipe.SetResult(this, 60);
			recipe.AddRecipe();
		}
	}
}
