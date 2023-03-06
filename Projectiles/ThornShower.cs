using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ThornShower : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rose Weave");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 66;
			Projectile.height = 66;
			Projectile.aiStyle = 1;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.WoodenArrowFriendly;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 36;
            height = 36;
            return true;
        }
		public override void AI()
		{
            Projectile.rotation = Projectile.timeLeft * -Projectile.direction;
			if (Projectile.timeLeft % 30 == 0)
			{
				Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-5, 5) * .05f, -1f, Mod.Find<ModProjectile>("Thorn").Type, (int)(Projectile.damage * 0.75f), 0, Projectile.owner);				
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.velocity *= -1;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Projectile.velocity *= -1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * 1f, 33, (int)(Projectile.damage * 1f), Projectile.knockBack / 2, Projectile.owner);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * -1f, 33, (int)(Projectile.damage * 1f), Projectile.knockBack / 2, Projectile.owner);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11) * -1f, Main.rand.Next(-10, -5) * 1f, 33, (int)(Projectile.damage * 1f), Projectile.knockBack / 2, Projectile.owner);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11) * -1f, Main.rand.Next(-10, -5) * -1f, 33, (int)(Projectile.damage * 1f), Projectile.knockBack / 2, Projectile.owner);	
		}

	}
}

