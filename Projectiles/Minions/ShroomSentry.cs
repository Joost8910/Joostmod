using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class ShroomSentry : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroom Sentry");
			Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 80;
			projectile.height = 97;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.sentry = true;
			projectile.penetrate = -1;
			projectile.timeLeft = Projectile.SentryLifeTime;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
			projectile.aiStyle = 0;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 12;
			height = 97;
			fallThrough = false;
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//projectile.velocity.Y = 0;
			projectile.ai[0] += projectile.ai[0] < 1 ? 1 : 0;
			return false;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return projectile.ai[0] < 30 && projectile.ai[0] >= 24 && !target.friendly;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			if (n.active && !n.friendly && !n.dontTakeDamage && n.type != 488)
			{
				n.velocity.Y = -2.5f * knockback * n.knockBackResist;
			}
		}
		public override void AI()
		{
			projectile.velocity.X = 0;
	        projectile.rotation = 0;
			Main.player[projectile.owner].UpdateMaxTurrets();
			if (Main.rand.Next(4) == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 17, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3), 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			if (projectile.ai[0] == 6)
			{
				projectile.frame = 1;
			}
			if (projectile.ai[0] == 12)
			{
				projectile.frame = 2;
			}
			if (projectile.ai[0] == 18)
			{
				projectile.frame = 3;
			}
			if (projectile.ai[0] == 24)
			{
				projectile.frame = 4;
			}
			if (projectile.ai[0] == 30)
			{
				projectile.frame = 5;
			}
			if (projectile.ai[0] > 0)
			{
				projectile.ai[0]++;
			}
			if (projectile.ai[0] % 20 == 0 && projectile.ai[0] > 30)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-20, 20)* 0.1f, Main.rand.Next(-20, 20) * 0.1f, mod.ProjectileType("Spore"), projectile.damage, projectile.knockBack/5, projectile.owner);
			}
		}
	}
}