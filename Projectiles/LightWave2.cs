using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class LightWave2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Pwnhammer");
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 50;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 18;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 12;
		}

		public override void AI()
		{
            if (projectile.ai[0] == -1)
            {
                projectile.rotation = 180 * 0.0174f;
            }
			if (projectile.timeLeft == 18)
			{
                int offset = projectile.ai[0] == -1 ? 0 : 6;
                projectile.position.Y = projectile.position.Y - offset;
			}
			if (projectile.timeLeft > 15)
			{
				projectile.frame = 1;
				projectile.height = 24;
			}
			if (projectile.timeLeft == 15)
			{
                int offset = projectile.ai[0] == -1 ? 0 : 12;
                projectile.position.Y = projectile.position.Y - offset;
			}
			if (projectile.timeLeft <= 15 && projectile.timeLeft > 12)
			{
				projectile.frame = 2;
				projectile.height = 36;
			}
			if (projectile.timeLeft == 12)
			{
                int offset = projectile.ai[0] == -1 ? 1 : 13;
                projectile.position.Y = projectile.position.Y - offset;
			}
			if (projectile.timeLeft <= 12 && projectile.timeLeft > 9)
			{
				projectile.frame = 3;
				projectile.height = 50;
			}
			if (projectile.timeLeft == 9)
			{
                int offset = projectile.ai[0] == -1 ? 1 : 13;
                projectile.position.Y = projectile.position.Y + offset;
			}
			if (projectile.timeLeft <= 9 && projectile.timeLeft > 6)
			{
				projectile.frame = 2;
				projectile.height = 36;
			}
			if (projectile.timeLeft == 6)
			{
                int offset = projectile.ai[0] == -1 ? 0 : 12;
                projectile.position.Y = projectile.position.Y + offset;		
			}
			if (projectile.timeLeft <= 6)
			{
				projectile.frame = 1;
				projectile.height = 24;
            }
            if (projectile.timeLeft == 3)
            {
                int offset = projectile.ai[0] == -1 ? 0 : 6;
                projectile.position.Y = projectile.position.Y + offset;
            }
            if (projectile.timeLeft <= 3)
            {
                projectile.frame = 0;
                projectile.height = 18;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= projectile.knockBack * target.knockBackResist;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= projectile.knockBack;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 0;
        }
    }
}
