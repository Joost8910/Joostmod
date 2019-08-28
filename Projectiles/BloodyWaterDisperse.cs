using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BloodyWaterDisperse : ModProjectile
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Bloody Water");
            Main.projFrames[projectile.type] = 4;
		}
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 20;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 600 + Main.rand.Next(600));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 600 + Main.rand.Next(600));
        }
        public override void AI()
        {
            projectile.alpha += 2;
            if (projectile.timeLeft % 5 == 1)
            {
                projectile.frame++;
            }
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid <= 80)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            int numberProjectiles = 6;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X / 2, projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(100));
                perturbedSpeed *= 1f - (Main.rand.NextFloat() * .5f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BloodyWater2"), projectile.damage, 0, projectile.owner);
            }
        }
    }
}

