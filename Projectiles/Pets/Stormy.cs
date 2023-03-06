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
			Main.projFrames[Projectile.type] = 34;
			Main.projPet[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Puppy);
			AIType = ProjectileID.Puppy;
            Projectile.width = 86;
            Projectile.height = 54;
            Projectile.scale = 0.75f;
        }

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.puppy = false; // Relic from aiType
			return false;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 56;
            height = 54;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(Mod, "JoostPlayer");
            if (!player.active || player.dead)
            {
                modPlayer.stormy = false;
                Projectile.active = false;
            }
            if (modPlayer.stormy)
            {
                Projectile.timeLeft = 2;
            }
            int dir = 1;
            if (player.Center.X < Projectile.Center.X)
            {
                dir = -1;
            }
            Projectile.tileCollide = true;
            if (Vector2.Distance(player.Center, Projectile.Center) > 1000f)
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            Vector2 playerPos = player.MountedCenter;
            if (Projectile.ai[0] == 1f && (Vector2.Distance(playerPos, Projectile.Center) > 200 || !Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1)))
            {
                Projectile.tileCollide = false;
                if (Projectile.velocity.Y >= 0 && player.position.Y < Projectile.position.Y)
                {
                    Projectile.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(player.position.Y - (Projectile.position.Y + Projectile.height)));
                }
                Projectile.velocity.X = (Projectile.Distance(player.Center) / 30) * dir;
            }
            if (Projectile.velocity.X > 0f)
            {
                Projectile.direction = 1;
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.direction = -1;
            }
            else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f && Vector2.Distance(playerPos, Projectile.Center) > 80)
            {
                Projectile.velocity.Y -= 4f;
            }

            if (Projectile.velocity.Y == 0)
            {
                Projectile.frameCounter++;
                if (Math.Abs(Projectile.velocity.X) > 0f)
                {
                    if (Projectile.frame > 16)
                    {
                        if (Projectile.frameCounter >= 2)
                        {
                            Projectile.frame--;
                            Projectile.frameCounter = 0;
                        }
                        if (Projectile.frame > 25)
                        {
                            Projectile.frame = 25;
                        }
                        Projectile.position.X -= Projectile.velocity.X;
                    }
                    else
                    {
                        if (Math.Abs(Projectile.velocity.X) <= 2)
                        {
                            if (Projectile.frameCounter * Math.Abs(Projectile.velocity.X) >= 4)
                            {
                                Projectile.frame++;
                                if (Projectile.frame > 7)
                                {
                                    Projectile.frame = 0;
                                }
                                Projectile.frameCounter = 0;
                            }
                        }
                        else
                        {
                            if (Projectile.frameCounter >= 6)
                            {
                                Projectile.frame++;
                                if (Projectile.frame > 15 || Projectile.frame < 8)
                                {
                                    Projectile.frame = 8;
                                }
                                Projectile.frameCounter = 0;
                            }
                        }
                    }
                }
                else
                {
                    bool facing = Projectile.direction != player.direction && ((player.Center.X < Projectile.Center.X - 20 && Projectile.direction == -1) || (player.Center.X > Projectile.Center.X + 20 && Projectile.direction == 1));
                    if (Projectile.frameCounter >= ((Projectile.frame >= 25) ? (facing ? 4 : 6) : 2))
                    {
                        if (facing && Projectile.Distance(player.Center) < 60)
                        {
                            player.AddBuff(BuffID.Regeneration, 10);
                        }
                        Projectile.frameCounter = 0;
                        Projectile.frame++;
                        if (Projectile.frame < 16)
                        {
                            Projectile.frame = 16;
                        }
                        if (Projectile.frame > 33)
                        {
                            Projectile.frame = 25;
                        }
                    }
                }
            }
            else if (Projectile.velocity.Y < 0)
            {
                Projectile.frame = 9;
            }
            else
            {
                Projectile.frame = 11;
            }

            Projectile.spriteDirection = Projectile.direction;
            Projectile.velocity.Y += 0.3f;
            float inertia = 40f;
            float distanceTo = Projectile.Distance(player.Center);
            if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1) && distanceTo >= 200f)
            {
                Projectile.ai[0] = 1f;
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

            Projectile.netUpdate = true;
            if (distanceTo < 200f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[0] = 0f;
                Projectile.netUpdate = true;
            }
            if (distanceTo > 2000f)
            {
                Projectile.active = false;
            }
            if (distanceTo > 60f)
            {
                Vector2 direction = Projectile.DirectionTo(player.Center);
                direction.Normalize();
                direction *= speed;
                float temp = inertia / 2f;
                Projectile.velocity.X = (Projectile.velocity.X * temp + direction.X) / (temp + 1);
                if (Math.Abs(direction.X) < 0.2f && Math.Abs(Projectile.velocity.X) < 0.2f)
                {
                    Projectile.velocity.X = 0;
                }
            }
            else
            {
                Projectile.velocity.X = 0;
            }
        }
	}
}