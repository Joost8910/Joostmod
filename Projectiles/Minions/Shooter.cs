using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

        public virtual void ShootEffects(ref Vector2 shootvel)
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
			Vector2 targetPos = Projectile.Center + new Vector2(Projectile.direction, 0) + Projectile.velocity;
			float targetDist = viewDist;
			bool target = false;
			Projectile.tileCollide = !noCollide;
			if(player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if(noCollide || Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(Projectile.Center, targetPos);
					targetPos = npc.Center;
                    if (predict)
                    {
                        ModContent.GetInstance<JoostFunctions>().PredictNPCPosition(Projectile.Center, shootSpeed, npc, ref targetPos, ref targetDist);
                    }
                    target = true;
				}
			}
			else for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy(this, false))
				{
                    //Main.NewText(Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height), Color.Red);
                    //Main.NewText(Collision.CanHitLine(Projectile.Center, 0, 0, npc.Center, 0, 0), Color.Green);
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        if (predict)
                        {
                            ModContent.GetInstance<JoostFunctions>().PredictNPCPosition(Projectile.Center, shootSpeed, npc, ref targetPos, ref targetDist);
                        }
                        target = true;
                    }
				}
			}
            if (grounded)
            {
                if (targetPos.Y > Projectile.position.Y + Projectile.height)
                {
                    fallThroughPlat = true;
                }
                else
                {
                    fallThroughPlat = false;
                }
            }
			if (Vector2.Distance(player.Center, Projectile.Center) > (player.HasMinionAttackTargetNPC ? 1500 : 750f)) 
			{
				Projectile.ai[0] = 1f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 1f)
			{
				Projectile.tileCollide = false;
            }
            if (target && (Projectile.ai[0] == 0f || player.HasMinionAttackTargetNPC))
			{
				Vector2 direction = targetPos - Projectile.Center;
				if (direction.Length() > chaseDist)
				{
					direction.Normalize();
					Projectile.velocity.X = (Projectile.velocity.X * inertia + direction.X * chaseAccel) / (inertia + 1);
                    if (!grounded)
                    {
                        Projectile.velocity.Y = (Projectile.velocity.Y * inertia + direction.Y * chaseAccel) / (inertia + 1);
                    }
                    else if (jump && Projectile.velocity.Y == 0)
                    {
                        Projectile.velocity.Y = -(float)Math.Sqrt(2 * 0.25f * Math.Abs(Projectile.Center.Y - targetPos.Y));
                    }
                }
				else
				{
					Projectile.velocity.X *= (float)Math.Pow(0.97, 40.0 / inertia);
                    if (!grounded)
                    {
                        Projectile.velocity.Y *= (float)Math.Pow(0.97, 40.0 / inertia);
                    }
				}
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
					speed = Projectile.Distance(player.Center) / 60;
				}
				Vector2 center = Projectile.Center;
				Vector2 direction = player.Center - center;
				Projectile.netUpdate = true;
				int num = 1;
				for (int k = 0; k < Projectile.whoAmI; k++)
				{
					if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
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
				if (distanceTo < 100f && Projectile.ai[0] == 1f && (noCollide || !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height)))
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (distanceTo > 3000f)
				{
					Projectile.Center = player.Center;
				}
				if (distanceTo > 48f)
				{
					direction.Normalize();
					direction *= speed;
					float temp = inertia / 2f;
                    Projectile.velocity.X = (Projectile.velocity.X * temp + direction.X) / (temp + 1);
                    if (!grounded)
                    {
                        Projectile.velocity.Y = (Projectile.velocity.Y * temp + direction.Y) / (temp + 1);
                    }
                }
				else
				{
					Projectile.direction = Main.player[Projectile.owner].direction;
                    Projectile.velocity.X *= (float)Math.Pow(0.9, 40.0 / inertia);
                    if (!grounded)
                    {
                        Projectile.velocity.Y *= (float)Math.Pow(0.9, 40.0 / inertia);
                    }
                }
            }
            if (grounded)
            {
                if (Projectile.velocity.X == 0f && Projectile.velocity.Y >= 0f)
                {
                    Projectile.velocity.Y -= 3f;
                }
                if (Projectile.ai[0] == 1f)
                {
                    int num = 1;
                    for (int k = 0; k < Projectile.whoAmI; k++)
                    {
                        if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
                        {
                            num++;
                        }
                    }
                    if (target || player.HasMinionAttackTargetNPC)
                    {
                        Projectile.ai[0] = 0;
                    }
                    Vector2 playerPos = player.Center + new Vector2(((10 + num * spacing) * -player.direction), 0);
                    Projectile.tileCollide = false;
                    Projectile.velocity = Projectile.DirectionTo(playerPos) * Math.Max(Projectile.Distance(playerPos) / 30, 1);
                    Projectile.rotation = Projectile.velocity.X * 0.05f;
                    FlyingDust();
                }
                else
                {
                    Projectile.rotation = 0f;
                    Projectile.velocity.Y += 0.25f;
                }
            }
            else
            {
                Projectile.rotation = Projectile.velocity.X * 0.05f;
            }

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
            if (target)
            {
                if (Projectile.ai[1] > shootCool && (maxShootDist < 0 || targetDist < maxShootDist))
                {
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
                if ((targetPos - Projectile.Center).X > 0f)
                {
                    Projectile.spriteDirection = (Projectile.direction = -1);
                }
                else if ((targetPos - Projectile.Center).X < 0f)
                {
                    Projectile.spriteDirection = (Projectile.direction = 1);
                }
                if (rapidAmount <= 1)
                {
                    if (Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] = 1f;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Vector2 shootVel = targetPos - Projectile.Center;
                            if (shootVel == Vector2.Zero)
                            {
                                shootVel = new Vector2(0f, 1f);
                            }
                            shootVel.Normalize();
                            shootVel *= shootSpeed;
                            ShootEffects(ref shootVel);
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
                                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), shoot, (int)(Projectile.damage * damageMult), Projectile.knockBack, Main.myPlayer, shootAI0, shootAI1);
                                    Main.projectile[proj].netUpdate = true;
                                }
                            }
                            else
                            {
                                int proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, shoot, (int)(Projectile.damage * damageMult), Projectile.knockBack, Main.myPlayer, shootAI0, shootAI1);
                                Main.projectile[proj].netUpdate = true;
                            }
                            Projectile.netUpdate = true;
                        }
                    }
                }
                else
                {
                    if (Projectile.ai[1] <= 0)
                    {
                        if (Projectile.ai[1] % rapidRate == 0 && Main.myPlayer == Projectile.owner)
                        {
                            Vector2 shootVel = targetPos - Projectile.Center;
                            if (shootVel == Vector2.Zero)
                            {
                                shootVel = new Vector2(0f, 1f);
                            }
                            shootVel.Normalize();
                            shootVel *= shootSpeed;
                            ShootEffects(ref shootVel);
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
                                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), shoot, (int)(Projectile.damage * damageMult), Projectile.knockBack, Main.myPlayer, 0f, 0f);
                                    Main.projectile[proj].netUpdate = true;
                                }
                            }
                            else
                            {
                                int proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, shoot, (int)(Projectile.damage * damageMult), Projectile.knockBack, Main.myPlayer, 0f, 0f);
                                Main.projectile[proj].netUpdate = true;
                            }
                            Projectile.netUpdate = true;
                        }
                        Projectile.ai[1]--;
                        if (Projectile.ai[1] <= -rapidAmount * rapidRate)
                        {
                            Projectile.ai[1] = 1;
                        }
                    }
                }
            }
            SelectFrame(targetPos);
            CreateDust();
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = fallThroughPlat;
            return true;
        }
        /*
        public void PredictPosition(NPC npc, ref Vector2 targetPos, ref float targetDist)
        {
            Vector2 predictedVel = npc.velocity;
            Vector2 predictedPos = npc.position + predictedVel;
            float predictedTime = Vector2.Distance(npc.Center, Projectile.Center) / shootSpeed;
            for (int i = 0; i < predictedTime; i++)
            {
                PredictGravity(npc, predictedPos, ref predictedVel);
                predictedPos += predictedVel;
            }

            predictedTime = Vector2.Distance(predictedPos, Projectile.Center) / shootSpeed;
            predictedVel = npc.velocity;
            predictedPos = npc.position;
            for (int i = 0; i < predictedTime; i++)
            {
                PredictGravity(npc, predictedPos, ref predictedVel);
                predictedPos += predictedVel;
            }

            targetDist = Vector2.Distance(Projectile.Center, predictedPos);
            targetPos = predictedPos + new Vector2(npc.width / 2, npc.height / 2);
            //Dust.NewDustPerfect(targetPos, DustID.Adamantite, Vector2.Zero, 0, Color.Red, 3f).noGravity = true;
        }
        public void PredictGravity(NPC npc, Vector2 predictedPos, ref Vector2 predictedVelocity)
        {
            if (!npc.noGravity)
            {
                float gravity = 0.3f;
                float maxFallSpeed = 10f;
                if (npc.type == NPCID.MushiLadybug)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 3f;
                }
                else if (npc.type == NPCID.VortexRifleman && npc.ai[2] == 1f)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 2f;
                }
                else if ((npc.type == NPCID.DD2OgreT2 || npc.type == NPCID.DD2OgreT3) && npc.ai[0] > 0f && npc.ai[1] == 2f)
                {
                    gravity = 0.45f;
                    maxFallSpeed = 32f;
                }
                else if (npc.type == NPCID.VortexHornet && npc.ai[2] == 1f)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 4f;
                }
                else if (npc.type == NPCID.VortexHornetQueen)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 3f;
                }
                else if (npc.type == NPCID.SandElemental)
                {
                    gravity = 0f;
                }
                float num = (float)(Main.maxTilesX / 4200);
                num *= num;
                float num2 = (float)((double)(npc.position.Y / 16f - (60f + 10f * num)) / (Main.worldSurface / 6.0));
                if ((double)num2 < 0.25)
                {
                    num2 = 0.25f;
                }
                if (num2 > 1f)
                {
                    num2 = 1f;
                }
                gravity *= num2;
                if (npc.wet)
                {
                    gravity = 0.2f;
                    maxFallSpeed = 7f;
                    if (npc.honeyWet)
                    {
                        gravity = 0.1f;
                        maxFallSpeed = 4f;
                    }
                }
                predictedVelocity.Y += gravity;
                if (predictedVelocity.Y > maxFallSpeed)
                {
                    predictedVelocity.Y = maxFallSpeed;
                }
            }
            if (!npc.noTileCollide)
            {
                predictedVelocity = Collision.TileCollision(predictedPos, predictedVelocity, npc.width, npc.height);
            }
        }
            */
    }
    
}