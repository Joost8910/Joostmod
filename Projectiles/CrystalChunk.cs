using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class CrystalChunk : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Chunk");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
            projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 30;
			projectile.alpha = 25;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 10;
			height = 10;
			return true;
		}
        public override void AI()
        {
            projectile.rotation = projectile.timeLeft * -projectile.direction;
        }
        public override void Kill(int timeLeft)
		{
            float spread = (float)Math.PI * 2;
            int baseSpeed = 4;
            double startAngle = (float)Math.PI / 2;
            double deltaAngle = spread / 8;
            double offsetAngle;
            int i;
            for (i = 0; i < 8; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), 90, projectile.damage, 4, projectile.owner);
            }
		}

	}
}

