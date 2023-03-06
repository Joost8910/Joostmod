using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.aiStyle = -1;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 40;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.timeLeft > 25)
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
                target.velocity.Y += Projectile.knockBack;
            }
        }
        public override void AI()
        {
			NPC host = Main.npc[(int)Projectile.ai[0]];
            Vector2 arm = host.Center + new Vector2(16 * host.direction, -16);
            Projectile.position = arm - new Vector2(Projectile.width/2, Projectile.height/2);
			Projectile.position += Projectile.velocity * Projectile.ai[1];
            Projectile.direction = host.direction;
            Projectile.spriteDirection = -Projectile.direction;
            if (Projectile.timeLeft == 25)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
            }
            if (Projectile.timeLeft > 25)
            {
                Projectile.velocity = Vector2.Normalize(Main.player[host.target].MountedCenter - arm) * Projectile.velocity.Length();
                Projectile.ai[1] -= 0.2f;
            }
			else if (Projectile.timeLeft < 15)
			{
				Projectile.ai[1] -= 0.4f;
			}
			else
			{
				Projectile.ai[1] += 0.7f;
			}
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
            if (!host.active || host.life <= 0)
            {
                Projectile.Kill();
            }
        }
	}
}
