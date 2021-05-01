using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgBusterMeleeSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Buster Sword");
            Main.projFrames[projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            projectile.width = 192;
            projectile.height = 192;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 9;
            projectile.alpha = 15;
            projectile.light = 0.2f;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)projectile.ai[0]];
            projectile.position = (host.Center + new Vector2(-28 * host.direction, -47) - projectile.Size / 2) + host.velocity;
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1] = 1;
            }
            int flip = projectile.ai[1] < 0 ? -1 : 1;
            float speed = Math.Abs(projectile.ai[1]);
            if (projectile.localAI[1] <= 0)
            {
                projectile.localAI[1] = 1.1f;
                projectile.netUpdate = true;
                projectile.direction = host.direction;
                projectile.spriteDirection = projectile.direction * flip;
                Main.PlaySound(42, projectile.Center, 220 + Main.rand.Next(3));
            }
            projectile.localAI[0] += speed;
            if (projectile.localAI[0] >= 8)
            {
                projectile.Kill();
            }
            else
            {
                projectile.frame = (int)projectile.localAI[0];
                projectile.timeLeft = 2;
                projectile.rotation = projectile.velocity.ToRotation() + flip == -1 ? 3.14f : 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.3f;
                target.wingTime -= 10;
            }
            target.immuneTime = (int)(9 / Math.Abs(projectile.ai[1]));
        }
        public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
        {
            player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = projectile.damage;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, Color.White * 0.94f, projectile.rotation, drawOrigin, 1.35f, effects, 0f);
            return false;
        }
    }
}

