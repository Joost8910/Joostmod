using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hybrid
{
    public class SoulArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 150;
            AIType = ProjectileID.Bullet;
            Projectile.arrow = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.CountsAsClass(DamageClass.Magic);
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 3 == 0)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.Length();
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && Main.npc[k].lifeMax > 5 && Main.npc[k].CanBeChasedBy(this, false) && Collision.CanHit(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, Main.npc[k].position, Main.npc[k].width, Main.npc[k].height))
                {
                    Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                if (move.Length() > Projectile.localAI[0] && Projectile.localAI[0] > 0)
                {
                    move *= Projectile.localAI[0] / move.Length();
                }
                float home = 20f;
                Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
            }
            if (Projectile.velocity.Length() < Projectile.localAI[0] && Projectile.localAI[0] > 0)
            {
                Projectile.velocity *= Projectile.localAI[0] / Projectile.velocity.Length();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            Color color = Color.White;
            color *= 0.7f;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 12;
            height = 12;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
        }
    }
}

