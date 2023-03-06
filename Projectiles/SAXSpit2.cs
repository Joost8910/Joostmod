using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            Projectile.alpha = 40;
            Projectile.hostile = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 300;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 290)
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
            Projectile.velocity.X *= 0.98f;
            Projectile.velocity.Y = (Projectile.velocity.Y < 10 ? Projectile.velocity.Y + 0.3f : Projectile.velocity.Y);
        }
        public override void Kill(int timeLeft)
        {
            if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount <= 80)
            {
                SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.position);
            }
        }
	}
}

