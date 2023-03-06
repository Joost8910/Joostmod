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
	public class SoulSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Spear");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 58;
			Projectile.height = 58;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = 20;
			Projectile.timeLeft = 180;
			Projectile.tileCollide = false;
			Projectile.light = 0.75f;
            Projectile.scale = 1f;
			AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92);
            Main.dust[dustIndex].noGravity = true;
            if (Projectile.timeLeft < 150)
            {
                Projectile.tileCollide = true;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY) + Projectile.velocity;
                Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            Color color = Color.White * 0.9f;
            spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 22; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
        }
    }
}

