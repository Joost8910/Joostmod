using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class StormWyvernMinion : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Wyvern");
			Main.projFrames[projectile.type] = 5;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targetting
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 18;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            float mult = 1f;
            if (player.ownedProjectileCounts[projectile.type] > 4)
            {
                mult = (player.ownedProjectileCounts[projectile.type] - 3) * 4 / player.ownedProjectileCounts[projectile.type];
            }
            damage = (int)(damage * mult);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[projectile.owner];
            float mult = 1f;
            if (player.ownedProjectileCounts[projectile.type] > 4)
            {
                mult = (player.ownedProjectileCounts[projectile.type] - 3) * 4 / player.ownedProjectileCounts[projectile.type];
            }
            damage = (int)(damage * mult);
        }
        int aimWindow = 30;
        public override bool PreAI()
        {
            //CheckActive()
            Player player = Main.player[projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.dead)
            {
                modPlayer.stormWyvernMinion = false;
            }
            if (modPlayer.stormWyvernMinion)
            {
                projectile.timeLeft = 2;
            }

            projectile.frame = (int)projectile.ai[0];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.ai[0] == 0)
            {
                bool foundWings = false;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == projectile.owner && p.minion)
                    {
                        if (i != projectile.whoAmI && p.type == projectile.type)
                        {
                            if (p.ai[0] == 0)
                            {
                                p.Kill();
                            }
                            if (p.ai[0] == 1)
                            {
                                foundWings = true;
                            }
                        }
                    }
                }
                if (!foundWings)
                {
                    Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 1, projectile.identity).minionSlots = 0;
                    projectile.netUpdate = true;
                    /*
                    int latestProj = projectile.whoAmI;
                    for (int i = 1; i < 4; i++)
                    {
                        latestProj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, i, latestProj);
                        Main.projectile[latestProj].minionSlots = 0;
                        projectile.netUpdate = true;
                    }
                    */
                }

                if (projectile.velocity.X > 0f)
                {
                    projectile.spriteDirection = (projectile.direction = -1);
                }
                else if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = (projectile.direction = 1);
                }
                if (projectile.localAI[0] < 0)
                {
                    projectile.localAI[0]++;
                    if (projectile.localAI[0] == 0)
                    {
                        Main.PlaySound(25, projectile.Center);
                        Dust.NewDustPerfect(projectile.Center, 178, new Vector2(-projectile.direction, -3), 0, Color.Yellow, 2);
                    }
                }
                int max = player.ownedProjectileCounts[projectile.type] * 24;
                bool channeling = projectile.localAI[0] >= 0 && (player.controlUseTile || projectile.localAI[0] >= max) && player.HeldItem.shoot == projectile.type && player.itemTime > 0 && player.itemTime < 4 && !player.noItems && !player.CCed;
                if (Main.myPlayer == projectile.owner && channeling)
                {
                    float chargeSpeed = 12.5f;
                    player.itemAnimation = 2;
                    player.itemTime = 2;
                    Vector2 dir = Main.MouseWorld - projectile.Center;
                    if (!player.controlUseTile)
                    {
                        if (player.HasMinionAttackTargetNPC)
                        {
                            dir = Main.npc[player.MinionAttackTargetNPC].Center - projectile.Center;
                        }
                        else
                        {
                            dir = (projectile.rotation - 1.57f).ToRotationVector2();
                        }
                    }
                    Vector2 playerDir = player.DirectionTo(Main.MouseWorld);
                    int pDir = 0;
                    if (playerDir.X < 0)
                    {
                        pDir = -1;
                    }
                    if (playerDir.X > 0)
                    {
                        pDir = 1;
                    }
                    player.ChangeDir(pDir);

                    if (projectile.localAI[0] > 0 || dir.Length() < 150)
                    {
                        projectile.localAI[0]++;
                    }

                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0, 0, 0, default(Color), 0.5f);
                    
                    if (projectile.localAI[0] == 12 || projectile.localAI[0] == 24)
                    {
                        Main.PlaySound(42, projectile.Center, 21);
                    }
                    if (projectile.localAI[0] > 0 && projectile.localAI[0] == max - 115)
                    {
                        Main.PlaySound(42, projectile.Center, 224);
                    }
                    if (projectile.localAI[0] < max - 12)
                    {
                        dir.X += pDir * (float)(100 * Math.Sin(MathHelper.ToRadians(projectile.localAI[0] * 4)));
                        dir.Y += (float)(100 * Math.Cos(MathHelper.ToRadians(projectile.localAI[0] * 4)));
                        float length = chargeSpeed / dir.Length();
                        dir *= length;
                        if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                        {
                            projectile.netUpdate = true;
                        }
                        projectile.velocity.X = (projectile.velocity.X * 20 + dir.X) / 21;
                        projectile.velocity.Y = (projectile.velocity.Y * 20 + dir.Y) / 21;
                    }
                    else if (projectile.localAI[0] < max)
                    {
                        float length = chargeSpeed / dir.Length();
                        dir *= length;
                        if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                        {
                            projectile.netUpdate = true;
                        }
                        projectile.velocity = dir;
                    }
                    if (projectile.localAI[0] == max)
                    {
                        Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 226, 1, -0.3f);
                    }
                    if (projectile.localAI[0] >= max && projectile.localAI[0] < max+ aimWindow)
                    {
                        dir.Normalize();
                        projectile.velocity = -dir;
                        projectile.rotation = (float)Math.Atan2(-projectile.velocity.Y, -projectile.velocity.X) + 1.57f;
                    }
                    else
                    {
                        projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                    }
                    if (projectile.localAI[0] == max+ aimWindow)
                    {
                        Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 122, 1f);
                        dir.Normalize();
                        projectile.velocity = dir;
                        Projectile.NewProjectile(projectile.Center, dir, mod.ProjectileType("StormWyvernMinionZap"), (int)(projectile.damage * player.ownedProjectileCounts[projectile.type]), projectile.knockBack, projectile.owner, projectile.whoAmI);
                    }
                    if (projectile.localAI[0] >= max+ aimWindow + 20)
                    {
                        projectile.localAI[0] = -180;
                    }
                    return false;
                }
                else
                {
                    if (projectile.localAI[0] > 0)
                    {
                        projectile.localAI[0] = 0;
                    }
                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                    return true;
                }
            }
            else
            {
                Projectile prevSeg = Main.projectile[(int)projectile.ai[1]];
                if (projectile.ai[0] == 1)
                {
                    bool foundLegs = false;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == projectile.owner && p.minion)
                        {
                            if (i != projectile.whoAmI && p.type == projectile.type && p.ai[0] == 2)
                            {
                                foundLegs = true;
                                break;
                            }
                        }
                    }
                    if (!foundLegs)
                    {
                        Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 2, projectile.whoAmI).minionSlots = 0;
                        projectile.netUpdate = true;
                    }
                }
                if (projectile.ai[0] == 2)
                {
                    bool foundTail = false;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == projectile.owner && p.minion)
                        {
                            if (i != projectile.whoAmI && p.type == projectile.type && p.ai[0] == 3)
                            {
                                foundTail = true;
                                break;
                            }
                        }
                    }
                    if (!foundTail)
                    {
                        if (prevSeg.ai[0] == 4)
                        {
                            prevSeg.ai[0] = 2;
                            projectile.ai[0] = 3;
                        }
                        if (prevSeg.ai[0] == 1)
                        {
                            Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 3, projectile.whoAmI).minionSlots = 0;
                            projectile.netUpdate = true;
                        }
                    }
                }
                if (!prevSeg.active || prevSeg.owner != projectile.owner || prevSeg.type != projectile.type)
                {
                    projectile.Kill();
                }
                if (prevSeg.localAI[0] > 24)
                {
                    projectile.localAI[0]++;
                    if (projectile.localAI[0] == 12 || projectile.localAI[0] == 24)
                    {
                        Main.PlaySound(42, projectile.Center, 21);
                    }
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0, 0, 0, default(Color), 0.5f);
                }
                else
                {
                    projectile.localAI[0] = 0;
                }
                float dirX = prevSeg.Center.X - projectile.Center.X;
                float dirY = prevSeg.Center.Y - projectile.Center.Y;
                projectile.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - 24) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                projectile.spriteDirection = projectile.direction = dirX > 0 ? -1 : 1;
                projectile.velocity = Vector2.Zero;
                projectile.position.X = projectile.position.X + posX;
                projectile.position.Y = projectile.position.Y + posY;
                return false;
            }
        }
        public override void AI()
        {
            float viewDist = 600f;
            float inertia = 20f;
            float chaseAccel = 10f;
            float chargeSpeed = 15f;
            int chargeCool = 120;


            Player player = Main.player[projectile.owner];
            Vector2 targetPos = projectile.position;
            float targetDist = viewDist;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, projectile.Center);
                    if ((distance < targetDist || !target))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                        if (projectile.localAI[1] < 0)
                        {
                            projectile.localAI[1] = 0;
                        }
                    }
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
                            target = true;
                        }
                    }
                }
            if (Vector2.Distance(player.Center, projectile.Center) > (target ? 1000f : 500f))
            {
                projectile.localAI[1] = -1;
                projectile.netUpdate = true;
            }
            if (target && projectile.localAI[1] >= 0f)
            {
                Vector2 direction = targetPos - projectile.Center;
                direction.Normalize();
                projectile.velocity = (projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
            }
            else
            {
                if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    projectile.localAI[1] = -1;
                }
                float speed = 6f;
                if (projectile.localAI[1] == -1)
                {
                    speed = 15;
                }
                Vector2 center = projectile.Center;
                Vector2 direction = player.Center - center;
                projectile.netUpdate = true;
                direction.X += player.direction * (float)(200 * Math.Cos(MathHelper.ToRadians((float)(Main.time % 180) * 2)));
                direction.Y += (float)(90 * Math.Sin(MathHelper.ToRadians((float)(Main.time % 180) * 2)));
                float distanceTo = direction.Length();
                if (distanceTo > 200f && speed < 9f)
                {
                    speed = 9f;
                }
                if (distanceTo < 100f && projectile.localAI[1] < 0)
                {
                    projectile.localAI[1] = 0f;
                    projectile.netUpdate = true;
                }
                if (distanceTo > 2000f)
                {
                    projectile.Center = player.Center;
                }
                if (distanceTo > 48f)
                {
                    direction.Normalize();
                    direction *= speed;
                    float temp = inertia / 2f;
                    projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
                }
                else
                {
                    projectile.direction = Main.player[projectile.owner].direction;
                    projectile.velocity *= (float)Math.Pow(0.98, 40.0 / inertia);
                }
            }

            if (projectile.localAI[1] > 0)
            {
                projectile.localAI[1]--;
                projectile.netUpdate = true;
            }
            if ((int)projectile.localAI[1] == 0f)
            {
                if (target)
                {
                    if ((targetPos - projectile.Center).X > 0f)
                    {
                        projectile.spriteDirection = (projectile.direction = -1);
                    }
                    else if ((targetPos - projectile.Center).X < 0f)
                    {
                        projectile.spriteDirection = (projectile.direction = 1);
                    }
                    projectile.velocity = projectile.DirectionTo(targetPos) * chargeSpeed;
                    projectile.netUpdate = true;
                    projectile.localAI[1] = chargeCool;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            int xFrameCount = 2;
            Texture2D tex = Main.projectileTexture[projectile.type];
            int frameX = 0;
            if (projectile.localAI[0] > 0)
            {
                frameX = 1;
            }
            Rectangle rect = new Rectangle(frameX * (tex.Width / xFrameCount), projectile.frame * (tex.Height / Main.projFrames[projectile.type]), (tex.Width / xFrameCount), (tex.Height / Main.projFrames[projectile.type]));
            Vector2 vect = new Vector2(((tex.Width / xFrameCount) / 2f), ((tex.Height / Main.projFrames[projectile.type]) / 2f));

            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rect, lightColor, projectile.rotation, vect, projectile.scale, effects, 0f);

            int max = Main.player[projectile.owner].ownedProjectileCounts[projectile.type] * 24;
            if (projectile.ai[0] == 0 && projectile.localAI[0] > 0 && projectile.localAI[0] < max + aimWindow)
            {
                float scale = projectile.localAI[0] < max ? projectile.localAI[0] / max : 1f;
                float rot = MathHelper.ToRadians(projectile.localAI[0] * 11);
                Texture2D texture = Main.projectileTexture[mod.ProjectileType("StormWyvernMinionZap")];
                rect = new Rectangle((texture.Width / 3) * ((int)projectile.localAI[0] / 2) % 3, 0, (texture.Width / 3), (texture.Height / 3));
                vect = new Vector2(((texture.Width / 3) / 2f), ((texture.Height / 3) / 2f));
                Vector2 offSet = (projectile.rotation - 1.57f).ToRotationVector2() * 30;
                spriteBatch.Draw(texture, offSet + new Vector2(projectile.position.X - Main.screenPosition.X + (projectile.width / 2f) - (texture.Width / 3) / 2f + vect.X, projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) - (float)(texture.Height / 3) / 2 + 4f + vect.Y), new Rectangle?(rect), Color.White, rot, vect, scale, effects, 0f);

            }
            return false;
        }
    }
}