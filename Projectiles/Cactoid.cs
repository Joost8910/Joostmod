using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Cactoid : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactoid");
		}
		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 74;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.penetrate = 4;
			Projectile.timeLeft = 40;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
			Projectile.tileCollide = false;
		}
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == Mod.Find<ModNPC>("Cactoid").Type || target.type == Mod.Find<ModNPC>("Cactite").Type || target.friendly || target.lifeMax <= 5 || !target.chaseable)
            {
                return false;
            }
            else
            {
                return base.CanHitNPC(target);
            }
        }
        public override void AI()
        {
            NPC owner = Main.npc[(int)Projectile.ai[0]];
            Projectile.position = owner.Center - new Vector2(Projectile.width/2, Projectile.height/2);
        }
    }
}

