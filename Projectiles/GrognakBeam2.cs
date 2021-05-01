using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrognakBeam2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 120;
            projectile.alpha = 75;
            projectile.tileCollide = true;
            projectile.extraUpdates = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = (int)(projectile.damage * 0.7f);
            projectile.knockBack *= 0.7f;
            if (projectile.damage < 1)
            {
                projectile.Kill();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            hitDirection = target.Center.X < player.Center.X ? -1 : 1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.timeLeft % 6 == 0)
            {
                int num1 = Dust.NewDust(
                            projectile.position,
                            projectile.width,
                            projectile.height,
                            197,
                            projectile.velocity.X,
                            projectile.velocity.Y,
                            (projectile.timeLeft < 75) ? 150 - (projectile.timeLeft * 2) : 0,
                            new Color(0, 255, 0),
                            2f
                            );
                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            projectile.direction = projectile.velocity.X < 0 ? -1 : 1;
            projectile.spriteDirection = projectile.direction;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 197, 0, 0, 230 - timeLeft * 2, new Color(0, 255, 0), 2f).noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            if (projectile.timeLeft < 75)
            {
                color *= projectile.timeLeft / 75f;
            }
            float rot = projectile.rotation;
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
            for (int k = projectile.oldPos.Length - 1; k > 0; k--)
            {
                float scale = projectile.scale * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(tex, drawPos, rect, color, projectile.oldRot[k], drawOrigin, scale, effects, 0f);
            }

            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rot, drawOrigin, projectile.scale, effects, 0f);

            return false;
        }
    }
}

