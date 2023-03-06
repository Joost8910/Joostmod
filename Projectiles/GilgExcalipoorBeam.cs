using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgExcalipoorBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Excalipoor... somehow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = 27;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 35;
            Projectile.alpha = 75;
            Projectile.light = 0.7f;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.damage = 1;
            Projectile.direction = 1;
            if (Projectile.velocity.X < 0)
            {
                Projectile.direction = -1;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction < 0 ? 3.14f : 0);
            Projectile.velocity *= 0.94f;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 261);
                Main.dust[dust].noGravity = true;
            }
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
            return false;
        }
    }
}

