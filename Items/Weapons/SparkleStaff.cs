using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SparkleStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle Staff");
			Tooltip.SetDefault("'Pretty, oh so pretty'");
		}
		public override void SetDefaults()
		{
			item.damage = 40;
			item.magic = true;
			item.width = 52;
			item.height = 52;
			item.mana = 24;
			item.useTime = 7;
			item.useAnimation = 28;
            item.reuseDelay = 10;
			Item.staff[item.type] = true; 
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 2;
			item.value = 230000;
			item.rare = 6;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Sparkle");
			item.shootSpeed = 2f;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 1; i <= 10; i++)
            {
                float spread = 15f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double baseAngle = Math.Atan2(speedX, speedY);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                speedX = baseSpeed * (float)Math.Sin(randomAngle);
                speedY = baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(position.X, position.Y, speedX * i, speedY * i, type, damage, knockBack, player.whoAmI);
            }
            return true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 12);
			recipe.AddIngredient(ItemID.PixieDust, 12);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}


