using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class NegativeThousandDegreeKnife : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("-1000'C Degrees Knife");
		}
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 18;
			projectile.aiStyle = 27;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}

	}
}

