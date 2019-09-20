using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GilgAxe : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh's Axe");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 190;
		}
        /*public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -projectile.velocity.X, -projectile.velocity.Y, mod.ProjectileType("GilgAxe2"), projectile.damage, projectile.knockBack, projectile.owner);
        }*/
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.timeLeft > 145)
            {
                projectile.timeLeft = 145;
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                target.velocity.Y += projectile.knockBack;
            }
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)projectile.ai[0]];
            Vector2 arm = host.Center + new Vector2(39 * host.direction, -38);
            projectile.direction = (projectile.velocity.X < 0 ? -1 : 1);
            projectile.spriteDirection = projectile.direction;
            if (projectile.timeLeft < 145)
            {
                projectile.velocity = projectile.DirectionTo(arm) * (18 + (projectile.Distance(arm) / 80));
                if (projectile.Distance(arm) < 30)
                {
                    projectile.Kill();
                }
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) - 1.57f;
                projectile.spriteDirection = -projectile.direction;
                projectile.tileCollide = false;
            }
            else
            {
                projectile.rotation = projectile.timeLeft * 0.0174f * 14f * -projectile.direction;
                if (projectile.timeLeft % 9 == 0)
                {
                    Main.PlaySound(SoundID.Item7, projectile.Center);
                }
                if (projectile.velocity.Y < 10 && projectile.timeLeft < 160)
                {
                    projectile.velocity.Y += 0.3f;
                }
            }
            if (!host.active || host.life <= 0)
            {
                projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
            }
            
            Texture2D texture = ModContent.GetTexture("JoostMod/Projectiles/Axe_Chain");
            NPC host = Main.npc[(int)projectile.ai[0]];
            Vector2 position = projectile.Center;
            Vector2 arm = host.Center + new Vector2(39 * host.direction, -38);
            Vector2 dir = position - arm;
            dir.Normalize();
            arm += dir * 32;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = arm - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = arm - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }

    }
}
