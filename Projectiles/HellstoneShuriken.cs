using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HellstoneShuriken : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire Shuriken");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = false;
			projectile.penetrate = 3;
			projectile.timeLeft = 600;
			projectile.alpha = 5;
			projectile.light = 0.5f;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Shuriken;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 15;
		}
		public override void AI()
		{
            projectile.spriteDirection = -projectile.direction;
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 150 && (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 0 || Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 2))
			{
				int d = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X*0.75f, projectile.velocity.Y*0.75f, mod.ProjectileType("DousedShuriken"), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 13);
				projectile.Kill();
			}
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(24, 180);
		}

	}
}


