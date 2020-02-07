using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SoulArrow : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Arrow");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 150;
			aiType = ProjectileID.Bullet;
            projectile.arrow = true;
            projectile.ranged = true;
            projectile.magic = true;
        }
        public override void AI()
        {
            if (projectile.timeLeft % 3 == 0)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = projectile.velocity.Length();
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && Main.npc[k].lifeMax > 5 && Main.npc[k].CanBeChasedBy(this, false) && Collision.CanHit(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, Main.npc[k].position, Main.npc[k].width, Main.npc[k].height))
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                if (move.Length() > projectile.localAI[0] && projectile.localAI[0] > 0)
                {
                    move *= projectile.localAI[0] / move.Length();
                }
                float home = 20f;
                projectile.velocity = ((home - 1f) * projectile.velocity + move) / home;
            }
            if (projectile.velocity.Length() < projectile.localAI[0] && projectile.localAI[0] > 0)
            {
                projectile.velocity *= (projectile.localAI[0] / projectile.velocity.Length());
            }
        }
        
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            Color color = Color.White;
            color *= 0.7f;
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 12;
            height = 12;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            Main.PlaySound(2, projectile.Center, 8);
        }
    }
}

