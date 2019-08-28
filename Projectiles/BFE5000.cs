using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BFE5000 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BFE5000");
            Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 80;
			projectile.height = 80;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 240;
			aiType = ProjectileID.Bullet;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 34;
			height = 34;
			return true;
		}
        public override void AI()
        {
            if (projectile.timeLeft % 5 == 0)
            {
                projectile.frame = (projectile.frame + 1) % 3;
                Dust.NewDust(projectile.position - Vector2.Normalize(projectile.velocity) * 80, projectile.width, projectile.height, 127, -projectile.velocity.X, -projectile.velocity.Y, 200, default(Color), 1f + (float)Main.rand.Next(10)/10);
            }
            if (projectile.timeLeft % 3 == 0)
            {
                Dust.NewDust(projectile.position - Vector2.Normalize(projectile.velocity) * 80, projectile.width, projectile.height, 6, -projectile.velocity.X*1.2f, -projectile.velocity.Y*1.2f, 100, default(Color), 2f + (float)Main.rand.Next(10) / 10);
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0, projectile.velocity.Y * 0, mod.ProjectileType("Explosion"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
            int shootNum = 3 + Main.rand.Next(4);
            float shootSpread = 360f;
            float spread = shootSpread * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(7f * 7f + 7f * 7f);
            double startAngle = Math.Atan2(7f, 7f) - spread / shootNum;
            double deltaAngle = spread / shootNum;
            double offsetAngle;
            int i;
            for (i = 0; i < shootNum; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("Kerbal"), projectile.damage, projectile.knockBack, projectile.owner);
            }
            Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileExplode"));
        }
	}
}

