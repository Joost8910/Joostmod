using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SAXBeamCharged : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            //projectile.light = 0.75f;
            Projectile.coldDamage = true;
            AIType = ProjectileID.Bullet;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            Color color = Color.White;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                if (!target.HasBuff(BuffID.Frozen))
                {
                    target.AddBuff(BuffID.Frozen, 100, true);
                }
            }
            target.AddBuff(BuffID.Frostburn, 600, true);
            target.AddBuff(BuffID.Chilled, 200, true);

        }
        public override void AI()
        {
            if (Projectile.ai[0] != 0)
            {
                if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = Projectile.position.X;
                }
                if (Projectile.localAI[1] == 0)
                {
                    Projectile.localAI[1] = Projectile.position.Y;
                }
                float freq = 0.15f;
                float mag = 40f;
                int time = 1800 - Projectile.timeLeft;
                Vector2 pos = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                Vector2 axis = dir.RotatedBy(90 * Projectile.ai[0] * 0.0174f);
                Vector2 wave = axis * (float)Math.Sin(time * freq) * mag;
                Projectile.position = pos + wave;
                Projectile.localAI[0] = Projectile.position.X - wave.X + Projectile.velocity.X;
                Projectile.localAI[1] = Projectile.position.Y - wave.Y + Projectile.velocity.Y;
            }
        }
    }
}

