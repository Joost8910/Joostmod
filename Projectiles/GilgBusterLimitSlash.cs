using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgBusterLimitSlash : ModProjectile
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
            Projectile.timeLeft = 48;
            Projectile.alpha = 15;
            Projectile.light = 0.2f;
            Projectile.tileCollide = false;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 46;
            hitbox.Y += 46;
            hitbox.Width = 100;
            hitbox.Height = 100;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.timeLeft > 16)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.timeLeft > 16)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            if (Projectile.ai[1] <= 0)
            {
                Projectile.ai[1] = 1.1f;
                Projectile.timeLeft -= (int)Projectile.ai[0] * 4;
                Projectile.netUpdate = true;
            }
            if (Projectile.timeLeft == 8)
            {
                SoundEngine.PlaySound(42, Projectile.Center, 220 + Main.rand.Next(3));
            }
            if (Projectile.timeLeft <= 8)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.frame++;
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.3f;
                target.wingTime -= 10;
            }
            target.immuneTime = 2;
        }
        public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
        {
            player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = Projectile.damage;
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

