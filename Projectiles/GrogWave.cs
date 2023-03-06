using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrogWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
		}
		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 1;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 100;
			Projectile.tileCollide = false;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
		public override void AI()
        {
            if (Projectile.localAI[0] < 10)
            {
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[0] = 10;
            }
            Projectile.scale = Projectile.timeLeft * 0.02f;
            Projectile.position.X += Projectile.scale * 16 * Projectile.spriteDirection;
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.position.Y -= 16 * Projectile.scale;
            }
            //int x = 8 + (int)(projectile.position.X/16)*16;
			//if (x != projectile.localAI[1])
			//{
				Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, 0, 15f, Mod.Find<ModProjectile>("GrogWave1").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.spriteDirection, Projectile.scale);			
				//projectile.localAI[1] = x;
			//}
		}
	}
}
