using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class CactusNeedle2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Needle");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 45;
			aiType = ProjectileID.Bullet;
		}
        Vector2 playerVelocity = Vector2.Zero;
        public override void AI()
        {
            projectile.damage = 1;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = 1;
            playerVelocity = target.velocity;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
		{
            if (!player.dead)
            {
                player.immuneTime = 1;
                player.velocity = playerVelocity;
            }
		}
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Cactus Person") && !NPC.AnyNPCs(mod.NPCType("JumboCactuar")))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
    }
}

