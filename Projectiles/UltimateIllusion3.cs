using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UltimateIllusion3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultimate Illusion");
		}
		public override void SetDefaults()
		{
			Projectile.width = 164;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 5;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle((int)Projectile.position.X + 38, (int)Projectile.position.Y, 86, 176);
        }
        public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 50, 0, 0, Mod.Find<ModProjectile>("UltimateIllusion4").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);				
		}

	}
}
