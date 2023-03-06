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
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 45;
			AIType = ProjectileID.Bullet;
		}
        Vector2 playerVelocity = Vector2.Zero;
        public override void AI()
        {
            Projectile.damage = 1;
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
            if (target.type == Mod.Find<ModNPC>("Cactus Person").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("JumboCactuar").Type))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
    }
}

