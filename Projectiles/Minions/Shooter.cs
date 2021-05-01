using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace JoostMod.Projectiles.Minions
{
	public abstract class Shooter : Minion
	{
		protected float idleAccel = 0.05f;
		protected float spacingMult = 1f;
		protected float viewDist = 600f;
		protected float chaseDist = 200f;
		protected float chaseAccel = 6f;
		protected float inertia = 40f;
		protected float shootCool = 90f;
		protected float shootSpeed;
		protected int shoot;
		protected int shootNum = 1;
		protected float shootSpread = 0;
        protected bool predict = false;
        protected int rapidAmount = 1;
        protected int rapidRate = 1;
        protected bool noCollide = false;
        protected bool grounded = false;
        protected bool jump = false;
        protected float damageMult = 1f;
        protected float shootAI0 = 0;
        protected float shootAI1 = 0;
        protected float maxShootDist = -1;

        protected bool fallThroughPlat = true;

        public virtual void ShootEffects()
        {
        }

        public virtual void FlyingDust()
        {

        }
        
        public virtual void CreateDust()
		{
		}

		public virtual void SelectFrame(Vector2 tPos)
		{
		}

		public override void Behavior()
		{
			Player player = Main.player[projectile.owner];
			float spacing = (float)projectile.width * spacingMult;
			for (int k = 0; k < 1000; k++)
			{
				Projectile otherProj = Main.projectile[k];
				if (k != projectile.whoAmI && otherProj.active && otherProj.owner == projectile.owner && otherProj.type == projectile.type && System.Math.Abs(projectile.position.X - otherProj.position.X) + System.Math.Abs(projectile.position.Y - otherProj.position.Y) < spacing)
				{
					if (projectile.position.X < Main.projectile[k].position.X)
					{
						projectile.velocity.X -= idleAccel;
					}
					else
					{
						projectile.velocity.X += idleAccel;
					}
					if (projectile.position.Y < Main.projectile[k].position.Y)
					{
						projectile.velocity.Y -= idleAccel;
					}
					else
					{
						projectile.velocity.Y += idleAccel;
					}
				}
			}
			Vector2 targetPos = projectile.Center + new Vector2(projectile.direction, 0) + projectile.velocity;
			float targetDist = viewDist;
			bool target = false;
			projectile.tileCollide = !noCollide;
			if(player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if(noCollide || Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(projectile.Center, targetPos);
					targetPos = npc.Center;
                    if (predict)
                    {
                        Vector2 predictedPos = npc.Center + npc.velocity + (npc.velocity * (Vector2.Distance(npc.Center, projectile.Center) / shootSpeed));
                        predictedPos = npc.Center + npc.velocity + (npc.velocity * (Vector2.Distance(predictedPos, projectile.Center) / shootSpeed));
                        targetDist = Vector2.Distance(projectile.Center, predictedPos);
                        targetPos = predictedPos;
                    }
                    target = true;
				}
			}
			else for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy(this, false))
				{
					float distance = Vector2.Distance(npc.Center, projectile.Center);
                    if ((distance < targetDist || !target) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        if (predict)
                        {
                            Vector2 predictedPos = npc.Center + npc.velocity + (npc.velocity * (Vector2.Distance(npc.Center, projectile.Center) / shootSpeed));
                            predictedPos = npc.Center + npc.velocity + (npc.velocity * (Vector2.Distance(predictedPos, projectile.Center) / shootSpeed));
                            targetDist = Vector2.Distance(projectile.Center, predictedPos);
                            targetPos = predictedPos;
                        }
                        target = true;
                    }
				}
			}
            if (grounded)
            {
                if (targetPos.Y > projectile.position.Y + projectile.height)
                {
                    fallThroughPlat = true;
                }
                else
                {
                    fallThroughPlat = false;
                }
            }
			if (Vector2.Distance(player.Center, projectile.Center) > (player.HasMinionAttackTargetNPC ? 1500 : 750f)) 
			{
				projectile.ai[0] = 1f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
            }
            if (target && (projectile.ai[0] == 0f || player.HasMinionAttackTargetNPC))
			{
				Vector2 direction = targetPos - projectile.Center;
				if (direction.Length() > chaseDist)
				{
					direction.Normalize();
					projectile.velocity.X = (projectile.velocity.X * inertia + direction.X * chaseAccel) / (inertia + 1);
                    if (!grounded)
                    {
                        projectile.velocity.Y = (projectile.velocity.Y * inertia + direction.Y * chaseAccel) / (inertia + 1);
                    }
                    else if (jump && projectile.velocity.Y == 0)
                    {
                        projectile.velocity.Y = -(float)Math.Sqrt(2 * 0.25f * Math.Abs(projectile.Center.Y - targetPos.Y));
                    }
                }
				else
				{
					projectile.velocity.X *= (float)Math.Pow(0.97, 40.0 / inertia);
                    if (!grounded)
                    {
                        projectile.velocity.Y *= (float)Math.Pow(0.97, 40.0 / inertia);
                    }
				}
			}
			else
			{
				if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
				{
					projectile.ai[0] = 1f;
				}
				float speed = 6f;
				if (projectile.ai[0] == 1f)
				{
					speed = projectile.Distance(player.Center) / 60;
				}
				Vector2 center = projectile.Center;
				Vector2 direction = player.Center - center;
				projectile.netUpdate = true;
				int num = 1;
				for (int k = 0; k < projectile.whoAmI; k++)
				{
					if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
					{
						num++;
					}
				}
                direction.X -= (float)((10 + num * spacing) * player.direction);
                if (!grounded)
                {
                    direction.Y -= 70f;
                }
				float distanceTo = direction.Length();
				if (distanceTo > 200f && speed < 9f)
				{
					speed = 9f;
				}
				if (distanceTo < 100f && projectile.ai[0] == 1f && (noCollide || !Collision.SolidCollision(projectile.position, projectile.width, projectile.height)))
				{
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
				}
				if (distanceTo > 3000f)
				{
					projectile.Center = player.Center;
				}
				if (distanceTo > 48f)
				{
					direction.Normalize();
					direction *= speed;
					float temp = inertia / 2f;
                    projectile.velocity.X = (projectile.velocity.X * temp + direction.X) / (temp + 1);
                    if (!grounded)
                    {
                        projectile.velocity.Y = (projectile.velocity.Y * temp + direction.Y) / (temp + 1);
                    }
                }
				else
				{
					projectile.direction = Main.player[projectile.owner].direction;
                    projectile.velocity.X *= (float)Math.Pow(0.9, 40.0 / inertia);
                    if (!grounded)
                    {
                        projectile.velocity.Y *= (float)Math.Pow(0.9, 40.0 / inertia);
                    }
                }
            }
            if (grounded)
            {
                if (projectile.velocity.X == 0f && projectile.velocity.Y >= 0f)
                {
                    projectile.velocity.Y -= 3f;
                }
                if (projectile.ai[0] == 1f)
                {
                    int num = 1;
                    for (int k = 0; k < projectile.whoAmI; k++)
                    {
                        if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
                        {
                            num++;
                        }
                    }
                    if (target || player.HasMinionAttackTargetNPC)
                    {
                        projectile.ai[0] = 0;
                    }
                    Vector2 playerPos = player.Center + new Vector2(((10 + num * spacing) * -player.direction), 0);
                    projectile.tileCollide = false;
                    projectile.velocity = projectile.DirectionTo(playerPos) * Math.Max(projectile.Distance(playerPos) / 30, 1);
                    projectile.rotation = projectile.velocity.X * 0.05f;
                    FlyingDust();
                }
                else
                {
                    projectile.rotation = 0f;
                    projectile.velocity.Y += 0.25f;
                }
            }
            else
            {
                projectile.rotation = projectile.velocity.X * 0.05f;
            }

            if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = (projectile.direction = -1);
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = (projectile.direction = 1);
            }
            if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += 1f;
				if (Main.rand.Next(3) == 0)
				{
					projectile.ai[1] += 1f;
				}
            }
            if (target)
            {
                if (projectile.ai[1] > shootCool && (maxShootDist < 0 || targetDist < maxShootDist))
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
                if ((targetPos - projectile.Center).X > 0f)
                {
                    projectile.spriteDirection = (projectile.direction = -1);
                }
                else if ((targetPos - projectile.Center).X < 0f)
                {
                    projectile.spriteDirection = (projectile.direction = 1);
                }
                if (rapidAmount <= 1)
                {
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] = 1f;
                        if (Main.myPlayer == projectile.owner)
                        {
                            ShootEffects();
                            Vector2 shootVel = targetPos - projectile.Center;
                            if (shootVel == Vector2.Zero)
                            {
                                shootVel = new Vector2(0f, 1f);
                            }
                            shootVel.Normalize();
                            shootVel *= shootSpeed;
                            if (shootNum > 1)
                            {
                                float spread = shootSpread * 0.0174f;
                                float baseSpeed = (float)Math.Sqrt(shootVel.X * shootVel.X + shootVel.Y * shootVel.Y);
                                double startAngle = Math.Atan2(shootVel.X, shootVel.Y) - spread / shootNum;
                                double deltaAngle = spread / shootNum;
                                double offsetAngle;
                                int i;
                                for (i = 0; i < shootNum; i++)
                                {
                                    offsetAngle = startAngle + deltaAngle * i;
                                    int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), shoot, (int)(projectile.damage * damageMult), projectile.knockBack, Main.myPlayer, shootAI0, shootAI1);
                                    Main.projectile[proj].netUpdate = true;
                                }
                            }
                            else
                            {
                                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, shoot, (int)(projectile.damage * damageMult), projectile.knockBack, Main.myPlayer, shootAI0, shootAI1);
                                Main.projectile[proj].netUpdate = true;
                            }
                            projectile.netUpdate = true;
                        }
                    }
                }
                else
                {
                    if (projectile.ai[1] <= 0)
                    {
                        if (projectile.ai[1] % rapidRate == 0 && Main.myPlayer == projectile.owner)
                        {
                            ShootEffects();
                            Vector2 shootVel = targetPos - projectile.Center;
                            if (shootVel == Vector2.Zero)
                            {
                                shootVel = new Vector2(0f, 1f);
                            }
                            shootVel.Normalize();
                            shootVel *= shootSpeed;
                            if (shootNum > 1)
                            {
                                float spread = shootSpread * 0.0174f;
                                float baseSpeed = (float)Math.Sqrt(shootVel.X * shootVel.X + shootVel.Y * shootVel.Y);
                                double startAngle = Math.Atan2(shootVel.X, shootVel.Y) - spread / shootNum;
                                double deltaAngle = spread / shootNum;
                                double offsetAngle;
                                int i;
                                for (i = 0; i < shootNum; i++)
                                {
                                    offsetAngle = startAngle + deltaAngle * i;
                                    int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), shoot, (int)(projectile.damage * damageMult), projectile.knockBack, Main.myPlayer, 0f, 0f);
                                    Main.projectile[proj].netUpdate = true;
                                }
                            }
                            else
                            {
                                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, shoot, (int)(projectile.damage * damageMult), projectile.knockBack, Main.myPlayer, 0f, 0f);
                                Main.projectile[proj].netUpdate = true;
                            }
                            projectile.netUpdate = true;
                        }
                        projectile.ai[1]--;
                        if (projectile.ai[1] <= -rapidAmount * rapidRate)
                        {
                            projectile.ai[1] = 1;
                        }
                    }
                }
            }
            SelectFrame(targetPos);
            CreateDust();
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = fallThroughPlat;
			return true;
		}
	}
}