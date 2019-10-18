using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgBusterFinishingTouch : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Finishing Touch");
            Main.projFrames[projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            projectile.width = 192;
            projectile.height = 192;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 48;
            projectile.alpha = 15;
            projectile.light = 0.2f;
            projectile.tileCollide = false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.timeLeft > 16)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.timeLeft > 16)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            if (projectile.ai[1] <= 0)
            {
                projectile.ai[1] = 1.1f;
                projectile.timeLeft -= (int)projectile.ai[0] * 4;
                projectile.netUpdate = true;
            }
            if (projectile.timeLeft == 8)
            {
                Main.PlaySound(42, projectile.Center, 220 + Main.rand.Next(3));
            }
            if (projectile.timeLeft <= 8)
            {
                projectile.velocity = Vector2.Zero;
                projectile.frame++;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.3f;
                target.wingTime -= 10;
            }
            target.immuneTime = 8;
        }
        public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
        {
            player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = projectile.damage;
        }
        /*
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            Rectangle rect = new Rectangle(0, projectile.frame * (tex.Height / 9), tex.Width, (tex.Height / 9));
            Vector2 drawPosition = Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, new Rectangle?(rect), Color.White, projectile.rotation, drawOrigin, 1f, effects, 0f);
            return false;
        }
        */
    }
}

