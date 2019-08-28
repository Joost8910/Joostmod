using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Leech : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leechy Boi");
		}
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			aiType = ProjectileID.Bullet;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            float lifeStoled = damage * 0.05f;
            if (lifeStoled < 1)
            {
                lifeStoled = 1;
            }
            if ((int)lifeStoled > 0 && !player.moonLeech && target.type != NPCID.TargetDummy && target.life > 2)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
            }
            Main.PlaySound(3, player.Center, 9);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            float lifeStoled = damage * 0.05f;
            if (lifeStoled < 1)
            {
                lifeStoled = 1;
            }
            if ((int)lifeStoled > 0 && !player.moonLeech)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
            }
            Main.PlaySound(3, player.Center, 9);
        }
        public override void AI()
        {
            if (projectile.timeLeft > 298)
            {
                Player player = Main.player[projectile.owner];
                if (player.HasMinionAttackTargetNPC)
                {
                    NPC target = Main.npc[player.MinionAttackTargetNPC];
                    if (Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
                    {
                        projectile.velocity = projectile.DirectionTo(target.Center) * 10;
                    }
                }
                else
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        if (!target.friendly && target.type != 488 && !target.dontTakeDamage && target.active && Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1) && target.life > 5)
                        {
                            projectile.velocity = projectile.DirectionTo(target.Center) * 10;
                        }
                    }
                }
            }
        }

    }
}

