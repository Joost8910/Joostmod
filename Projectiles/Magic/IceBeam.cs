using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class IceBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            //projectile.light = 0.75f;
            AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.coldDamage = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            Color color = Color.White;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            n.AddBuff(44, 300);
        }
        public override void AI()
        {/*
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 8;
            }*/
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
                float mag = 30f;
                int time = 600 - Projectile.timeLeft;
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

