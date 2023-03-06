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
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 90;
			Projectile.alpha = 95;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
			Projectile.coldDamage = true;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
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
            int damage = (int)(Projectile.damage * 0.5f);
            float knockback = Projectile.knockBack * 0.5f;
            for (i = 0; i < shootNum; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("Icicle").Type, damage, knockback, Projectile.owner);
            }
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.ai[1] * 6;
            if ((int)Projectile.ai[1] % 10 == 0)
            {
                int shootNum = 3;
                float shootSpread = 360f;
                float spread = shootSpread * 0.0174f;
                float baseSpeed = 3f;
                double startAngle = Projectile.rotation - spread / shootNum;
                double deltaAngle = spread / shootNum;
                double offsetAngle;
                int i;
                int damage = (int)(Projectile.damage * 0.5f);
                float knockback = Projectile.knockBack * 0.5f;
                for (i = 0; i < shootNum; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("Icicle").Type, damage, knockback, Projectile.owner);
                }
            }
            Projectile.ai[1]++;
        }

	}
}

