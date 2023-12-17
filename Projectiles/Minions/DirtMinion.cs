using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class DirtMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soil Spirit");
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
			Projectile.height = 30;
			Projectile.friendly = true;
			Main.projPet[Projectile.type] = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
			inertia = 30f;
            shootCool = 150f;
            shoot = ModContent.ProjectileType<DirtBoltSummon>();
			shootSpeed = 5.5f;
			chaseDist = 150f;
		}
		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.dirtMinion = false;
			}
			if (modPlayer.dirtMinion)
			{
				Projectile.timeLeft = 2;
			}
		}
		public override void CreateDust()
		{
			if (Main.rand.Next(8) == 0)
			{	
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0);
			}
		}
		public override void SelectFrame(Microsoft.Xna.Framework.Vector2 dir)
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}
		}
	}
}


