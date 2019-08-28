using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Needle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Needle");
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
			aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 2;
            height = 2;
            return true;
        }
    }
}
