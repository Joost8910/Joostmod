using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HostileSoulGreatsword : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Greatsword");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 160;
			projectile.height = 160;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 50;
            projectile.tileCollide = false;
            projectile.alpha = 255;
		}
        public override void AI()
        {
	        NPC host = Main.npc[(int)projectile.ai[0]];
            projectile.direction = host.direction;
			projectile.spriteDirection = host.spriteDirection;
			double rad = (host.rotation - 2.355f) + (projectile.ai[1] * 0.0174f * projectile.direction) + (projectile.direction == 1 ? 0 : -1.57f); 
			projectile.rotation = (float)rad;
            double dist = -122 * projectile.direction; 
            projectile.position.X = host.Center.X + (40 * host.direction) - (int)(Math.Cos(rad) * dist) - (projectile.width / 2);
            projectile.position.Y = host.Center.Y + (-16) - (int)(Math.Sin(rad) * dist) - (projectile.height / 2);
            projectile.scale = 1f;
            if (!host.active || host.type != mod.NPCType("Spectre"))
            {
                projectile.Kill();
            }
            if (projectile.timeLeft > 25)
            {
                projectile.alpha -= 8;
            }
            if (projectile.timeLeft == 25)
            {
                Main.PlaySound(25, projectile.Center);
            }
            if (projectile.timeLeft == 11)
            {
                Main.PlaySound(42, projectile.Center, 186);
            }
            if (projectile.timeLeft <= 10)
            {
                if (projectile.ai[1] < 180)
                {
                    projectile.timeLeft = 10;
                    projectile.ai[1] += 15;
                }
                else
                {
                    projectile.alpha += 16;
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.timeLeft <= 10 && (projectile.timeLeft > 4 || projectile.localAI[1] > 0))
            {
                NPC host = Main.npc[(int)projectile.ai[0]];
                float rot = projectile.rotation - 1.57f + (0.785f * host.direction);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = projectile.Center + (unit * -projectile.width / 2);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 186 * projectile.scale, 52 * projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        /*
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (projectile.ai[1] < 45)
            {
                hitbox.Width = 220;
                hitbox.Height = 50;
                hitbox.Y += 55;
                hitbox.X -= 30;
            }
            else if (projectile.ai[1] < 90)
            {
                hitbox.Width = 160;
                hitbox.Height = 160;
            }
            else if (projectile.ai[1] < 135)
            {
                hitbox.Width = 50;
                hitbox.Height = 220;
                hitbox.X += 55;
                hitbox.Y -= 30;
            }
            else if (projectile.ai[1] < 180)
            {
                hitbox.Width = 160;
                hitbox.Height = 160;
            }
            else
            {
                hitbox.Width = 220;
                hitbox.Height = 50;
                hitbox.Y += 55;
                hitbox.X -= 30;
            }
        }*/
        public override bool CanHitPlayer(Player target)
        {
            return projectile.timeLeft <= 10 && projectile.timeLeft > 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (projectile.alpha < 100)
            {
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                    sb.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            Color color = Color.White * (1f - (projectile.alpha / 255f));
            color.A = 150;
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
    }
}
