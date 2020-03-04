using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SoulSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Spear");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 58;
			projectile.height = 58;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 20;
			projectile.timeLeft = 180;
			projectile.tileCollide = false;
			projectile.light = 0.75f;
            projectile.scale = 1f;
			aiType = ProjectileID.Bullet;
            projectile.thrown = true;
            projectile.magic = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
            Main.dust[dustIndex].noGravity = true;
            if (projectile.timeLeft < 150)
            {
                projectile.tileCollide = true;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY) + projectile.velocity;
                Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            Color color = Color.White * 0.9f;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 22; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            Main.PlaySound(2, projectile.Center, 27);
        }
    }
}

