using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Boulder : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boulder");
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 500;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Projectile.velocity.Y < 20)
            {
                Projectile.velocity.Y += 0.3f;
            }
            Projectile.scale = Projectile.ai[0];
            Projectile.Resize((int)(40 * Projectile.scale), (int)(40 * Projectile.scale));
            if (Projectile.ai[1] > 5 * Projectile.scale)
            {
                Projectile.tileCollide = true;
            }
            else
            {
                Projectile.ai[1]++;
            }
            Projectile.rotation = Projectile.timeLeft * Projectile.direction * 0.0174f * Projectile.velocity.Y * 0.1f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.damage -= 50;
            Projectile.knockBack *= 0.8f;
            if (Projectile.damage <= 0)
            {
                Projectile.Kill();
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -0.5f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.5f;
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_impact_0").WithPitchOffset(0.1f), Projectile.Center); //207
            //SoundEngine.PlaySound(SoundID.Trackable.WithPitchOffset(0.1f), Projectile.Center);
            for (int d = 0; d < 20; d++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone, 0, 0, 0, default, 2);
            }
        }
    }
}
