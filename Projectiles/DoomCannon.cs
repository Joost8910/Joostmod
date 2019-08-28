using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DoomCannon : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Cannon");
			Main.projFrames[projectile.type] = 12;
		}
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Main.myPlayer == projectile.owner && Main.mouseRight)
            {
                projectile.ai[0] = 0;
                projectile.netUpdate = true;
                projectile.netUpdate2 = true;
                projectile.Kill();
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 dir = Main.MouseWorld - playerPos;
                    projectile.direction = dir.X > 0 ? 1 : -1;
                    if (Vector2.Distance(Main.MouseWorld, playerPos) > 40)
                    {
                        dir = Main.MouseWorld - projectile.Center;
                    }
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    dir *= scaleFactor6;
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = dir;
                } 
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.ai[0] < 60)
            {
                projectile.frame = 0;
            }
            else if (projectile.ai[0] < 120)
            {
                projectile.frame = 1;
            }
            else if (projectile.ai[0] < 180)
            {
                projectile.frame = 2;
            }
            else if (projectile.ai[0] < 240)
            {
                projectile.frame = 3;
            }
            else if (projectile.ai[0] < 300)
            {
                projectile.frame = 4;
            }
            else if (projectile.ai[0] < 360)
            {
                projectile.frame = 5;
                player.velocity.X *= 0.99f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.99f;
                }
            }
            else if (projectile.ai[0] < 420)
            {
                projectile.frame = 6;
                player.velocity.X *= 0.975f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.985f;
                }
            }
            else if (projectile.ai[0] < 480)
            {
                projectile.frame = 7;
                player.velocity.X *= 0.96f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.96f;
                }
            }
            else if (projectile.ai[0] < 540)
            {
                projectile.frame = 8;
                player.velocity.X *= 0.945f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.945f;
                }
            }
            else if (projectile.ai[0] < 600)
            {
                projectile.frame = 9;
                player.velocity.X *= 0.93f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.93f;
                }
            }
            else if (projectile.ai[0] < 660)
            {
                projectile.frame = 10;
                player.velocity.X *= 0.915f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.915f;
                }
            }
            else
            {
                player.velocity.X *= 0.9f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.9f;
                }
                Vector2 vel = Vector2.Normalize(projectile.velocity);
                if (Math.Abs(vel.X) < 0.15f)
                {
                    projectile.velocity.X = 0;
                }
                if (Math.Abs(vel.Y) < 0.15f)
                {
                    projectile.velocity.Y = 0;
                }
                projectile.frame = 11;
                int dust2 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 261, 5, -3 * player.gravDir, 0, default(Color), 1);
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 261, -5, -3 * player.gravDir, 0, default(Color), 1);
                Main.dust[dust3].noGravity = true;
            }
            if (projectile.ai[0] <= 660)
            {
                projectile.ai[0]++;
            }
            if (projectile.ai[0] == 660)
            {
                Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 217);
            }
            if (projectile.ai[0] % 60 == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 75);
                if (projectile.ai[0] >= 360)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 114);
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.position = playerPos - projectile.Size / 2 + new Vector2(-14, 0) + new Vector2((float)Math.Cos(projectile.rotation - (Math.PI / 2)), (float)Math.Sin(projectile.rotation - (Math.PI / 2))) * 14;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = MathHelper.WrapAngle(projectile.rotation);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner && !Main.mouseRight && projectile.ai[0] >= 60)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
                Vector2 pos = projectile.Center + projectile.velocity * 26;
                float speed = 10;
                float mult = 1;
                int type = mod.ProjectileType("DoomSkull");
                if (projectile.ai[0] < 120)
                {
                    speed = 10;
                    mult = 1;
                }
                else if (projectile.ai[0] < 180)
                {
                    speed = 11;
                    mult = 2;
                }
                else if (projectile.ai[0] < 240)
                {
                    speed = 12;
                    mult = 3;
                }
                else if (projectile.ai[0] < 300)
                {
                    speed = 13;
                    mult = 4;
                }
                else if (projectile.ai[0] < 360)
                {
                    speed = 14;
                    mult = 5;
                }
                else if (projectile.ai[0] < 420)
                {
                    type = mod.ProjectileType("DoomSkull2");
                    speed = 15;
                    mult = 6;
                }
                else if (projectile.ai[0] < 480)
                {
                    type = mod.ProjectileType("DoomSkull2");
                    speed = 16;
                    mult = 7;
                }
                else if (projectile.ai[0] < 540)
                {
                    type = mod.ProjectileType("DoomSkull2");
                    speed = 17;
                    mult = 8;
                }
                else if (projectile.ai[0] < 600)
                {
                    type = mod.ProjectileType("DoomSkull2");
                    speed = 18;
                    mult = 9;
                }
                else if (projectile.ai[0] < 660)
                {
                    type = mod.ProjectileType("DoomSkull2");
                    speed = 19;
                    mult = 10;
                }
                else
                {
                    type = mod.ProjectileType("DoomSkull3");
                    speed = 7;
                    mult = 11;
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
                    pos = projectile.Center + projectile.velocity * 140;
                }
                if (float.IsNaN(projectile.velocity.X) || float.IsNaN(projectile.velocity.Y))
                {
                    projectile.velocity = -Vector2.UnitY;
                }
                Projectile.NewProjectile(pos, projectile.velocity * speed, type, (int)(projectile.damage * mult), projectile.knockBack * mult, projectile.owner);
            }
        }
    }
}