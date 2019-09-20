using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GilgNaginata1 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh's Naginata");
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 26;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 40;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.timeLeft > 25)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                target.velocity.Y += projectile.knockBack;
            }
        }
        public override void AI()
        {
			NPC host = Main.npc[(int)projectile.ai[0]];
            Vector2 arm = host.Center + new Vector2(16 * host.direction, -16);
            projectile.position = arm - new Vector2(projectile.width/2, projectile.height/2);
			projectile.position += projectile.velocity * projectile.ai[1];
            projectile.direction = host.direction;
            projectile.spriteDirection = -projectile.direction;
            if (projectile.timeLeft == 25)
            {
                Main.PlaySound(SoundID.Item1, projectile.Center);
            }
            if (projectile.timeLeft > 25)
            {
                projectile.velocity = Vector2.Normalize(Main.player[host.target].MountedCenter - arm) * projectile.velocity.Length();
                projectile.ai[1] -= 0.2f;
            }
			else if (projectile.timeLeft < 15)
			{
				projectile.ai[1] -= 0.4f;
			}
			else
			{
				projectile.ai[1] += 0.7f;
			}
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            if (!host.active || host.life <= 0)
            {
                projectile.Kill();
            }
        }
	}
}
