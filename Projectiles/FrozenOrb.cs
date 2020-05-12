using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FrozenOrb : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Orb");
		}
		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 90;
			projectile.alpha = 95;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
			projectile.coldDamage = true;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 20;
			height = 20;
			return true;
		}
		public override void Kill(int timeLeft)
        {
            int shootNum = 16;
            float shootSpread = 360f;
            float spread = shootSpread * 0.0174f;
            float baseSpeed = 3f;
            double startAngle = 0 - spread / shootNum;
            double deltaAngle = spread / shootNum;
            double offsetAngle;
            int i;
            int damage = (int)(projectile.damage * 0.5f);
            float knockback = projectile.knockBack * 0.5f;
            for (i = 0; i < shootNum; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("Icicle"), damage, knockback, projectile.owner);
            }
        }

        public override void AI()
        {
            projectile.rotation = projectile.ai[1] * 6;
            if ((int)projectile.ai[1] % 10 == 0)
            {
                int shootNum = 3;
                float shootSpread = 360f;
                float spread = shootSpread * 0.0174f;
                float baseSpeed = 3f;
                double startAngle = projectile.rotation - spread / shootNum;
                double deltaAngle = spread / shootNum;
                double offsetAngle;
                int i;
                int damage = (int)(projectile.damage * 0.5f);
                float knockback = projectile.knockBack * 0.5f;
                for (i = 0; i < shootNum; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("Icicle"), damage, knockback, projectile.owner);
                }
            }
            projectile.ai[1]++;
        }

	}
}

