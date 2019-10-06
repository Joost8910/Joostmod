using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgPortal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portal");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 292;
            projectile.height = 292;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 10)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;
            }
            NPC host = Main.npc[(int)projectile.ai[0]];
            Player player = Main.player[host.target];
            Vector2 center = host.Center + new Vector2(host.direction * -200, -100);
            projectile.position = center - projectile.Size / 2;
            projectile.localAI[0]++;
            projectile.rotation = projectile.velocity.ToRotation();
            if (!host.active || host.type != mod.NPCType("Gilgamesh2"))
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 55)
            {
                Main.PlaySound(42, projectile.Center, 202);
            }
            float lightStrength = (255f - projectile.alpha) / 200f;
            Lighting.AddLight(projectile.Center / 16, 1f * lightStrength, 0.56f * lightStrength, 0.76f * lightStrength);

            Vector2 targetPos = player.MountedCenter;
            projectile.velocity = projectile.DirectionTo(targetPos);

            if (projectile.localAI[0] < 80)
            {
                projectile.alpha -= 3;
            }
            else if (projectile.localAI[0] % 3 == 0)
            {
                projectile.alpha = 0;
                Main.PlaySound(SoundID.Item19, projectile.Center);
                float speed = 15f;
                Vector2 offset =  new Vector2(Main.rand.Next(-24, 24), Main.rand.Next(-120, 120));
                offset = offset.RotatedBy(projectile.rotation);
                Vector2 vel = projectile.velocity * speed;
                Projectile.NewProjectile(projectile.Center + offset, vel, mod.ProjectileType("GilgSword"), projectile.damage, projectile.knockBack);
            }
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin, bool ignoreY)
        {
            NPC host = Main.npc[(int)projectile.ai[0]];
            Player P = Main.player[host.target];
            Vector2 vel = (ignoreY ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Color.White;
            color.A = (byte)(255 * (1f - (projectile.alpha / 255f)));
            Rectangle rect = new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), (tex.Width), (tex.Height / Main.projFrames[projectile.type]));

            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rect, color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / (2 * Main.projFrames[projectile.type])), projectile.scale, effects, 0f);
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
