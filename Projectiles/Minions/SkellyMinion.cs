using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class SkellyMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skelly");
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 30;
			Projectile.height = 31;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			inertia = 17f;
			shoot = Mod.Find<ModProjectile>("Bone").Type;
			shootSpeed = 14f;
			shootCool = 80f;
            grounded = true;
        }
		public override void FlyingDust()
		{
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 111, 0f, 0f, 0, default(Color), 0.7f);
		}
		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.Skelly = false;
			}
			if (modPlayer.Skelly)
			{
				Projectile.timeLeft = 2;
			}
		}
		public override void SelectFrame(Vector2 tPos)
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 5 && Projectile.ai[0] != 1f && Math.Abs(Projectile.velocity.X) > 0.1f)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}
			if (Projectile.ai[0] == 1f)
			{
				Projectile.frame = 1;
			}
			else if (Math.Abs(Projectile.velocity.X) <= 0.1f)
			{
				Projectile.frame = 2;
			}
		}
	}
}


