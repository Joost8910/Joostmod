using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace JoostMod.Projectiles
{
	public class GiantGnunderken : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnunderson's Giant Shuriken");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 104;
			projectile.height = 104;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.timeLeft = 300;
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
            projectile.localNPCHitCooldown = (int)(25 / projectile.velocity.Length());
            projectile.spriteDirection = projectile.direction;
            projectile.ai[1] += projectile.spriteDirection * 3.6f * projectile.velocity.Length();
            projectile.rotation = projectile.ai[1] * 0.0174f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
            for (int k = 1; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2) - projectile.velocity * k;
                Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
            }
            return true;
        }
    }
}


