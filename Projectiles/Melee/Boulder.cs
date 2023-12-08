using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Boulder : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boulder");
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
            if (Projectile.ai[0] > 5)
            {
                Projectile.tileCollide = true;
            }
            else
            {
                Projectile.ai[0]++;
            }
            Projectile.rotation = Projectile.timeLeft * Projectile.direction * 0.0174f * Projectile.velocity.Y * 0.1f;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_impact_0") with { Pitch = 1.1f }, Projectile.Center); //207, og 0.1f pitch, adaptation adds 1f
            //SoundEngine.PlaySound(SoundID.Trackable.WithPitchOffset(0.1f), Projectile.Center);
            for (int d = 0; d < 20; d++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone, 0, 0, 0, default, 2);
            }
        }
    }
}
