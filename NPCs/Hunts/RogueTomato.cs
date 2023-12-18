using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Legendaries;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class RogueTomato : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rogue Tomato");
            Main.npcFrameCount[NPC.type] = 10;
        }
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 48;
            NPC.damage = 20;
            NPC.defense = 8;
            NPC.lifeMax = 650;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0;
            NPC.knockBackResist = 0.01f;
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
            return Main.dayTime && !spawnInfo.Sky && spawnInfo.SpawnTileY <= Main.worldSurface && !JoostWorld.downedRogueTomato && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frame.Y = (int)(NPC.ai[0] * 2);
        }
        public override void OnKill()
        {
            JoostWorld.downedRogueTomato = true;
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(NPC, ModContent.ItemType<Items.Quest.RogueTomato>(), Main.rand, 1, 1, 1, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EvilStone>(), 100));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0] += NPC.ai[0] < 1 ? 1 : 0;
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("RogueTomato").Type);
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.ai[0] > 20 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC.netUpdate = true;
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || P.dead || !P.active)
            {
                NPC.TargetClosest(true);
                P = Main.player[NPC.target];
                if (NPC.ai[0] >= 20 && (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2500))
                {
                    NPC.ai[0] = 19;
                    NPC.ai[3] = 1;
                }
            }
            if (NPC.ai[0] < 1)
            {
                NPC.noTileCollide = false;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.velocity.X = 0;
                NPC.rotation = 0;
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(NPC.Center, P.Center) < 400 && !P.dead && P.active)
                {
                    NPC.ai[0]++;
                }
            }
            else if (NPC.ai[0] < 20)
            {
                if (NPC.ai[3] == 1)
                {
                    NPC.ai[0]--;
                }
                else
                {
                    NPC.ai[0]++;
                }
            }
            else
            {
                if (NPC.velocity.Y == 0)
                {
                    NPC.direction = P.Center.X < NPC.Center.X ? -1 : 1;
                }
                if (NPC.velocity.X == 0 && NPC.velocity.Y >= 0 && P.position.Y + P.height < NPC.position.Y + NPC.height)
                {
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.velocity.X = -NPC.direction;
                    }
                    NPC.velocity.Y = -6;
                    NPC.noTileCollide = true;
                }
                if (NPC.velocity.X * NPC.direction < 4)
                {
                    NPC.velocity.X += NPC.direction * 0.081f;
                }
                if (NPC.velocity.X > 4 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = 4;
                }
                if (NPC.velocity.X < -4 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = -4;
                }
                if (NPC.velocity.Y > 0)
                {
                    NPC.noTileCollide = false;
                }
                if (P.position.Y > NPC.position.Y + NPC.height && Math.Abs(NPC.Center.X - P.Center.X) < 100)
                {
                    NPC.noTileCollide = true;
                }

                if (NPC.velocity.Y != 0)
                {
                    NPC.ai[0] = 260;
                    NPC.rotation += NPC.direction * 24 * 0.0174f;
                }
                else
                {
                    NPC.rotation = 0;
                    NPC.ai[1]++;
                    if (NPC.ai[1] > 6)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[0] += 30;
                    }
                    if (NPC.ai[0] > 200)
                    {
                        NPC.ai[0] = 50;
                    }
                    if (NPC.ai[0] == 140)
                    {
                        if (NPC.soundDelay <= 0)
                        {
                            NPC.soundDelay = 30;
                            SoundEngine.PlaySound(SoundID.Dig.WithVolumeScale(0.275f * NPC.scale).WithPitchOffset(0.3f - (0.5f * (NPC.scale - 1))), NPC.Center);
                        }
                        NPC.position.X += NPC.direction * 8;
                        NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
                    }
                    else if (NPC.velocity.X * NPC.direction <= 0)
                    {
                        NPC.ai[0] = 230;
                        Vector2 posi = new Vector2(NPC.Center.X - NPC.direction * 12, NPC.position.Y + NPC.height + 4);
                        Point pos = posi.ToTileCoordinates();
                        Tile tile = Framing.GetTileSafely(pos.X, pos.Y);
                        if (tile.HasTile)
                        {
                            Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tile)];
                            dust.velocity.Y = -2.5f;
                            dust.velocity.X = NPC.velocity.X * 2.5f;
                        }
                    }
                    else if (NPC.position.Y > P.position.Y + P.height)
                    {
                        NPC.ai[2]++;
                        if (NPC.ai[2] > 15)
                        {
                            NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                            NPC.noTileCollide = true;
                            NPC.ai[2] = 0;
                        }
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}

