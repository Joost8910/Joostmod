using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Shrapnel : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shrapnel");
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1200;
            Projectile.arrow = true;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.extraUpdates = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft -= 600;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(45);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .5f; 
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("Needle4").Type, Projectile.damage/2, Projectile.knockBack/2, Projectile.owner);
            }
        }

    }
}

