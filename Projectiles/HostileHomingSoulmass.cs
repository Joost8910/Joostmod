using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HostileHomingSoulmass : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Homing Soulmass");
		}
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 360;
			projectile.tileCollide = false;
            projectile.alpha = 55;
		}
		public override void AI()
		{
			if (projectile.timeLeft % 2 == 0)
			{
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
			double deg = projectile.ai[1];
			double rad = deg * (Math.PI / 180);
            NPC host = Main.npc[(int)projectile.ai[0]];
            Player player = Main.player[host.target];
            if (projectile.localAI[1] == 0)
            {
                projectile.position.X = host.Center.X - (int)(Math.Cos(rad) * 100) - projectile.width / 2;
                projectile.position.Y = (host.Center.Y - 50) - (int)(Math.Sin(rad) * 100) - projectile.height / 2;
                projectile.direction = host.direction;
                if (!host.active || host.type != mod.NPCType("Spectre"))
                {
                    projectile.Kill();
                }
            }
            projectile.spriteDirection = projectile.direction;
            Vector2 move = Vector2.Zero;
            float distance = 800f;
            bool target = false;
            for (int k = 0; k < 255; k++)
            {
                if (projectile.Distance(player.Center) > distance || !player.active || player.dead)
                {
                    player = Main.player[k];
                }
                if (player.active && !player.dead && Collision.CanHit(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, player.position, player.width, player.height))
                {
                    Vector2 newMove = player.Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            projectile.rotation = (float)rad + (projectile.timeLeft * -12 * (float)Math.PI / 180f * projectile.direction);
            if (projectile.timeLeft < 270 && Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1) && projectile.localAI[1] == 0)
            {
                projectile.velocity = projectile.DirectionTo(player.Center) * 8;
                projectile.localAI[0] = projectile.velocity.Length();
                Main.PlaySound(2, projectile.Center, 9);
                projectile.localAI[1] = 1;
                projectile.ai[1] = 0;
                projectile.tileCollide = true;
            }
            if (projectile.localAI[1] == 1)
            {
                if (target)
                {
                    if (move.Length() > projectile.localAI[0] && projectile.localAI[0] > 0)
                    {
                        move *= projectile.localAI[0] / move.Length();
                    }
                    float home = 30f;
                    projectile.velocity = ((home - 1f) * projectile.velocity + move) / home;
                }
                if (projectile.velocity.Length() < projectile.localAI[0] && projectile.localAI[0] > 0)
                {
                    projectile.velocity *= (projectile.localAI[0] / projectile.velocity.Length());
                }
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(255, 255, 255, 150);
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
        }
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            Main.PlaySound(2, projectile.Center, 27);
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 20;
            height = 20;
            return true;
        }
    }
}

