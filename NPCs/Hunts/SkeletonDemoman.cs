using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class SkeletonDemoman : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skeleton Demolitionist");
            Main.npcFrameCount[npc.type] = 13;
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 40;
            npc.damage = 24;
            npc.defense = 8;
            npc.lifeMax = 2200;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.frameCounter = 0;
            npc.noTileCollide = false;
            npc.noGravity = false;
            npc.netAlways = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneDungeon && spawnInfo.spawnTileY >= Main.rockLayer && !JoostWorld.downedSkeletonDemoman && JoostWorld.activeQuest.Contains(npc.type) && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override void NPCLoot()
        {
            JoostWorld.downedSkeletonDemoman = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("SkeletonDemoman"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0] += npc.ai[0] < 1 ? 1 : 0;
            if (npc.life > 0)
            {
                for (int i = 0; i < (int)((damage / npc.lifeMax) * 500); i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 26, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 26, 2.5f * hitDirection, -2.5f, 0, default(Color), 1f);
                }
                if (Main.netMode != 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-2, 2), -2, mod.ProjectileType("Grenade"), 20, 10, Main.myPlayer);
                    }
                }
                Gore.NewGore(new Vector2(npc.Center.X, npc.position.Y), npc.velocity, mod.GetGoreSlot("SkeletonDemoman"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, 43, npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, 43, npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, 44, npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, 44, npc.scale);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.velocity.Y == 0)
            {
                if (npc.ai[2] <= 0)
                {
                    npc.frameCounter += Math.Abs(npc.velocity.X);
                    if (npc.frameCounter > 20)
                    {
                        npc.frame.Y += 50;
                        npc.frameCounter = 0;
                    }
                    if (npc.frame.Y >= 200)
                    {
                        npc.frame.Y = 0;
                    }
                }
                if (npc.ai[2] == 1)
                {
                    if (npc.frame.Y < 200 || npc.frame.Y >= 350 || (npc.ai[1] % 30 >= 20 && npc.ai[1] % 30 < 30))
                    {
                        npc.frame.Y = 200;
                    }
                    if (npc.ai[1] % 30 >= 0 && npc.ai[1] % 30 < 10)
                    {
                        npc.frame.Y = 250;
                    }
                    if (npc.ai[1] % 30 >= 10 && npc.ai[1] % 30 < 20)
                    {
                        npc.frame.Y = 300;
                    }
                }
                if (npc.ai[2] == 3)
                {
                    npc.frame.Y = 550;
                }
                if (npc.ai[2] == 4)
                {
                    npc.frame.Y = 600;
                }
            }
            else
            {
                npc.frame.Y = 350;
            }
            if (npc.ai[2] == 2)
            {
                if (npc.frame.Y < 400 || npc.frame.Y >= 550 || (npc.ai[1] % 30 >= 10 && npc.ai[1] % 30 < 20))
                {
                    npc.frame.Y = 400;
                }
                if (npc.ai[1] % 30 >= 20 && npc.ai[1] % 30 < 30)
                {
                    npc.frame.Y = 450;
                }
                if (npc.ai[1] % 30 >= 0 && npc.ai[1] % 30 < 10)
                {
                    npc.frame.Y = 500;
                }
            }
        }
        public override void AI()
        {
            npc.damage = 0;
            Player P = Main.player[npc.target];
            npc.netUpdate = true;
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
                P = Main.player[npc.target];
                if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2500)
                {
                    npc.ai[0] = 0;
                }
            }
            if (npc.ai[2] == 3)
            {
                npc.velocity = Vector2.Zero;
                npc.ai[1]++;
                if (npc.ai[1] > 30)
                {
                    Main.PlaySound(0, npc.Center);
                    Main.PlaySound(7, npc.Center);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(8 * npc.direction + ((int)npc.Center.X / 16) * 16, npc.Center.Y, 0, 5, mod.ProjectileType("Landmine"), 40, 25, Main.myPlayer);
                    }
                    npc.ai[2] = 0;
                    npc.velocity.X = npc.direction;
                }
            }
            if (npc.ai[0] < 1)
            {
                if (npc.ai[2] != 3)
                {
                    npc.ai[2] = 0;
                    npc.ai[1] = 0;
                    if (npc.velocity.Y == 0)
                    {
                        if (npc.velocity.X == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                npc.direction *= -1;
                            }
                            else
                            {
                                npc.velocity.Y = -6;
                            }
                        }
                        if (Main.rand.Next(600) == 0)
                        {
                            npc.ai[2] = 3;
                        }
                        npc.rotation = 0;
                    }
                    else
                    {
                        npc.rotation += npc.direction * 24 * 0.0174f;
                    }

                    if (npc.velocity.X * npc.direction < 2)
                    {
                        npc.velocity.X += npc.direction * 0.11f;
                    }
                }
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)(npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(npc.Center, P.Center) < 400)
                {
                    npc.ai[0]++;
                }
            }
            else
            {
                if (npc.ai[2] <= 0)
                {
                    if (npc.velocity.Y == 0)
                    {
                        if (Main.rand.Next(150) == 0)
                        {
                            npc.ai[2] = 1;
                            npc.ai[1] = 20;
                        }
                        if (npc.Distance(P.Center) < 80)
                        {
                            npc.direction = npc.Center.X > P.Center.X ? 1 : -1;
                        }
                        else if (Main.rand.Next(600) == 0)
                        {
                            npc.ai[2] = 3;
                            npc.ai[1] = 0;
                        }
                        if (npc.velocity.X == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                npc.direction *= -1;
                            }
                            else
                            {
                                npc.velocity.Y = -8;
                            }
                        }
                        npc.rotation = 0;
                    }
                    else
                    {
                        npc.rotation += npc.direction * 24 * 0.0174f;
                        if (npc.velocity.Y > 3 && npc.velocity.X == 0)
                        {
                            npc.ai[2] = 2;
                            npc.ai[1] = 10;
                        }
                    }
                    if (npc.ai[2] <= 0)
                    {
                        if (npc.velocity.X * npc.direction < 5f)
                        {
                            npc.velocity.X += npc.direction * 0.15f;
                        }
                        else
                        {
                            npc.velocity.X = npc.direction * 5f;
                        }
                    }
                }
                if (npc.ai[2] == 1)
                {
                    npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
                    npc.velocity = Vector2.Zero;
                    npc.ai[1]++;
                    if (npc.ai[1] % 30 == 0)
                    {
                        if (Main.rand.Next(5) < 2 || npc.Distance(P.Center) < 80)
                        {
                            npc.ai[2] = 0;
                            npc.velocity.X = npc.direction * 2.5f;
                        }
                        if (Main.netMode != 1)
                        {
                            float Speed = 5.5f;
                            int damage = 20;
                            int type = mod.ProjectileType("Grenade");
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                            float rotation = (float)Math.Atan2(npc.Center.Y - (P.Center.Y), npc.Center.X - (P.Center.X));
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10, Main.myPlayer);
                        }
                    }
                }
                if (npc.ai[2] == 2)
                {
                    npc.rotation = 0;
                    npc.velocity.Y = 1;
                    npc.ai[1]++;
                    if (npc.ai[1] % 30 == 0)
                    {
                        if (Main.rand.Next(5) < 2 || npc.Distance(P.Center) < 80)
                        {
                            npc.direction *= -1;
                            npc.velocity.X = 5f * npc.direction;
                            npc.velocity.Y = -8;
                        }
                        if (Main.netMode != 1)
                        {
                            float Speed = 5.5f;
                            int damage = 20;
                            int type = mod.ProjectileType("Grenade");
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                            float rotation = (float)Math.Atan2(npc.Center.Y - (P.Center.Y), npc.Center.X - (P.Center.X));
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10, Main.myPlayer);
                        }
                    }
                    if (npc.velocity.X != 0)
                    {
                        npc.ai[2] = 0;
                    }
                    if (npc.velocity.X * npc.direction < 5f)
                    {
                        npc.velocity.X += npc.direction * 0.15f;
                    }
                }
                if (npc.ai[2] == 4)
                {
                    npc.velocity.X = 0;
                    if (npc.ai[3] == 0 && Main.netMode != 1)
                    {
                        int damage = 200;
                        int type = mod.ProjectileType("DoomCannonHostile");
                        float rotation = (float)Math.Atan2(npc.Center.Y - (P.Center.Y), npc.Center.X - (P.Center.X));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1), type, damage, 30, Main.myPlayer, npc.whoAmI);
                    }
                    npc.ai[3]++;
                    if (npc.ai[3] > 700)
                    {
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                    }
                }
                else if (npc.life < npc.lifeMax / 4)
                {
                    npc.ai[3] = 0;
                    npc.ai[2] = 4;
                    npc.ai[1] = 0;
                    npc.velocity.X = 0;
                }
            }
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            if (npc.direction == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}

