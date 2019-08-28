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
			projectile.width = 200;
			projectile.height = 600;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
			projectile.light = 0.95f;
			projectile.ignoreWater = true;
		}
        public override void AI()
        {
            if (projectile.timeLeft == 30)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        Dust.NewDust(new Vector2((int)projectile.position.X + (i * 40), (int)projectile.position.Y + (j * 40)), 8, 8, Main.rand.Next(2), 0, 0, 0, default(Color), 1f + (Main.rand.Next(30) / 10f));
                    }
                }
            }
        }
    }
}
