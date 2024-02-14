using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class EternalFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Flame");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 8;
            Projectile.timeLeft = 75;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 8;
        }
        public override void ModifyDamageScaling(ref float damageScale)
        {
            damageScale = 0.1f * Projectile.penetrate;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 4f).noGravity = true;
            }
            double deg = Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = 60;
            Projectile parent = Main.projectile[(int)Projectile.ai[0]];
            if (parent.active && parent.timeLeft > 0 && parent.type == ModContent.ProjectileType<EternalFlame2>())
            {
                Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.ai[1] -= 13 * parent.spriteDirection;
            Projectile.spriteDirection = parent.spriteDirection;
            Projectile.rotation = (float)rad;
            int s = 75 - Projectile.timeLeft;
            if (Projectile.timeLeft >= 45)
            {
                Projectile.scale = s / 30f;
            }
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(40 * Projectile.scale);
            hitbox.Height = (int)(40 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Color color = Color.White * (Projectile.penetrate / 8f);
            int s = 75 - Projectile.timeLeft;
            if (Projectile.timeLeft > 45)
            {
                color *= s / 30f;
            }
            else if (Projectile.timeLeft < 15)
            {
                color *= Projectile.timeLeft / 15f;
            }
            float rot = Projectile.rotation;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection > 0)
            {
                effects = SpriteEffects.FlipVertically;
            }
            Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / 2);
            for (int k = Math.Min(Projectile.oldPos.Length - 1, s); k > 0; k--)
            {
                float scale = Projectile.scale * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                Main.EntitySpriteDraw(tex, drawPos, rect, color, Projectile.oldRot[k], drawOrigin, scale, effects, 0);
            }

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rot, drawOrigin, Projectile.scale, effects, 0);

            return false;
        }
    }
}

