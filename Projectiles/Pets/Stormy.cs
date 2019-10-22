using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Pets
{
	public class Stormy : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormy");
			Main.projFrames[projectile.type] = 34;
			Main.projPet[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Puppy);
			aiType = ProjectileID.Puppy;
            projectile.width = 86;
            projectile.height = 54;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.puppy = false; // Relic from aiType
			return false;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 56;
            height = 56;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
            if (!player.active || player.dead)
            {
                modPlayer.stormy = false;
                projectile.active = false;
            }
            if (modPlayer.stormy)
            {
                projectile.timeLeft = 2;
            }
            int dir = 1;
            if (player.Center.X < projectile.Center.X)
            {
                dir = -1;
            }
            projectile.tileCollide = true;
            if (Vector2.Distance(player.Center, projectile.Center) > 1000f)
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            Vector2 playerPos = player.MountedCenter;
            if (projectile.ai[0] == 1f && (Vector2.Distance(playerPos, projectile.Center) > 200 || !Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1)))
            {
                projectile.tileCollide = false;
                if (projectile.velocity.Y >= 0 && player.position.Y < projectile.position.Y)
                {
                    projectile.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(player.position.Y - (projectile.position.Y + projectile.height)));
                }
                projectile.velocity.X = (projectile.Distance(player.Center) / 30) * dir;
            }
            if (projectile.velocity.X > 0f)
            {
                projectile.direction = 1;
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.direction = -1;
            }
            else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f && Vector2.Distance(playerPos, projectile.Center) > 80)
            {
                projectile.velocity.Y -= 4f;
            }

            if (projectile.velocity.Y == 0)
            {
                projectile.frameCounter++;
                if (Math.Abs(projectile.velocity.X) > 0f)
                {
                    if (projectile.frame > 16)
                    {
                        if (projectile.frameCounter >= 2)
                        {
                            projectile.frame--;
                            projectile.frameCounter = 0;
                        }
                        if (projectile.frame > 25)
                        {
                            projectile.frame = 25;
                        }
                        projectile.position.X -= projectile.velocity.X;
                    }
                    else
                    {
                        if (Math.Abs(projectile.velocity.X) <= 2)
                        {
                            if (projectile.frameCounter * Math.Abs(projectile.velocity.X) >= 4)
                            {
                                projectile.frame++;
                                if (projectile.frame > 7)
                                {
                                    projectile.frame = 0;
                                }
                                projectile.frameCounter = 0;
                            }
                        }
                        else
                        {
                            if (projectile.frameCounter >= 6)
                            {
                                projectile.frame++;
                                if (projectile.frame > 15 || projectile.frame < 8)
                                {
                                    projectile.frame = 8;
                                }
                                projectile.frameCounter = 0;
                            }
                        }
                    }
                }
                else
                {
                    bool facing = projectile.direction != player.direction && ((player.Center.X < projectile.Center.X - 20 && projectile.direction == -1) || (player.Center.X > projectile.Center.X + 20 && projectile.direction == 1));
                    if (projectile.frameCounter >= ((projectile.frame >= 25) ? (facing ? 4 : 6) : 2))
                    {
                        if (facing && projectile.Distance(player.Center) < 60)
                        {
                            player.AddBuff(BuffID.Regeneration, 10);
                        }
                        projectile.frameCounter = 0;
                        projectile.frame++;
                        if (projectile.frame < 16)
                        {
                            projectile.frame = 16;
                        }
                        if (projectile.frame > 33)
                        {
                            projectile.frame = 25;
                        }
                    }
                }
            }
            else if (projectile.velocity.Y < 0)
            {
                projectile.frame = 9;
            }
            else
            {
                projectile.frame = 11;
            }

            projectile.spriteDirection = projectile.direction;
            projectile.velocity.Y += 0.3f;
            float inertia = 40f;
            float distanceTo = projectile.Distance(player.Center);
            if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1) && distanceTo >= 200f)
            {
                projectile.ai[0] = 1f;
            }
            float speed = distanceTo / 50;
            if (speed > 2)
            {
                speed = 2;
            }
            if (speed < 0.2f)
            {
                speed = 0;
            }
            if (distanceTo > 200)
            {
                speed = (distanceTo / 20) - 8;
            }

            projectile.netUpdate = true;
            if (distanceTo < 200f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
            }
            if (distanceTo > 2000f)
            {
                projectile.active = false;
            }
            if (distanceTo > 60f)
            {
                Vector2 direction = projectile.DirectionTo(player.Center);
                direction.Normalize();
                direction *= speed;
                float temp = inertia / 2f;
                projectile.velocity.X = (projectile.velocity.X * temp + direction.X) / (temp + 1);
                if (Math.Abs(direction.X) < 0.2f && Math.Abs(projectile.velocity.X) < 0.2f)
                {
                    projectile.velocity.X = 0;
                }
            }
            else
            {
                projectile.velocity.X = 0;
            }
        }
	}
}