using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgTomahawk : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Tomahawk");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.wingTime -= 30;
                target.rocketTime -= 30;
            }
        }
        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X < 0 ? -1 : 1);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.timeLeft * 0.0174f * 14f * -Projectile.direction;
            if (Projectile.timeLeft % 9 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);
            }
            if (Projectile.velocity.Y < 10 && Projectile.timeLeft < 240)
            {
                Projectile.velocity.Y += 0.3f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0f);
            }
            //Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0));
            //spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return true;
        }
    }
}
