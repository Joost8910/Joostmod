using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EggSack : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Kraken);
			projectile.width = 22;
			projectile.height = 22;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Egg Sack");
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 20;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 300f;
		}
public override void AI()
{
	int SPIDERS = Main.rand.Next(1,37);
              if(SPIDERS > 35f) 
           {
                 
               Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, 379, (int)(projectile.damage * 0.5f), 0, projectile.owner, 0f, 0f); //Spawning a projectile

           }
}

	}
}


