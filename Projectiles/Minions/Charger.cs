using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace JoostMod.Projectiles.Minions
{
	public abstract class Charger : Minion
	{
		protected float idleAccel = 0.05f;
		protected float spacingMult = 1f;
		protected float viewDist = 400f;
		protected float chaseAccel = 6f;
		protected float inertia = 40f;
        protected float chargeSpeed = 10f;
		protected int chargeCool = 30;

		public virtual void CreateDust()
		{
		}

		public virtual void SelectFrame()
		{
		}

		public override void Behavior()
		{
			Player player = Main.player[Projectile.owner];
			float spacing = (float)Projectile.width * spacingMult;
			for (int k = 0; k < 1000; k++)
			{
				Projectile otherProj = Main.projectile[k];
				if (k != Projectile.whoAmI && otherProj.active && otherProj.owner == Projectile.owner && otherProj.type == Projectile.type && System.Math.Abs(Projectile.position.X - otherProj.position.X) + System.Math.Abs(Projectile.position.Y - otherProj.position.Y) < spacing)
				{
					if (Projectile.position.X < Main.projectile[k].position.X)
					{
						Projectile.velocity.X -= idleAccel;
					}
					else
					{
						Projectile.velocity.X += idleAccel;
					}
					if (Projectile.position.Y < Main.projectile[k].position.Y)
					{
						Projectile.velocity.Y -= idleAccel;
					}
					else
					{
						Projectile.velocity.Y += idleAccel;
					}
				}
			}
			Vector2 targetPos = Projectile.position;
			float targetDist = viewDist;
			bool target = false;
			Projectile.tileCollide = true;
			if(player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(this, false))
				{
					float distance = Vector2.Distance(npc.Center, Projectile.Center);
					if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = distance;
						targetPos = npc.Center;
						target = true;
					}
				}
			}
			else for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy(this, false))
				{
					float distance = Vector2.Distance(npc.Center, Projectile.Center);
					if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = distance;
						targetPos = npc.Center;
						target = true;
					}
				}
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > (target ? 1000f : 500f))
			{
				Projectile.ai[0] = 1f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 1f)
			{
				Projectile.tileCollide = false;
			}
			if (target && Projectile.ai[0] == 0f)
			{
				Vector2 direction = targetPos - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
            }
			else
			{
				if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
				{
					Projectile.ai[0] = 1f;
				}
				float speed = 6f;
				if (Projectile.ai[0] == 1f)
				{
					speed = 15f;
				}
				Vector2 center = Projectile.Center;
				Vector2 direction = player.Center - center;
				Projectile.ai[1] = 3600f;
				Projectile.netUpdate = true;
				int num = 1;
				for (int k = 0; k < Projectile.whoAmI; k++)
				{
					if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
					{
						num++;
					}
				}
				direction.X -= (float)((10 + num * 40) * player.direction);
				direction.Y -= 70f;
				float distanceTo = direction.Length();
				if (distanceTo > 200f && speed < 9f)
				{
					speed = 9f;
				}
				if (distanceTo < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (distanceTo > 2000f)
				{
					Projectile.Center = player.Center;
				}
				if (distanceTo > 48f)
				{
					direction.Normalize();
					direction *= speed;
					float temp = inertia / 2f;
					Projectile.velocity = (Projectile.velocity * temp + direction) / (temp + 1);
				}
				else
				{
					Projectile.direction = Main.player[Projectile.owner].direction;
					Projectile.velocity *= (float)Math.Pow(0.9, 40.0 / inertia);
				}
			}
			SelectFrame();
			CreateDust();
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = (Projectile.direction = -1);
			}
			else if (Projectile.velocity.X < 0f)
			{
				Projectile.spriteDirection = (Projectile.direction = 1);
			}
			if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += 1f;
				if (Main.rand.NextBool(3))
				{
					Projectile.ai[1] += 1f;
				}
			}
			if (Projectile.ai[1] > chargeCool)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 0f)
			{
				if (target)
				{
					if ((targetPos - Projectile.Center).X > 0f)
					{
						Projectile.spriteDirection = (Projectile.direction = -1);
					}
					else if ((targetPos - Projectile.Center).X < 0f)
					{
						Projectile.spriteDirection = (Projectile.direction = 1);
					}
					if (Projectile.ai[1] == 0f)
					{
						Projectile.ai[1] = 1f;
						if (Main.myPlayer == Projectile.owner)
						{
							Projectile.velocity = Projectile.DirectionTo(targetPos) * chargeSpeed;
							Projectile.netUpdate = true;
						}
					}
				}
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = true;
			return true;
		}
	}
}