using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Legendaries;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class SkeletonDemoman : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skeleton Demolitionist");
            Main.npcFrameCount[NPC.type] = 13;
        }
        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 40;
            NPC.damage = 24;
            NPC.defense = 8;
            NPC.lifeMax = 2200;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 0;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.frameCounter = 0;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.netAlways = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneDungeon && spawnInfo.SpawnTileY >= Main.rockLayer && !JoostWorld.downedSkeletonDemoman && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override void OnKill()
        {
            JoostWorld.downedSkeletonDemoman = true;
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(NPC, ModContent.ItemType<Items.Quest.SkeletonDemoman>(), Main.rand, 1, 1, 1, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EvilStone>(), 100));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0] += NPC.ai[0] < 1 ? 1 : 0;
            if (NPC.life > 0)
            {
                for (int i = 0; i < (int)((damage / NPC.lifeMax) * 500); i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 26, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
            else
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 26, 2.5f * hitDirection, -2.5f, 0, default(Color), 1f);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-2, 2), -2, ModContent.ProjectileType<Grenade>(), 20, 10, Main.myPlayer);
                    }
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(sauce, new Vector2(NPC.Center.X, NPC.position.Y), NPC.velocity, Mod.Find<ModGore>("SkeletonDemoman").Type);
                    Gore.NewGore(sauce, new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, 43, NPC.scale);
                    Gore.NewGore(sauce, new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, 43, NPC.scale);
                    Gore.NewGore(sauce, new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, 44, NPC.scale);
                    Gore.NewGore(sauce, new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, 44, NPC.scale);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.velocity.Y == 0)
            {
                if (NPC.ai[2] <= 0)
                {
                    NPC.frameCounter += Math.Abs(NPC.velocity.X);
                    if (NPC.frameCounter > 20)
                    {
                        NPC.frame.Y += 50;
                        NPC.frameCounter = 0;
                    }
                    if (NPC.frame.Y >= 200)
                    {
                        NPC.frame.Y = 0;
                    }
                }
                if (NPC.ai[2] == 1)
                {
                    if (NPC.frame.Y < 200 || NPC.frame.Y >= 350 || (NPC.ai[1] % 30 >= 20 && NPC.ai[1] % 30 < 30))
                    {
                        NPC.frame.Y = 200;
                    }
                    if (NPC.ai[1] % 30 >= 0 && NPC.ai[1] % 30 < 10)
                    {
                        NPC.frame.Y = 250;
                    }
                    if (NPC.ai[1] % 30 >= 10 && NPC.ai[1] % 30 < 20)
                    {
                        NPC.frame.Y = 300;
                    }
                }
                if (NPC.ai[2] == 3)
                {
                    NPC.frame.Y = 550;
                }
                if (NPC.ai[2] == 4)
                {
                    NPC.frame.Y = 600;
                }
            }
            else
            {
                NPC.frame.Y = 350;
            }
            if (NPC.ai[2] == 2)
            {
                if (NPC.frame.Y < 400 || NPC.frame.Y >= 550 || (NPC.ai[1] % 30 >= 10 && NPC.ai[1] % 30 < 20))
                {
                    NPC.frame.Y = 400;
                }
                if (NPC.ai[1] % 30 >= 20 && NPC.ai[1] % 30 < 30)
                {
                    NPC.frame.Y = 450;
                }
                if (NPC.ai[1] % 30 >= 0 && NPC.ai[1] % 30 < 10)
                {
                    NPC.frame.Y = 500;
                }
            }
        }
        public override void AI()
        {
            NPC.damage = 0;
            var sauce = NPC.GetSource_FromAI();
            Player P = Main.player[NPC.target];
            NPC.netUpdate = true;
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                P = Main.player[NPC.target];
                if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2500)
                {
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[2] == 3)
            {
                NPC.velocity = Vector2.Zero;
                NPC.ai[1]++;
                if (NPC.ai[1] > 30)
                {
                    SoundEngine.PlaySound(SoundID.Dig, NPC.Center);
                    SoundEngine.PlaySound(SoundID.Grab, NPC.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, 8 * NPC.direction + ((int)NPC.Center.X / 16) * 16, NPC.Center.Y, 0, 5, ModContent.ProjectileType<Landmine>(), 40, 25, Main.myPlayer);
                    }
                    NPC.ai[2] = 0;
                    NPC.velocity.X = NPC.direction;
                }
            }
            if (NPC.ai[0] < 1)
            {
                if (NPC.ai[2] != 3)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[1] = 0;
                    if (NPC.velocity.Y == 0)
                    {
                        if (NPC.velocity.X == 0)
                        {
                            if (Main.rand.NextBool(4))
                            {
                                NPC.direction *= -1;
                            }
                            else
                            {
                                NPC.velocity.Y = -6;
                            }
                        }
                        if (Main.rand.Next(600) == 0)
                        {
                            NPC.ai[2] = 3;
                        }
                        NPC.rotation = 0;
                    }
                    else
                    {
                        NPC.rotation += NPC.direction * 24 * 0.0174f;
                    }

                    if (NPC.velocity.X * NPC.direction < 2)
                    {
                        NPC.velocity.X += NPC.direction * 0.11f;
                    }
                }
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)(NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(NPC.Center, P.Center) < 400)
                {
                    NPC.ai[0]++;
                }
            }
            else
            {
                if (NPC.ai[2] <= 0)
                {
                    if (NPC.velocity.Y == 0)
                    {
                        if (Main.rand.NextBool(150))
                        {
                            NPC.ai[2] = 1;
                            NPC.ai[1] = 20;
                        }
                        if (NPC.Distance(P.Center) < 80)
                        {
                            NPC.direction = NPC.Center.X > P.Center.X ? 1 : -1;
                        }
                        else if (Main.rand.Next(600) == 0)
                        {
                            NPC.ai[2] = 3;
                            NPC.ai[1] = 0;
                        }
                        if (NPC.velocity.X == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                NPC.direction *= -1;
                            }
                            else
                            {
                                NPC.velocity.Y = -8;
                            }
                        }
                        NPC.rotation = 0;
                    }
                    else
                    {
                        NPC.rotation += NPC.direction * 24 * 0.0174f;
                        if (NPC.velocity.Y > 3 && NPC.velocity.X == 0)
                        {
                            NPC.ai[2] = 2;
                            NPC.ai[1] = 10;
                        }
                    }
                    if (NPC.ai[2] <= 0)
                    {
                        if (NPC.velocity.X * NPC.direction < 5f)
                        {
                            NPC.velocity.X += NPC.direction * 0.15f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.direction * 5f;
                        }
                    }
                }
                if (NPC.ai[2] == 1)
                {
                    NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
                    NPC.velocity = Vector2.Zero;
                    NPC.ai[1]++;
                    if (NPC.ai[1] % 30 == 0)
                    {
                        if (Main.rand.Next(5) < 2 || NPC.Distance(P.Center) < 80)
                        {
                            NPC.ai[2] = 0;
                            NPC.velocity.X = NPC.direction * 2.5f;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float Speed = 5.5f;
                            int damage = 20;
                            int type = ModContent.ProjectileType<Grenade>();
                            SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                            float rotation = (float)Math.Atan2(NPC.Center.Y - (P.Center.Y), NPC.Center.X - (P.Center.X));
                            Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10, Main.myPlayer);
                        }
                    }
                }
                if (NPC.ai[2] == 2)
                {
                    NPC.rotation = 0;
                    NPC.velocity.Y = 1;
                    NPC.ai[1]++;
                    if (NPC.ai[1] % 30 == 0)
                    {
                        if (Main.rand.Next(5) < 2 || NPC.Distance(P.Center) < 80)
                        {
                            NPC.direction *= -1;
                            NPC.velocity.X = 5f * NPC.direction;
                            NPC.velocity.Y = -8;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float Speed = 5.5f;
                            int damage = 20;
                            int type = ModContent.ProjectileType<Grenade>();
                            SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                            float rotation = (float)Math.Atan2(NPC.Center.Y - (P.Center.Y), NPC.Center.X - (P.Center.X));
                            Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10, Main.myPlayer);
                        }
                    }
                    if (NPC.velocity.X != 0)
                    {
                        NPC.ai[2] = 0;
                    }
                    if (NPC.velocity.X * NPC.direction < 5f)
                    {
                        NPC.velocity.X += NPC.direction * 0.15f;
                    }
                }
                if (NPC.ai[2] == 4)
                {
                    NPC.velocity.X = 0;
                    if (NPC.ai[3] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int damage = 200;
                        int type = ModContent.ProjectileType<DoomCannonHostile>();
                        float rotation = (float)Math.Atan2(NPC.Center.Y - (P.Center.Y), NPC.Center.X - (P.Center.X));
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1), type, damage, 30, Main.myPlayer, NPC.whoAmI);
                    }
                    NPC.ai[3]++;
                    if (NPC.ai[3] > 700)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                    }
                }
                else if (NPC.life < NPC.lifeMax / 4)
                {
                    NPC.ai[3] = 0;
                    NPC.ai[2] = 4;
                    NPC.ai[1] = 0;
                    NPC.velocity.X = 0;
                }
            }
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            if (NPC.direction == 1)
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

