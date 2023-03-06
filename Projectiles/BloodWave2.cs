using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BloodWave2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Blood Breaker");
			Main.projFrames[Projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 50;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18;
			Projectile.extraUpdates = 1;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 12;
		}

		public override void AI()
		{
            if (Projectile.ai[0] == -1)
            {
                Projectile.rotation = 180 * 0.0174f;
            }
			if (Projectile.timeLeft == 18)
			{
                int offset = Projectile.ai[0] == -1 ? 0 : 6;
                Projectile.position.Y = Projectile.position.Y - offset;
			}
			if (Projectile.timeLeft > 15)
			{
				Projectile.frame = 1;
				Projectile.height = 24;
			}
			if (Projectile.timeLeft == 15)
			{
                int offset = Projectile.ai[0] == -1 ? 0 : 12;
                Projectile.position.Y = Projectile.position.Y - offset;
			}
			if (Projectile.timeLeft <= 15 && Projectile.timeLeft > 12)
			{
				Projectile.frame = 2;
				Projectile.height = 36;
			}
			if (Projectile.timeLeft == 12)
			{
                int offset = Projectile.ai[0] == -1 ? 1 : 13;
                Projectile.position.Y = Projectile.position.Y - offset;
			}
			if (Projectile.timeLeft <= 12 && Projectile.timeLeft > 9)
			{
				Projectile.frame = 3;
				Projectile.height = 50;
			}
			if (Projectile.timeLeft == 9)
			{
                int offset = Projectile.ai[0] == -1 ? 1 : 13;
                Projectile.position.Y = Projectile.position.Y + offset;
			}
			if (Projectile.timeLeft <= 9 && Projectile.timeLeft > 6)
			{
				Projectile.frame = 2;
				Projectile.height = 36;
			}
			if (Projectile.timeLeft == 6)
			{
                int offset = Projectile.ai[0] == -1 ? 0 : 12;
                Projectile.position.Y = Projectile.position.Y + offset;		
			}
			if (Projectile.timeLeft <= 6)
			{
				Projectile.frame = 1;
				Projectile.height = 24;
            }
            if (Projectile.timeLeft == 3)
            {
                int offset = Projectile.ai[0] == -1 ? 0 : 6;
                Projectile.position.Y = Projectile.position.Y + offset;
            }
            if (Projectile.timeLeft <= 3)
            {
                Projectile.frame = 0;
                Projectile.height = 18;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= Projectile.knockBack * target.knockBackResist;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= Projectile.knockBack;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 0;
        }
    }
}
