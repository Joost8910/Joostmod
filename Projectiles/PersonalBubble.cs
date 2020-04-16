using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class PersonalBubble : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Personal Bubble");
		}
		public override void SetDefaults()
		{
			projectile.width = 56;
			projectile.height = 56;
			projectile.aiStyle = 0;
            projectile.friendly = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 4;
			projectile.alpha = 50;
			projectile.extraUpdates = 1;
            drawHeldProjInFrontOfHeldItemAndArms = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;
		}
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        /*
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.Center.X < target.Center.X)
            {
                hitDirection = 1;
            }
            else
            {
                hitDirection = -1;
            }
        }
        */
        public override void AI()
		{
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead && player.GetModPlayer<JoostPlayer>().waterBubble)
            {
                projectile.position.X = player.MountedCenter.X - projectile.width / 2;
                projectile.position.Y = player.MountedCenter.Y - projectile.height / 2;
                projectile.velocity.Y = 0;
                projectile.rotation = 0;
                player.heldProj = projectile.whoAmI;
                projectile.timeLeft = 2;
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (target.active && projectile.Hitbox.Intersects(target.Hitbox))
                    {
                        int dir = 0;
                        if (target.Center.X > projectile.Center.X)
                        {
                            dir = 1;
                        }
                        if (target.Center.X < projectile.Center.X)
                        {
                            dir = -1;
                        }
                        int dirY = 0;
                        if (target.Center.Y > projectile.Center.Y)
                        {
                            dirY = 1;
                        }
                        if (target.Center.Y < projectile.Center.Y)
                        {
                            dirY = -1;
                        }
                        target.wet = true;
                        if (target.wetCount != 0)
                        {
                            target.wetCount++;
                        }
                        if (target.knockBackResist > 0)
                        {
                            target.velocity.X += dir;
                            target.velocity.Y += dirY;
                        }
                    }
                }
            }
        }

	}
}

