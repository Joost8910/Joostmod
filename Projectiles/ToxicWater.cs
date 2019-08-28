using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.alpha = 80;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
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
            projectile.velocity.X *= 0.98f;
            projectile.velocity.Y = (projectile.velocity.Y < 10 ? projectile.velocity.Y + 0.3f : projectile.velocity.Y);
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
		{
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("ToxicWaterDisperse"), projectile.damage, 0, projectile.owner);
            }
            else
            {
                Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0);
                int numberProjectiles = 6;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X / 2, projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(360));
                    perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ToxicWater2"), projectile.damage, 0, projectile.owner);
                }
            }
        }
	}
}

