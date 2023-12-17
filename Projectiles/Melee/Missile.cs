using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Missile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Missile");
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Projectile.timeLeft > 550)
            {
                int dustIndex = Dust.NewDust(Projectile.Center - Projectile.velocity, 1, 1, 127, 0, 0, 0, default, 2f);
                Main.dust[dustIndex].noGravity = true;
            }
            else
            {
                Projectile.velocity.Y += 0.3f;
                if (Projectile.velocity.Y > 10)
                {
                    Projectile.velocity.Y = 10;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Explosion2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}

