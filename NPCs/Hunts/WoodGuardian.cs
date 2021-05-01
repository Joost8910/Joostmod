using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
	[AutoloadBossHead]
	public class WoodGuardian : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forest's Vengeance");
			Main.npcFrameCount[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 6;
        }
		public override void SetDefaults()
		{
			npc.width = 30;
			npc.height = 96;
			npc.damage = 16;
			npc.defense = 10;
			npc.lifeMax = 800;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0;
			npc.knockBackResist = 0.1f;
            if (Main.expertMode)
            {
                npc.knockBackResist = 0;
            }
			npc.aiStyle = -1;
			npc.frameCounter = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
            npc.netAlways = true;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return (tile.type == 2 || tile.type == 3 || tile.type == 5) && spawnInfo.spawnTileY <= Main.worldSurface && !spawnInfo.sky && !JoostWorld.downedWoodGuardian && JoostWorld.activeQuest.Contains(npc.type) && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return npc.ai[0] > 0 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void NPCLoot()
		{
			JoostWorld.downedWoodGuardian = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("WoodGuardian"), 1, false);
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
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WoodGuardian1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WoodGuardian2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WoodGuardian2"), 1f);
            }
		}
		public override void AI()
		{
			Player P = Main.player[npc.target];
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(true);
				P = Main.player[npc.target];
				if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2500)
				{
					npc.ai[0] = 0;
				}
			}
			if (npc.ai[0] < 1)
			{
				npc.velocity.Y = 0;
				npc.velocity.X = 0;
                npc.frame.Y = 672;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1+(int)(npc.lifeMax * 0.001f) : npc.lifeMax;
				if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(npc.Center, P.Center) < 800)
				{
                    npc.ai[0]++;
				}
			}
			else
			{
                npc.direction = P.Center.X < npc.Center.X ? -1 : 1;
                npc.spriteDirection = npc.direction;
                npc.frameCounter++;
                npc.ai[2] += 1+Main.rand.Next(5);
                if (npc.ai[2] < 700)
                {
                    if (npc.velocity.X * npc.direction < 4)
                    {
                        npc.velocity.X += npc.direction * 0.07f;
                    }
                    if (npc.position.Y > P.Center.Y)
                    {
                        npc.directionY = -1;
                    }
                    if (npc.Center.Y < P.position.Y)
                    {
                        npc.directionY = 1;
                    }
                    if (npc.velocity.Y * npc.directionY < 2)
                    {
                        npc.velocity.Y += npc.directionY * 0.3f;
                    }
                    if (npc.frameCounter >= 14)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 96;
                    }
                    if (npc.frame.Y >= 384)
                    {
                        npc.frame.Y = 0;
                    }
                }
                else
                {
                    npc.ai[3]++;
                    if (npc.ai[1] == 0)
                    {
                        npc.ai[1] = Main.rand.Next(2) + 1;
                    }
                    if (npc.ai[1] == 1)
                    {
                        npc.velocity.X = 0;
                        npc.velocity.Y = 0;
                        if (npc.frameCounter >= 12)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y += 96;
                        }
                        if (npc.frame.Y >= 576)
                        {
                            npc.frame.Y = 384;
                        }
                        if (npc.ai[3] % 60 == 0)
                        {
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("LeafHostile"), 7, 0, Main.myPlayer, npc.whoAmI, 0f);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("LeafHostile"), 7, 0, Main.myPlayer, npc.whoAmI, 180f);
                            }
                            if (Main.rand.Next(4) == 0 || Vector2.Distance(npc.Center, P.Center) < 70)
                            {
                                npc.ai[1] = 0;
                                npc.ai[2] = 0;
                                npc.ai[3] = 0;
                            }
                        }
                    }
                    if (npc.ai[1] == 2)
                    {
                        if (npc.velocity.X * npc.direction < 7)
                        {
                            npc.velocity.X += npc.direction * 0.12f;
                        }
                        if (npc.position.Y > P.position.Y - 160)
                        {
                            npc.directionY = -1;
                        }
                        if (npc.Center.Y < P.position.Y - 160)
                        {
                            npc.directionY = 1;
                        }
                        if (npc.velocity.Y * npc.directionY < 2)
                        {
                            npc.velocity.Y += npc.directionY * 0.7f;
                        }
                        if (npc.frameCounter >= 8)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y += 96;
                        }
                        if (npc.frame.Y >= 384)
                        {
                            npc.frame.Y = 0;
                        }
                        if (((Collision.CanHitLine(npc.position, npc.width, npc.height, P.position, P.width, P.height) && npc.ai[3] > 200) || npc.ai[3] > 450) && Math.Abs(P.Center.X - npc.Center.X) < 50 )
                        {
                            npc.noTileCollide = false;
                            npc.noGravity = false;
                            npc.velocity.X = 0;
                            npc.velocity.Y = 9;
                            npc.ai[3] = 0;
                            npc.ai[1] = 3;
                        }
                    }
                    if (npc.ai[1] >= 3)
                    {
                        npc.noTileCollide = false;
                        npc.noGravity = false;
                        if (npc.velocity.Y == 0)
                        {
                            npc.knockBackResist = 0;
                            npc.frame.Y = 672;
                            if (npc.ai[1] == 3)
                            {
                                if (Main.netMode != 1)
                                {
                                    for (int i = 0; i < 14; i++)
                                    {
                                        Projectile.NewProjectile((npc.Center.X - 455) + (i * 70), npc.position.Y, 0, 16, mod.ProjectileType("Root"), 20, 3, Main.myPlayer);
                                    }
                                }
                                npc.ai[1] = 4;
                                npc.ai[3] = 0;
                            }
                        }
                        else
                        {
                            npc.frame.Y = 576;
                        }
                        if (npc.ai[3] > 170)
                        {
                            npc.noTileCollide = true;
                            npc.noGravity = true;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            if (!Main.expertMode)
                            {
                                npc.knockBackResist = 0.1f;
                            }
                        }
                    }
                }
            }
            npc.netUpdate = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[1] == 3)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color = npc.GetAlpha(drawColor) * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 148), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]));
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, rect, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        public override bool CheckActive()
		{
			return false;
		}
	}
}

