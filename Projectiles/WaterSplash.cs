using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class WaterSplash : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Splash");
		}
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.alpha = 80;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage /= 2;
            projectile.knockBack /= 2;
        }
        public override void AI()
        {
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 100 || Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.tileCollide = false;
                projectile.penetrate = -1;
                projectile.timeLeft = 2;
                projectile.velocity.Y = (projectile.velocity.Y > -10 ? projectile.velocity.Y - 0.3f : projectile.velocity.Y);
            }
            else
            {
                projectile.velocity.Y = (projectile.velocity.Y < 10 ? projectile.velocity.Y + 0.3f : projectile.velocity.Y);
            }
            projectile.velocity.X *= 0.98f;
        }
        public override void Kill(int timeLeft)
        {
            int x = (int)projectile.Center.ToTileCoordinates().X;
            int y = (int)projectile.Center.ToTileCoordinates().Y;
            Main.tile[x, y].liquidType(0);
            Main.tile[x, y].liquid = 255;
            WorldGen.SquareTileFrame(x, y, true);
            if (Main.netMode == 1)
            {
                NetMessage.sendWater(x, y);
            }
            Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 1);
        }
	}
}

