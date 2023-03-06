using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class SporeSpawn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Mother");
            Main.npcFrameCount[NPC.type] = 3;
		}
		public override void SetDefaults()
		{
			NPC.width = 50;
			NPC.height = 50;
			NPC.damage = 30;
			NPC.defense = 14;
			NPC.lifeMax = 1500;
			NPC.HitSound = SoundID.NPCHit7;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.netAlways = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneJungle && spawnInfo.SpawnTileY > (Main.worldSurface + Main.rockLayer) / 2 && !JoostWorld.downedSporeSpawn && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.ai[0] > 0 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void OnKill()
		{
            JoostWorld.downedSporeSpawn = true;
            NPC.DropItemInstanced(NPC.position, NPC.Size, Mod.Find<ModItem>("SporeSpawn").Type, 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }
        }
		public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0]++;
            if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SporeSpawn"), 1f);
				Gore.NewGore(NPC.position, -NPC.velocity, Mod.GetGoreSlot("Gores/SporeSpawn"), 1f);
			}
        }
        Vector2 posOff = new Vector2(0, -100);
        public override void AI()
		{
            Player P = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.rotation += 0.0174f * 7.2f * NPC.direction;
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                P = Main.player[NPC.target];
                if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2500)
                {
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[0] < 1)
            {
                NPC.velocity *= 0;
                NPC.ai[1] = 810;
                NPC.ai[2] = 0;
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)(NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(NPC.Center, P.Center) < 400)
                {
                    NPC.ai[0]++;
                }   
            }
            else
            {
                NPC.ai[1]++;
                NPC.direction = NPC.velocity.X > 0 ? -1 : 1;
                if (Main.netMode != 1)
                {
                    if (NPC.ai[1] % 50 == 0)
                    {
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("Spore").Type, 0, NPC.whoAmI, 2f, 4);
                    }
                    if (NPC.ai[1] % 50 == 25)
                    {
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("Spore").Type, 0, NPC.whoAmI, -2f, 4);
                    }
                }
                NPC.defense = 10;
                if (NPC.ai[1] > 500)
                {
                    if (NPC.ai[1] >= 818)
                    {
                        NPC.ai[1] = 0;
                    }
                    if (NPC.ai[1] < 510 || NPC.ai[1] > 810)
                    {
                        NPC.velocity = Vector2.Zero;
                    }
                    else
                    {
                        NPC.defense = 25;
                        if (NPC.ai[1] == 510)
                        {
                            NPC.velocity = NPC.DirectionTo(P.Center) * 10;
                        }
                        else
                        {
                            if (NPC.velocity.X != NPC.oldVelocity.X)
                            {
                                NPC.velocity.X = -NPC.oldVelocity.X;
                                SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);
                            }
                            if (NPC.velocity.Y != NPC.oldVelocity.Y)
                            {
                                NPC.velocity.Y = -NPC.oldVelocity.Y;
                                SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);
                            }
                        }
                    }
                }
                else
                {
                    Vector2 targetPos = P.Center + posOff;
                    NPC.velocity = NPC.DirectionTo(targetPos) * 5;
                    if (NPC.Distance(targetPos) < 25)
                    {
                        NPC.ai[2]++;
                        if (NPC.ai[2] >= 8)
                        {
                            NPC.ai[2] = 0;
                        }

                        if (NPC.ai[2] == 0 || NPC.ai[2] == 4)
                        {
                            posOff = new Vector2(0, -100);
                        }
                        if (NPC.ai[2] == 1)
                        {
                            posOff = new Vector2(100, -50);
                        }
                        if (NPC.ai[2] == 2)
                        {
                            posOff = new Vector2(150, -100);
                        }
                        if (NPC.ai[2] == 3)
                        {
                            posOff = new Vector2(100, -150);
                        }
                        if (NPC.ai[2] == 5)
                        {
                            posOff = new Vector2(-100, -50);
                        }
                        if (NPC.ai[2] == 6)
                        {
                            posOff = new Vector2(-150, -100);
                        }
                        if (NPC.ai[2] == 7)
                        {
                            posOff = new Vector2(100, -150);
                        }
                    }
                }
            }
		}
        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[1] <= 500)
            {
                NPC.frame.Y = 0;
            }
            else if ((NPC.ai[1] > 500 && NPC.ai[1] < 510) || NPC.ai[1] > 810)
            {
                NPC.frame.Y = 70;
            }
            else
            {
                NPC.frame.Y = 140;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}

