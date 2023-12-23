using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class EmberMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember");
			Main.projFrames[Projectile.type] = 5;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 40;
			Projectile.friendly = true;
			Main.projPet[Projectile.type] = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
			inertia = 20f;
            shootCool = 110f;
            shoot = ModContent.ProjectileType<FlameSummon>();
			shootSpeed = 9f;
			chaseDist = 70f;
		}
		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.emberMinion = false;
			}
			if (modPlayer.emberMinion)
			{
				Projectile.timeLeft = 2;
			}
		}
		public override void CreateDust()
		{
			if (Main.rand.Next(5) == 0)
			{	
				int dustGen = Main.rand.Next(3);
                int dustType = 158;
                if (dustGen == 1)
                {
                    dustType = 6;
                }
                if (dustGen == 2)
                {
                    dustType = 127;
                }
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
			}
        }
        public override void SelectFrame(Microsoft.Xna.Framework.Vector2 dir)
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 5;
			}
		}
	}
}


