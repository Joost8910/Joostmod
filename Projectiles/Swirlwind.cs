using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Swirlwind : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hurricane");
		}
        public override void SetDefaults()
        {
            projectile.width = 360;
            projectile.height = 360;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.minion = true;
		    projectile.alpha = 255;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 12;
            projectile.ignoreWater = true;
            drawHeldProjInFrontOfHeldItemAndArms = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed && player.CheckMana(1, true);
            if (channeling)
            {
                if (projectile.ai[0] <= 1)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
                        vector13.Normalize();
                        if (vector13.HasNaNs())
                        {
                            vector13 = Vector2.UnitX * (float)player.direction;
                        }
                        if (vector13.X > 0)
                        {
                            projectile.direction = (int)player.gravDir;
                            projectile.netUpdate = true;
                        }
                        else
                        {
                            projectile.direction = -(int)player.gravDir;
                            projectile.netUpdate = true;
                        }
                    }
                }
                projectile.ai[0]++;
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.ai[0] % (int)(20 - projectile.ai[1]) <= 0)
            {
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 7, 1.5f, -0.5f);
            }
            if (projectile.ai[0] % 2 == 0 && projectile.ai[1] < 12)
            {
                projectile.ai[1]++;
            }
            projectile.alpha = 255 - (int)(projectile.ai[1] * 12);
            projectile.idStaticNPCHitCooldown = 24 - (int)projectile.ai[1];
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.velocity.X = projectile.direction;
            projectile.velocity.Y = 0;
            projectile.rotation = projectile.ai[0] * projectile.direction * projectile.ai[1] * 0.625f * 0.0174f;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (projectile.Hitbox.Intersects(I.Hitbox))
                    {
                        I.velocity = I.DirectionTo(projectile.Center) * 5;
                        I.position += I.velocity;
                    }
                }
            }
            for (int n = 0; n < 200; n++)
            {
                NPC target = Main.npc[n];
                if (target.Distance(projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                {
                    bool tooClose = player.Distance(target.Center) <= (target.width > target.height ? target.width : target.height) + 40;
                    if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
                    {
                        Vector2 vel = target.DirectionTo(projectile.Center) * projectile.ai[1] * 0.625f;
                        vel = vel.RotatedBy(90f * -projectile.direction);
                        if (tooClose)
                        {
                            vel -= target.DirectionTo(projectile.Center) * 4f;
                        }
                        else
                        {
                            vel += target.DirectionTo(projectile.Center) * 4f;
                        }
                        if (!target.noGravity)
                            vel.Y -= 0.3f;
                        target.velocity = vel + player.velocity;
                    }
                }
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.Distance(projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                return base.CanHitNPC(target);
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (target.Distance(projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                return base.CanHitPvp(target);
            return false;
        }

    }
}