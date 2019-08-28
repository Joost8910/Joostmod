using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
	[AutoloadBossHead]
	public class ImpLord : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Imp Lord");
			Main.npcFrameCount[npc.type] = 24;
        }
		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 46;
			npc.damage = 30;
			npc.defense = 16;
			npc.lifeMax = 3000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0;
			npc.knockBackResist = 0;
			npc.aiStyle = -1;
			npc.frameCounter = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
            npc.netAlways = true;
            npc.buffImmune[BuffID.OnFire] = true;
        }
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return spawnInfo.spawnTileY >= Main.maxTilesY - 250 && !JoostWorld.downedImpLord && JoostWorld.activeQuest == npc.type && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override void NPCLoot()
		{
			JoostWorld.downedImpLord = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("ImpLord"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
		}
		public override void HitEffect(int hitDirection, double damage)
		{
            if (npc.ai[0] == 0)
            {
                npc.ai[0]++;
            }
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ImpLord1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ImpLord2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ImpLord2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ImpLord3"), 1f);
            }
		}
        int dir = 1;
        int dirx = 1;
		public override void AI()
		{
			Player P = Main.player[npc.target];
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(true);
				P = Main.player[npc.target];
				if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2000)
				{
					npc.ai[0] = 0;
				}
			}
			if (npc.ai[0] < 1)
            {
                if (Main.rand.Next(100) == 0)
                {
                    npc.direction *= -1;
                }
                if (npc.velocity.X * npc.direction < 5)
                {
                    npc.velocity.X += npc.direction * 0.2f;
                }
                npc.velocity.Y = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Vector2.Distance(npc.Center, P.Center) < 1000 && P.active && !P.dead)
                {
                    npc.ai[0]++;
                }
            }
			else
			{
                npc.direction = P.Center.X < npc.Center.X ? -1 : 1;
                if (P.position.Y > npc.position.Y + 250)
                {
                    dir = 1;
                }
                if (P.position.Y < npc.position.Y - 150)
                {
                    dir = -1;
                }
                if (P.Center.X < npc.Center.X - 250)
                {
                    dirx = -1;
                }
                if (P.Center.X > npc.Center.X + 250)
                {
                    dirx = 1;
                }

                if (npc.velocity.X * dirx < 6)
                {
                    npc.velocity.X += dirx * 0.3f;
                }
                if (npc.velocity.Y * dir < 4)
                {
                    npc.velocity.Y += dir * 0.3f;
                }
                npc.ai[1]++;
                if (npc.ai[1] == 20 && Main.rand.Next(5) < 3)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.BurningSphere);
                    Main.PlaySound(SoundID.Item1, npc.Center);
                    npc.ai[2] = 1;
                }
                if (npc.ai[1] == 40 && (Main.rand.Next(5) < 4 || Vector2.Distance(npc.Center, P.Center) > 600 || npc.Center.Y >= P.position.Y))
                {
                    Projectile.NewProjectile(npc.Center, npc.DirectionTo(P.Center + new Vector2(P.velocity.X * (Vector2.Distance(P.Center, npc.Center) / 12), 0)) * 12, mod.ProjectileType("ImpFireBolt"), 20, 5, Main.myPlayer);
                    Main.PlaySound(SoundID.Item45, npc.Center);
                    npc.ai[2] = 1;
                }
                if (npc.ai[1] >= 60)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("FireBall"));
                    Main.PlaySound(SoundID.Item73, npc.Center);
                    npc.ai[2] = 1;
                }
                if (npc.ai[2] > 0)
                {
                    npc.ai[1] = 0;
                    npc.ai[2]++;
                    if (npc.ai[2] > 16)
                    {
                        npc.ai[2] = 0;
                    }
                }
                int i = 0;
                NPC f = Main.npc[i];
                bool fireball = false;
                if (NPC.AnyNPCs(mod.NPCType("FireBall")))
                {
                    for (i = 0; i < 200; i++)
                    {
                        f = Main.npc[i];
                        if (f.type == mod.NPCType("FireBall"))
                        {
                            if (f.friendly)
                            {
                                fireball = true;
                            }
                            break;
                        }
                    }
                    npc.velocity = Vector2.Zero;
                    npc.ai[1] = 0;
                }
                if (npc.ai[3] < 1 && (Vector2.Distance(P.Center, npc.Center) < 70 || (fireball && Vector2.Distance(f.Center + f.velocity*9, npc.Center) < 80)))
                {
                    npc.ai[3]++;
                    Projectile.NewProjectile(npc.Center, npc.velocity, mod.ProjectileType("ImpTail"), 15, 8, 0, npc.whoAmI);
                    Main.PlaySound(42, npc.Center, 230);
                }
                if (npc.ai[3] > 0)
                {
                    npc.ai[3]++;
                    if (npc.ai[3] > 16)
                    {
                        if (!NPC.AnyNPCs(mod.NPCType("FireBall")))
                        {
                            Vector2 targetPos = new Vector2((P.Center.X - 250) + Main.rand.Next(500), (P.position.Y - 150) + Main.rand.Next(300));
                            npc.Teleport(targetPos, 1);
                        }
                        npc.ai[3] = 0;
                    }
                }
            }
            npc.netUpdate = true;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 54;
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.ai[3] > 0)
            {
                if (npc.frameCounter >= 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                }
                if (npc.frame.Y < 20 * frameHeight || npc.frame.Y > 23 * frameHeight)
                {
                    npc.frame.Y = 20 * frameHeight;
                }
            }
            else
            {
                if (npc.ai[2] > 0)
                {
                    if (npc.frameCounter >= 4)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += frameHeight;
                    }
                    if (npc.frame.Y > 19 * frameHeight || npc.frame.Y < 16 * frameHeight)
                    {
                        npc.frame.Y = 16 * frameHeight;
                    }
                }
                else
                {
                    if (npc.ai[1] < 1)
                    {
                        if (npc.frameCounter >= 6)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y += frameHeight;
                        }
                        if (npc.frame.Y > 3 * frameHeight)
                        {
                            npc.frame.Y = 0;
                        }
                    }
                    else
                    {
                        if (npc.frameCounter >= 5)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y += frameHeight;
                        }
                        if (npc.frame.Y > 15 * frameHeight || npc.frame.Y < 4 * frameHeight)
                        {
                            npc.frame.Y = 4 * frameHeight;
                        }
                    }
                }
            }
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            if (npc.direction == -1)
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

