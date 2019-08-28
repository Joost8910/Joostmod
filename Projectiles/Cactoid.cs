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
			projectile.width = 28;
			projectile.height = 74;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 4;
			projectile.timeLeft = 40;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
			projectile.tileCollide = false;
		}
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Cactoid") || target.type == mod.NPCType("Cactite") || target.damage == 0)
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
            NPC owner = Main.npc[(int)projectile.ai[0]];
            projectile.position = owner.Center - new Vector2(projectile.width/2, projectile.height/2);
        }
    }
}

