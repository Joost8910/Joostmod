using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class GnomeSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome");
            Main.projFrames[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.timeLeft = 16;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 9;
		}
		
		public override void AI()
		{
            Projectile owner = Main.projectile[(int)projectile.ai[0]];
            projectile.spriteDirection = projectile.direction;
            projectile.position += projectile.velocity * 10f * projectile.ai[1];
            projectile.position = owner.Center + new Vector2(0, 9) - (projectile.Size / 2);
			projectile.position += projectile.velocity * projectile.ai[1];
            if (projectile.ai[1] == 0f)
			{
				projectile.ai[1] = 3f;
				projectile.netUpdate = true;
			}
			if (projectile.timeLeft < 10)
            {
                projectile.ai[1] -= 0.6f;
                projectile.frame = 0;
            }
			else
			{
				projectile.ai[1] += 1f;
                projectile.frame = 1;
                owner.velocity = projectile.velocity * 2f;
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= 1.57f;
			}
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor; 
            Rectangle? rect = new Rectangle?(new Rectangle(0, projectile.frame * tex.Height / Main.projFrames[projectile.type], tex.Width, tex.Height / Main.projFrames[projectile.type]));

            Vector2 vel = projectile.velocity;
            vel.Normalize();
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition - vel * 22 * projectile.scale, rect, color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 4), projectile.scale, effects, 0f);
            return false;
        }
    }
}