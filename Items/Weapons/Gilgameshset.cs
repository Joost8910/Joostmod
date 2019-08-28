using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
	public class Gilgameshset : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh's Weapon Set");
			Tooltip.SetDefault("'Too bad you don't have 8 arms'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(48, 4));
		}
        public override void SetDefaults()
        {
            item.damage = 800;
            item.melee = true;
            item.width = 36;
            item.height = 60;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 50000000;
            item.rare = -12;
            item.scale = 1f;
            item.expert = true;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Naginata");
            item.shootSpeed = 8f;

        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("Naginata")] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int Gilgwep = Main.rand.Next(4);
			if (Gilgwep == 1)
				{
				float spread = 15f * 0.0174f;
				float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
				double startAngle = Math.Atan2(speedX, speedY)- spread/2;
				double deltaAngle = spread/2f;
				double offsetAngle;
				int i;
				for (i = 0; i < 3;i++ )
				{
					offsetAngle = startAngle + deltaAngle * i;
					Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle) * 2, baseSpeed*(float)Math.Cos(offsetAngle) * 2, mod.ProjectileType("Kunai"), damage, knockBack / 2, player.whoAmI);
				}
			}
			if (Gilgwep == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 3), (speedY * 3), mod.ProjectileType("Axe"), (damage * 2), knockBack * 1.2f, player.whoAmI);
			}
			if (Gilgwep == 3)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 4), (speedY * 4), mod.ProjectileType("Flail"), (int)(damage * 2.5f), knockBack * 2, player.whoAmI);
			}
			if (Gilgwep == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, (damage * 3), knockBack, player.whoAmI);
			}
			return false;
		}
		
	}
}


