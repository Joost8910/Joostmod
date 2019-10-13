using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgBusterLimitBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Buster Sword");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 120;
            projectile.aiStyle = 27;
            projectile.hostile = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.alpha = 25;
            projectile.light = 0.7f;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
        public override void AI()
        {
            if (projectile.timeLeft % 5 == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 56);
                Main.dust[dust].noGravity = true;
            }
            projectile.direction = 1;
            if (projectile.velocity.X < 0)
            {
                projectile.direction = -1;
            }
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction < 0 ? 3.14f : 0);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0;
            }
            target.immuneTime = 1;
            projectile.position = target.Center - projectile.Size / 2;
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                float Speed = Main.rand.Next(1, 8) * 0.1f;
                float randRot = Main.rand.Next(360);
                Vector2 vel = new Vector2((float)((Math.Cos(randRot) * Speed) * -1), (float)((Math.Sin(randRot) * Speed) * -1));
                int type = mod.ProjectileType("GilgBusterLimitSlash");
                Projectile.NewProjectile(projectile.Center, vel, type, 10, 0, Main.myPlayer, i);
            }
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 56);
                Main.dust[dust].noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(tex, drawPos, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            }
            Vector2 drawPosition = Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, Color.White, projectile.rotation, drawOrigin, 1f, effects, 0f);
            return false;
        }
    }
}

