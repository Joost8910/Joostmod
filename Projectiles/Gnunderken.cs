using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Gnunderken : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnunderson's Shuriken");
		}
		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.Shuriken;
		}
        public override void PostAI()
        {
            projectile.spriteDirection = -projectile.direction;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 10;
			height = 10;
			return true;
		}
	}
}


