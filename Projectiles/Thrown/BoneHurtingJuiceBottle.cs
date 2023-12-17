using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class BoneHurtingJuiceBottle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottle Of Bone Hurting Juice");
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 350;
            AIType = ProjectileID.Shuriken;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item107, Projectile.position);
            for (int i = 0; i < 12; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, -3).RotatedByRandom(MathHelper.ToRadians(360));
                float scale = 1f - Main.rand.NextFloat() * .3f;
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<BoneHurtingJuice>(), Projectile.damage / 2, 0, Projectile.owner);
            }
        }
    }
}

