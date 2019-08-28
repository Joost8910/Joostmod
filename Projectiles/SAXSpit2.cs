using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SAXSpit2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acidic Saliva");
		}
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 1;
            projectile.alpha = 40;
            projectile.hostile = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 300;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.timeLeft > 290)
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                return false;
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300 + Main.rand.Next(300));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300 + Main.rand.Next(300));
            if (Main.expertMode)
            {
                target.AddBuff(BuffID.OgreSpit, 100);
            }
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.98f;
            projectile.velocity.Y = (projectile.velocity.Y < 10 ? projectile.velocity.Y + 0.3f : projectile.velocity.Y);
        }
        public override void Kill(int timeLeft)
        {
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid <= 80)
            {
                Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }
        }
	}
}

