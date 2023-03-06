using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ToxicWater : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxic Water");
		}
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.alpha = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600 + Main.rand.Next(600));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600 + Main.rand.Next(600));
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.98f;
            Projectile.velocity.Y = (Projectile.velocity.Y < 10 ? Projectile.velocity.Y + 0.3f : Projectile.velocity.Y);
            if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
		{
            if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80)
            {
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("ToxicWaterDisperse").Type, Projectile.damage, 0, Projectile.owner);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Splash, Projectile.position);
                int numberProjectiles = 6;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X / 2, Projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(360));
                    perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f);
                    Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("ToxicWater2").Type, Projectile.damage, 0, Projectile.owner);
                }
            }
        }
	}
}

