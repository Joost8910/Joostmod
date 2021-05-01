using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Pumpkin : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 28;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.minion = true;
			//projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 1801;
			projectile.extraUpdates = 1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            hitDirection = target.Center.X < player.Center.X ? -1 : 1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] < 0)
            {
                projectile.hide = true;
            }
            else
            {
                projectile.hide = false;
                if ((int)(projectile.ai[0] / 5f) % 10 == 0)
                {
                    int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
                    Main.dust[num1].noGravity = true;
                    Main.dust[num1].velocity *= 0.01f;
                }
            }
            if ((int)projectile.ai[0] == 0)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    Vector2 diff = Main.MouseWorld - player.Center;
                    diff.Normalize();
                    projectile.velocity = diff * projectile.velocity.Length();
                    projectile.netUpdate = true;
                }
            }
			double deg = (double)projectile.ai[0] + 90;
			double rad = deg * (Math.PI / 180);
			double dist = 64; 
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			if (projectile.ai[1] >= 1)
			{
                projectile.localAI[0] += projectile.velocity.X;
                projectile.localAI[1] += projectile.velocity.Y;
                projectile.netUpdate = true;
                dist = projectile.timeLeft < 1744 ? 8 : (projectile.timeLeft - 1736);
				projectile.rotation = (float)rad;
				projectile.ownerHitCheck = false;
                if (Collision.SolidCollision(new Vector2(projectile.localAI[0] - 5, projectile.localAI[1] - 5), 10, 10))
                {
                    projectile.Kill();
                }
            }
			else
            {
                projectile.localAI[0] = player.Center.X;
                projectile.localAI[1] = player.Center.Y;
                projectile.ownerHitCheck = true;
                projectile.timeLeft = 1800;
            }
            Vector2 origin = new Vector2(projectile.localAI[0], projectile.localAI[1]);
            projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
			projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;	
			projectile.ai[0] += 5;
		}
        public override bool? CanHitNPC(NPC target)
        {
            return projectile.ai[0] > 0;
        }
        public override bool CanHitPvp(Player target)
        {
            return projectile.ai[0] > 0;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 8;
			height = 8;
			fallThrough = true;
			return false;
		}
	}
}

