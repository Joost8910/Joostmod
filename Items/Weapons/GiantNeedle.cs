using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class GiantNeedle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant Needle");
			Tooltip.SetDefault("'Better than a sharp stick in the eye.'\n" + "'No, definitely worse...'");
		}
		public override void SetDefaults()
		{
			item.damage = 280;
			item.thrown = true;
			item.width = 26;
			item.height = 72;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 7;
			item.value = 10000000;
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GiantNeedle");
			item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
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

