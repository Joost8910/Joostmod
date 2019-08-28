using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DoomSkull2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Skull");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = 80;
			projectile.height = 80;
			projectile.aiStyle = 1;
			projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
			projectile.ranged = true;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
			projectile.tileCollide = true;
            projectile.ignoreWater = true;
            aiType = ProjectileID.Bullet;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 34;
            height = 34;
            return true;
        }
        public override void AI()
        {
            projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (projectile.direction == -1 ? 3.14f : 0); ;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            }
            return true;
        }
    }
}

