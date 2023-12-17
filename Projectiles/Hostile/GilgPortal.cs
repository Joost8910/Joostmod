using JoostMod.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class GilgPortal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portal");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 292;
            Projectile.height = 292;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Player player = Main.player[host.target];
            Vector2 center = host.Center + new Vector2(host.direction * -200, -100);
            Projectile.position = center - Projectile.Size / 2;
            Projectile.localAI[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (!host.active || host.type != ModContent.NPCType<Gilgamesh2>())
            {
                Projectile.Kill();
            }
            if (Projectile.localAI[0] == 55)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_1"), Projectile.Center); // 202
            }
            float lightStrength = (255f - Projectile.alpha) / 200f;
            Lighting.AddLight(Projectile.Center / 16, 1f * lightStrength, 0.56f * lightStrength, 0.76f * lightStrength);

            Vector2 targetPos = player.MountedCenter;
            Projectile.velocity = Projectile.DirectionTo(targetPos);

            if (Projectile.localAI[0] < 80)
            {
                Projectile.alpha -= 3;
            }
            else if (Projectile.localAI[0] % 3 == 0)
            {
                Projectile.alpha = 0;
                SoundEngine.PlaySound(SoundID.Item19, Projectile.Center);
                float speed = 15f;
                Vector2 offset = new Vector2(Main.rand.Next(-24, 24), Main.rand.Next(-120, 120));
                offset = offset.RotatedBy(Projectile.rotation);
                Vector2 vel = Projectile.velocity * speed;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + offset, vel, ModContent.ProjectileType<GilgSword>(), Projectile.damage, Projectile.knockBack);
            }
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin, bool ignoreY)
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Player P = Main.player[host.target];
            Vector2 vel = ignoreY ? new Vector2(P.velocity.X, 0) : P.velocity;
            Vector2 predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(P.MountedCenter, origin) / speed);
            predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(predictedPos, origin) / speed);
            predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(predictedPos, origin) / speed);
            return predictedPos;
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
            color.A = (byte)(255 * (1f - Projectile.alpha / 255f));
            Rectangle rect = new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, tex.Height / Main.projFrames[Projectile.type]);

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / (2 * Main.projFrames[Projectile.type])), Projectile.scale, effects, 0);
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

    }
}
