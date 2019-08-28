using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class Cactite : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactite");
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 46;
            npc.damage = 0;
            npc.defense = 0;
            if (Main.expertMode)
            {
                if (Main.hardMode)
                {
                    npc.lifeMax = 220;
                }
                else
                {
                    npc.lifeMax = 110;
                }
            }
            else
            {
                npc.lifeMax = 55;
            }
            if (NPC.downedMoonlord)
            {
                if (Main.expertMode)
                {
                    npc.lifeMax = 880;
                }
                else
                {
                    npc.lifeMax = 440;
                }
                npc.defense = 15;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 0, 60);
            npc.knockBackResist = 0.3f;
            npc.aiStyle = 0;
            npc.frameCounter = 0;
            banner = mod.NPCType("Cactoid");
            bannerItem = mod.ItemType("CactoidBanner");
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Cactus, 10);
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Anniversary"), 1);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Cactus Person"))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.velocity.X != 0)
            {
                if (npc.frameCounter >= 15 / (1 + Math.Abs(npc.velocity.X)))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 54);
                }
                if (npc.frame.Y >= 216)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 54);
                }
                if (npc.frame.Y >= 432)
                {
                    npc.frame.Y = 216;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[2]++;
            for (int n = 0; n < 200; n++)
            {
                NPC N = Main.npc[n];
                if (N.active && N.Distance(npc.Center) < 400 && Collision.CanHitLine(npc.Center, 1, 1, N.Center, 1, 1) && (N.type == mod.NPCType("Cactite") || N.type == mod.NPCType("Cactoid")))
                {
                    N.target = npc.target;
                    N.ai[2]++;
                    N.netUpdate = true;
                }
            }
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactite1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactite2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactite2"), 1f);
            }
            if (npc.friendly)
            {
                npc.ai[3] = 20;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (target.GetModPlayer<JoostPlayer>(mod).cactoidCommendation)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if (player.GetModPlayer<JoostPlayer>(mod).cactoidCommendation)
            {
                return false;
            }
            return base.CanBeHitByItem(player, item);
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (Main.player[projectile.owner].GetModPlayer<JoostPlayer>(mod).cactoidCommendation && projectile.type != mod.ProjectileType("Manipulation") && projectile.friendly)
            {
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (!npc.friendly && (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active))
            {
                npc.TargetClosest(false);
            }
            bool playerCactoid = (player.GetModPlayer<JoostPlayer>(mod).cactoidCommendation || player.HasBuff(mod.BuffType("CactoidFriend")));
            bool cactusPersonNear = false;
            int cactusPerson = -1;
            if (!playerCactoid)
            {
                float num = 0f;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && !Main.player[i].ghost && (Main.player[i].GetModPlayer<JoostPlayer>(mod).cactoidCommendation || Main.player[i].HasBuff(mod.BuffType("CactoidFriend"))))
                    {
                        float num4 = Math.Abs(Main.player[i].Center.X - npc.Center.X + Math.Abs(Main.player[i].Center.Y - npc.Center.Y));
                        if (num4 < num)
                        {
                            num = num4;
                            npc.target = i;
                            playerCactoid = true;
                        }
                    }
                }
                for (int k = 0; k < 200; k++)
                {
                    NPC cactu = Main.npc[k];
                    if (cactu.active && cactu.type == mod.NPCType("Cactus Person") && npc.Distance(cactu.Center) < 800)
                    {
                        cactusPersonNear = true;
                        cactusPerson = cactu.whoAmI;
                        break;
                    }
                }
            }
            if (playerCactoid || cactusPersonNear)
            {
                npc.friendly = true;
            }
            else
            {
                npc.friendly = false;
            }
            if (npc.friendly)
            {
                float idleAccel = 0.05f;
                float viewDist = 600f;
                float chaseAccel = 10f;
                float inertia = 20f;
                npc.dontCountMe = true;
                if (npc.ai[3] > 0)
                {
                    npc.dontTakeDamageFromHostiles = true;
                    npc.ai[3]--;
                }
                else
                {
                    npc.dontTakeDamageFromHostiles = false;
                }
                npc.ai[2] = 0;
                if (Main.expertMode)
                {
                    if (Main.hardMode)
                    {
                        npc.damage = 45;
                    }
                    else
                    {
                        npc.damage = 30;
                    }
                }
                else
                {
                    npc.damage = 15;
                }
                if (NPC.downedMoonlord)
                {
                    if (Main.expertMode)
                    {
                        npc.damage = 120;
                    }
                    else
                    {
                        npc.damage = 60;
                    }
                }
                if (npc.localAI[1] % 15 == 0 && npc.life < npc.lifeMax)
                {
                    npc.life++;
                }
                if (npc.localAI[1] <= 0)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("Cactite"), npc.damage, 2, Main.myPlayer, npc.whoAmI);
                    }
                    npc.localAI[1] = 40;
                }
                npc.localAI[1]--;

                int num = 1;
                for (int k = 0; k < npc.whoAmI; k++)
                {
                    if (Main.npc[k].active && Main.npc[k].target == npc.target && (Main.npc[k].type == npc.type || Main.npc[k].type == mod.NPCType("Cactoid")))
                    {
                        num++;
                    }
                }
                if (npc.velocity.X > 0f)
                {
                    npc.direction = 1;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                else if (npc.velocity.Y == 0 && playerCactoid && Math.Abs(npc.Center.X - (player.Center.X - ((10 + num * 40) * player.direction))) > 30)
                {
                    npc.velocity.Y = -7;
                }
                if (playerCactoid)
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC otherCactoid = Main.npc[k];
                        if (k != npc.whoAmI && otherCactoid.friendly && otherCactoid.active && otherCactoid.target == npc.target && (otherCactoid.type == npc.type || otherCactoid.type == mod.NPCType("Cactoid")) && Math.Abs(npc.position.X - otherCactoid.position.X) + Math.Abs(npc.position.Y - otherCactoid.position.Y) < npc.width)
                        {
                            if (npc.position.X < otherCactoid.position.X)
                            {
                                npc.velocity.X -= idleAccel;
                            }
                            else
                            {
                                npc.velocity.X += idleAccel;
                            }
                        }
                    }
                }
                Vector2 targetPos = npc.position;
                float targetDist = viewDist;
                bool target = false;
                if (player.HasMinionAttackTargetNPC && playerCactoid)
                {
                    NPC N = Main.npc[player.MinionAttackTargetNPC];
                    if (Collision.CanHitLine(npc.position, npc.width, npc.height, N.position, N.width, N.height))
                    {
                        targetDist = Vector2.Distance(npc.Center, targetPos);
                        targetPos = N.Center;
                        target = true;
                    }
                }
                else
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC N = Main.npc[k];
                        if (N.active && !N.friendly && !N.dontTakeDamage && N.damage > 0)
                        {
                            float distance = Vector2.Distance(npc.Center, N.Center);
                            if ((distance < targetDist || !target) && Collision.CanHitLine(npc.position, npc.width, npc.height, N.position, N.width, N.height))
                            {
                                targetDist = distance;
                                targetPos = N.Center;
                                target = true;
                            }
                        }
                    }
                }
                if (Vector2.Distance(player.Center, npc.Center) > (target ? 1500f : 750f) && playerCactoid)
                {
                    npc.ai[0] = 1f;
                }
                if (Vector2.Distance(Main.npc[cactusPerson].Center, npc.Center) > 600f && cactusPersonNear && !playerCactoid)
                {
                    npc.ai[0] = 1f;
                }


                if (target && npc.ai[0] == 0f)
                {
                    Vector2 direction = targetPos - npc.Center;
                    direction.Normalize();
                    npc.direction = direction.X < 0 ? -1 : 1;
                    npc.velocity.X = (npc.velocity.X * inertia + direction.X * chaseAccel) / (inertia + 1);
                    if (targetPos.Y + 60 < npc.position.Y && npc.velocity.Y == 0)
                    {
                        npc.velocity.Y = -7;
                    }
                }
                else if (playerCactoid)
                {
                    Vector2 center = npc.Center;
                    Vector2 direction = player.Center - center;
                    if (!Collision.CanHitLine(npc.Center, 1, 1, player.Center, 1, 1) && direction.Length() >= 200f)
                    {
                        npc.ai[0] = 1f;
                    }
                    float speed = 6f;
                    if (npc.ai[0] == 1f)
                    {
                        speed = 15f;
                    }
                    direction.X -= ((10 + num * 40) * player.direction);
                    direction.Y -= 70f;
                    float distanceTo = direction.Length();
                    if (distanceTo > 200f && speed < 9f)
                    {
                        speed = 9f;
                    }
                    if (distanceTo < 200f && npc.ai[0] == 1f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[0] = 0f;
                    }
                    if (distanceTo > 48f)
                    {
                        direction.Normalize();
                        direction *= speed;
                        float temp = inertia / 2f;
                        npc.velocity.X = (npc.velocity.X * temp + direction.X) / (temp + 1);
                    }
                    else
                    {
                        npc.direction = Main.player[npc.target].direction;
                        npc.velocity.X *= (float)Math.Pow(0.9, 40.0 / inertia);
                    }
                }
                else if (Vector2.Distance(Main.npc[cactusPerson].Center, npc.Center) > 600f && cactusPersonNear)
                {
                    if (npc.Center.X > Main.npc[cactusPerson].Center.X)
                    {
                        npc.direction = -1;
                    }
                    else
                    {
                        npc.direction = 1;
                    }
                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {
                        npc.velocity.Y = -7;
                    }
                    npc.velocity.X = npc.direction * 4;
                }
                else
                {
                    if (cactusPersonNear && Vector2.Distance(Main.npc[cactusPerson].Center, npc.Center) < 400f)
                    {
                        npc.ai[0] = 0;
                    }
                    npc.ai[1] += 1 + Main.rand.Next(5);
                    if (npc.ai[1] > 900)
                    {
                        if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                npc.direction *= -1;
                            }
                            else
                            {
                                npc.velocity.Y = -7;
                            }
                        }
                        npc.velocity.X = npc.direction * 2;
                    }
                    if (npc.ai[1] > 2000 && npc.velocity.Y == 0)
                    {
                        npc.ai[1] = 0;
                        npc.velocity.X = 0f;
                    }
                }
            }
            else
            {
                if (npc.ai[2] < 1)
                {
                    npc.aiStyle = 0;
                    npc.damage = 0;
                    npc.ai[1] += 1 + Main.rand.Next(5);
                    if (npc.ai[1] > 900)
                    {
                        if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                npc.direction *= -1;
                            }
                            else
                            {
                                npc.velocity.Y = -7;
                            }
                        }
                        npc.velocity.X = npc.direction * 2;
                    }
                    if (npc.ai[1] > 2000 && npc.velocity.Y == 0)
                    {
                        npc.ai[1] = 0;
                        npc.velocity.X = 0f;
                    }
                }
                else
                {
                    npc.FaceTarget();
                    npc.aiStyle = 26;
                    aiType = NPCID.Unicorn;
                    if (Main.expertMode)
                    {
                        if (Main.hardMode)
                        {
                            npc.damage = 45;
                        }
                        else
                        {
                            npc.damage = 30;
                        }
                    }
                    else
                    {
                        npc.damage = 15;
                    }
                    if (NPC.downedMoonlord)
                    {
                        if (Main.expertMode)
                        {
                            npc.damage = 120;
                        }
                        else
                        {
                            npc.damage = 60;
                        }
                    }
                    npc.ai[1] = 1000;
                    npc.ai[2] = 1;
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
            }
            npc.netUpdate = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.player.ZoneBeach && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.spawnTileY < Main.rockLayer && spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneHoly ? (Main.hardMode ? 0.075f : 0.15f) : 0f;
        }

    }
}

