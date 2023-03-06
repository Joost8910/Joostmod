using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UltimateIllusion5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultimate Illusion");
		}
		public override void SetDefaults()
		{
			Projectile.width = 200;
			Projectile.height = 600;
			Projectile.aiStyle = 0;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30;
			Projectile.tileCollide = false;
			Projectile.light = 0.95f;
			Projectile.ignoreWater = true;
		}
        public override void AI()
        {
            if (Projectile.timeLeft == 30)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        Dust.NewDust(new Vector2((int)Projectile.position.X + (i * 40), (int)Projectile.position.Y + (j * 40)), 8, 8, Main.rand.Next(2), 0, 0, 0, default(Color), 1f + (Main.rand.Next(30) / 10f));
                    }
                }
            }
        }
    }
}
