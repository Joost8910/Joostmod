using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class TrueHallowedFlailBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Hallowed Flail");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 75;
            //Projectile.light = 0.5f;

        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(38 * Projectile.scale);
            hitbox.Height = (int)(38 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 2 == 0)
            {
                //say you wanted to add particles that stay mostly still to leave a trail behind a projectile
                int num1 = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         72, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         1f
                         );

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            Lighting.AddLight(Projectile.Center, 0.625f, 0.3f, 0.6f);
            Projectile.rotation += Projectile.timeLeft * -Projectile.direction * 0.0174f * 5;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         DustID.Gastropod, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         1f
                         );

                Main.dust[dust].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }

    }
}

