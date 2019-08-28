using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Toad : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toad");
			Main.projFrames[projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 13;
			projectile.aiStyle = 63;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 300;
			aiType = ProjectileID.BabySpider;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(20, 180);
		}
			bool start = false;
			int color = 0;
		public void Initialize()
		{
			color = Main.rand.Next(8);
			start = true;	
		}
		public override void AI()
		{
			if (!start)
			{
				Initialize();
			}
			projectile.frame = color;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.75f;
			}
			return false;
		}
	}
}
