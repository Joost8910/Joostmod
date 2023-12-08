using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class Cactuar : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar");
			Main.projFrames[Projectile.type] = 2;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targetting
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 48;
			Projectile.height = 59;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            inertia = 20f;
			chaseAccel = 7f;
			chaseDist = 10f;
			shootCool = 200f;
			shoot = Mod.Find<ModProjectile>("Needle7").Type;
			shootSpeed = 15f;
            predict = true;
            grounded = true;
            rapidAmount = 10;
        }

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.cactuarMinions = false;
			}
			if (modPlayer.cactuarMinions)
			{
				Projectile.timeLeft = 2;
			}
		}
		public override void FlyingDust()
		{
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 93, 0f, 0f, 0, default(Color), 0.7f);
		}
        public override void ShootEffects()
        {
            Projectile.frame = (Projectile.frame + 1) % 2;
            SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);
        }
        public override void SelectFrame(Vector2 tPos)
        {
			Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X*2.5f);
			if (Projectile.frameCounter >= 65)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 2;
			}
		}
    }
}