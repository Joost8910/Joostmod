using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BoneHurtingJuice : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bone Hurting Juice");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 200;
			aiType = ProjectileID.WoodenArrowFriendly;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("BoneHurt"), 600, false);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("BoneHurt"), 600, false);
        }
    }
}

