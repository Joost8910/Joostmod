using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("a thousand swords");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 400;
            projectile.extraUpdates = 1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] == 0)
            {
                float rot = projectile.rotation;
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = projectile.Center + (unit * -projectile.width / 2);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 68, 16, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.9f;
                target.wingTime -= 5;
            }
            target.immuneTime = 1;
        }
        public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
        {
            player.GetModPlayer<JoostPlayer>(mod).enemyIgnoreDefenseDamage = projectile.damage;
        }
        public override void AI()
        {
            projectile.direction = (projectile.velocity.X < 0 ? -1 : 1);
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = projectile.velocity.ToRotation() + 2.355f + (projectile.direction > 0 ? 0 : -1.57f);
            if (projectile.velocity.Y < 10 && projectile.timeLeft < 150)
            {
                projectile.velocity.Y += 0.15f;
                projectile.rotation = projectile.timeLeft * 0.0174f * 15f * -projectile.direction;
                if (projectile.timeLeft % 9 == 0)
                {
                    Main.PlaySound(SoundID.Item7, projectile.Center);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft -= 48;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X * 0.8f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y * 0.8f;
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
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
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
            }
            //Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0));
            //spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return true;
        }
    }
}
