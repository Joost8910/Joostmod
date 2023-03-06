using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Gunblade");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 1800;
            Projectile.alpha = 5;
            Projectile.extraUpdates = 2;
            Projectile.scale = 1.5f;
            AIType = ProjectileID.Bullet;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.velocity *= -1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft -= 1000;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            }
            return true;
        }

    }
}

