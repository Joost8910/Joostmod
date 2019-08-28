using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class LunarShuriken : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Shuriken");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.alpha = 5;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Shuriken;
        }
        public override void PostAI()
        {
            projectile.spriteDirection = -projectile.direction;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 10;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(60);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 vel = new Vector2(0, -2.5f);
                if (projectile.velocity.ToRotation() < 0)
                {
                    vel = new Vector2(0, 2.5f);
                }
                Vector2 perturbedSpeed = vel.RotatedByRandom(rotation);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.MoonlordArrowTrail, projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
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
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
            }
            return false;
        }

    }
}


