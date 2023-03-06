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
			Projectile.width = 40;
			Projectile.height = 18;
			Projectile.aiStyle = 27;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
		}

	}
}

