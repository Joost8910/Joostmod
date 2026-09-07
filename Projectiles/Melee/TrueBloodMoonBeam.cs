using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public class TrueBloodMoonBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Night's Fury");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.timeLeft = 180;
            Projectile.extraUpdates = 3;
            Projectile.alpha = 75;
            Projectile.tileCollide = false;
            //Projectile.light = 0.5f;

        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(38 * Projectile.scale);
            hitbox.Height = (int)(38 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage =(int)(Projectile.damage * 0.75f);

            for (int i = 0; i < 4; i++)
            {
                float rot = MathHelper.ToRadians(i * (360f / 4));
                Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(Projectile.Center), DustID.PortalBoltTrail, rot.ToRotationVector2() * 3, 0, Color.Yellow, 1.5f).noGravity = true;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);

            for (int i = 0; i < 4; i++)
            {
                float rot = MathHelper.ToRadians(i * (360f / 4));
                Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(Projectile.Center), DustID.PortalBoltTrail, rot.ToRotationVector2() * 3, 0, Color.Yellow, 1.5f).noGravity = true;
            }
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 2 == 0)
            {
                //say you wanted to add particles that stay mostly still to leave a trail behind a projectile
                int num1 = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         DustID.GoldFlame, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         Projectile.scale + 1f
                         );

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            Lighting.AddLight(Projectile.Center, 0.5f, 0.8f, 0.25f);
            //Projectile.rotation += Projectile.timeLeft * -Projectile.direction * 0.0174f * 5;

            if (Projectile.timeLeft == 180)
            {
                Projectile.localAI[0] = Projectile.Center.X;
                Projectile.localAI[1] = Projectile.Center.Y;
                Projectile.direction = Math.Sign(Projectile.ai[0]);
                Projectile.spriteDirection = Projectile.direction;
                //Main.NewText(Projectile.ai[0]);
                Projectile.ai[2] = 1f;
                Projectile.scale = 0.5f * Projectile.ai[1];
            }
            float updateScale = 1f / (Projectile.extraUpdates + 1);
            Vector2 origin = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            double rad = Projectile.ai[0];
            double dist = (180 - (Projectile.timeLeft) + 24) * Projectile.ai[1];
            Projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.rotation = Projectile.spriteDirection > 0 ? (float)rad : (float)(rad + Math.PI);
            Projectile.ai[0] += MathHelper.ToRadians(11) * Projectile.spriteDirection * Projectile.ai[2] * updateScale;

            Projectile.scale += 0.01f * updateScale;
            Projectile.ai[2] += 0.01f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         DustID.GoldFlame, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         1f
                         );

                Main.dust[dust].noGravity = true;
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

            Color color = Color.White;

            if (Projectile.timeLeft < 20)
            {
                color *= Projectile.timeLeft / 20f;
            }

            Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / 2);
            Rectangle? drawRect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                float scale = Projectile.scale * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Main.EntitySpriteDraw(tex, drawPos, drawRect, color, Projectile.oldRot[k], drawOrigin, scale, effects);
            }

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), drawRect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects);

            return false;
        }
    }
}

