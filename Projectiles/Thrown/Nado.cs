using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class Nado : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 310;
            Projectile.tileCollide = false;
            Projectile.scale = 0;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }

        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
                int num1 = Dust.NewDust(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                51, //Dust ID
                Main.rand.Next(5) - 2,
                Main.rand.Next(5) - 2,
                100, //alpha goes from 0 to 255
                default,
                1f
                );
                Main.dust[num1].noGravity = true;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
            if (Projectile.timeLeft > 300)
            {
                Projectile.scale = (311 - Projectile.timeLeft) * 0.1f;
                Projectile.position.X = Projectile.Center.X - (int)(56 * Projectile.scale) / 2;
                Projectile.position.Y = Projectile.Center.Y - (int)(56 * Projectile.scale) / 2;
                Projectile.width = (int)(56 * Projectile.scale);
                Projectile.height = (int)(56 * Projectile.scale);
            }
            if (Projectile.timeLeft < 60)
            {
                Projectile.scale = Projectile.timeLeft * 0.016f;
                Projectile.position.X = Projectile.Center.X - (int)(56 * Projectile.scale) / 2;
                Projectile.position.Y = Projectile.Center.Y - (int)(56 * Projectile.scale) / 2;
                Projectile.width = (int)(56 * Projectile.scale);
                Projectile.height = (int)(56 * Projectile.scale);
            }
            Vector2 move = Vector2.Zero;
            float speed = 2.5f;
            float home = 9f;
            if (Projectile.ai[1] > 0)
            {
                NPC target = Main.npc[(int)Projectile.ai[1]];
                if (target.active)
                {
                    move = target.Center - Projectile.Center;
                    if (move.Length() > speed)
                    {
                        move *= speed / move.Length();
                    }
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                    Projectile.velocity *= speed / Projectile.velocity.Length();
                }
                else
                {
                    Projectile.ai[1] = 0;
                }
            }
            else if (Projectile.ai[2] > 0)
            {
                Player target = Main.player[(int)Projectile.ai[2]];
                if (target.active)
                {
                    move = target.Center - Projectile.Center;
                    if (move.Length() > speed)
                    {
                        move *= speed / move.Length();
                    }
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                    Projectile.velocity *= speed / Projectile.velocity.Length();
                }
                else
                {
                    Projectile.ai[2] = 0;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + new Vector2(0f, Projectile.height * 2.5f), new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type])), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

    }
}
