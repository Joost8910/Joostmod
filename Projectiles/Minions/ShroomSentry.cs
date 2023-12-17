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
			Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 80;
			Projectile.height = 97;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.sentry = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = Projectile.SentryLifeTime;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
			Projectile.aiStyle = 0;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = 12;
			height = 97;
			fallThrough = false;
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//projectile.velocity.Y = 0;
			Projectile.ai[0] += Projectile.ai[0] < 1 ? 1 : 0;
			return false;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return Projectile.ai[0] < 30 && Projectile.ai[0] >= 24 && !target.friendly;
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
			Projectile.velocity.X = 0;
	        Projectile.rotation = 0;
			Main.player[Projectile.owner].UpdateMaxTurrets();
			if (Main.rand.NextBool(4))
			{
				int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 17, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3), 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			if (Projectile.ai[0] == 6)
			{
				Projectile.frame = 1;
			}
			if (Projectile.ai[0] == 12)
			{
				Projectile.frame = 2;
			}
			if (Projectile.ai[0] == 18)
			{
				Projectile.frame = 3;
			}
			if (Projectile.ai[0] == 24)
			{
				Projectile.frame = 4;
			}
			if (Projectile.ai[0] == 30)
			{
				Projectile.frame = 5;
			}
			if (Projectile.ai[0] > 0)
			{
				Projectile.ai[0]++;
			}
			if (Projectile.ai[0] % 20 == 0 && Projectile.ai[0] > 30)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-20, 20)* 0.1f, Main.rand.Next(-20, 20) * 0.1f, ModContent.ProjectileType<Spore>(), Projectile.damage, Projectile.knockBack/5, Projectile.owner);
			}
		}
	}
}