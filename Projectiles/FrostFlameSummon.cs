using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FrostFlameSummon : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FrostFlame");
		}
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.ignoreWater = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 250;
			projectile.alpha = 35;
			projectile.light = 0.15f;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Shuriken;
			projectile.coldDamage = true;
		}
		public override void AI()
		{
			if(Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80 && (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 0 || Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 2))
			{
				projectile.Kill();
			}
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(44, 60);
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("FrostFlame2Summon"), projectile.damage, 0, projectile.owner);	
		}

	}
}


