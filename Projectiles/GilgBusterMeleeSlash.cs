using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgBusterMeleeSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Buster Sword");
            Main.projFrames[Projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.width = 192;
            Projectile.height = 192;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 9;
            Projectile.alpha = 15;
            Projectile.light = 0.2f;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Projectile.position = (host.Center + new Vector2(-28 * host.direction, -47) - Projectile.Size / 2) + host.velocity;
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
            }
            int flip = Projectile.ai[1] < 0 ? -1 : 1;
            float speed = Math.Abs(Projectile.ai[1]);
            if (Projectile.localAI[1] <= 0)
            {
                Projectile.localAI[1] = 1.1f;
                Projectile.netUpdate = true;
                Projectile.direction = host.direction;
                Projectile.spriteDirection = Projectile.direction * flip;
                SoundEngine.PlaySound(42, Projectile.Center, 220 + Main.rand.Next(3));
            }
            Projectile.localAI[0] += speed;
            if (Projectile.localAI[0] >= 8)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.frame = (int)Projectile.localAI[0];
                Projectile.timeLeft = 2;
                Projectile.rotation = Projectile.velocity.ToRotation() + flip == -1 ? 3.14f : 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.3f;
                target.wingTime -= 10;
            }
            target.immuneTime = (int)(9 / Math.Abs(Projectile.ai[1]));
        }
        public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
        {
            player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = Projectile.damage;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, Color.White * 0.94f, Projectile.rotation, drawOrigin, 1.35f, effects, 0f);
            return false;
        }
    }
}

