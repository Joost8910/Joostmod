using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Flamethrown : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame");
		}
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.ignoreWater = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 250;
			Projectile.alpha = 35;
			Projectile.light = 0.15f;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Shuriken;
		}
		public override void AI()
		{
			if(Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80 && (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
			{
				Projectile.Kill();
			}
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("Flame2thrown").Type, Projectile.damage, 0, Projectile.owner);	
		}

	}
}


