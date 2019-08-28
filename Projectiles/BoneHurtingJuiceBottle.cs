using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BoneHurtingJuiceBottle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottle Of Bone Hurting Juice");
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 350;
            aiType = ProjectileID.Shuriken;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item107, projectile.position);
            for (int i = 0; i < 12; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, -3).RotatedByRandom(MathHelper.ToRadians(360));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BoneHurtingJuice"), projectile.damage/2, 0, projectile.owner);
            }
        }
    }
}

