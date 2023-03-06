using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
			Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 6;
        }
		public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 96;
			NPC.damage = 16;
			NPC.defense = 10;
			NPC.lifeMax = 800;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0.1f;
            if (Main.expertMode)
            {
                NPC.knockBackResist = 0;
            }
			NPC.aiStyle = -1;
			NPC.frameCounter = 0;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
            NPC.netAlways = true;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return (tile.TileType == 2 || tile.TileType == 3 || tile.TileType == 5) && spawnInfo.SpawnTileY <= Main.worldSurface && !spawnInfo.Sky && !JoostWorld.downedWoodGuardian && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.ai[0] > 0 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void OnKill()
		{
			JoostWorld.downedWoodGuardian = true;
            NPC.DropItemInstanced(NPC.position, NPC.Size, Mod.Find<ModItem>("WoodGuardian").Type, 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }
        }
		public override void HitEffect(int hitDirection, double damage)
		{
            if (NPC.ai[0] == 0)
            {
                NPC.ai[0]++;
            }
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/WoodGuardian1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/WoodGuardian2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/WoodGuardian2"), 1f);
            }
		}
		public override void AI()
		{
			Player P = Main.player[NPC.target];
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
				NPC.velocity.Y = 0;
				NPC.velocity.X = 0;
                NPC.frame.Y = 672;
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1+(int)(NPC.lifeMax * 0.001f) : NPC.lifeMax;
				if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(NPC.Center, P.Center) < 800)
				{
                    NPC.ai[0]++;
				}
			}
			else
			{
                NPC.direction = P.Center.X < NPC.Center.X ? -1 : 1;
                NPC.spriteDirection = NPC.direction;
                NPC.frameCounter++;
                NPC.ai[2] += 1+Main.rand.Next(5);
                if (NPC.ai[2] < 700)
                {
                    if (NPC.velocity.X * NPC.direction < 4)
                    {
                        NPC.velocity.X += NPC.direction * 0.07f;
                    }
                    if (NPC.position.Y > P.Center.Y)
                    {
                        NPC.directionY = -1;
                    }
                    if (NPC.Center.Y < P.position.Y)
                    {
                        NPC.directionY = 1;
                    }
                    if (NPC.velocity.Y * NPC.directionY < 2)
                    {
                        NPC.velocity.Y += NPC.directionY * 0.3f;
                    }
                    if (NPC.frameCounter >= 14)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += 96;
                    }
                    if (NPC.frame.Y >= 384)
                    {
                        NPC.frame.Y = 0;
                    }
                }
                else
                {
                    NPC.ai[3]++;
                    if (NPC.ai[1] == 0)
                    {
                        NPC.ai[1] = Main.rand.Next(2) + 1;
                    }
                    if (NPC.ai[1] == 1)
                    {
                        NPC.velocity.X = 0;
                        NPC.velocity.Y = 0;
                        if (NPC.frameCounter >= 12)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += 96;
                        }
                        if (NPC.frame.Y >= 576)
                        {
                            NPC.frame.Y = 384;
                        }
                        if (NPC.ai[3] % 60 == 0)
                        {
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, 0, 0, Mod.Find<ModProjectile>("LeafHostile").Type, 7, 0, Main.myPlayer, NPC.whoAmI, 0f);
                                Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, 0, 0, Mod.Find<ModProjectile>("LeafHostile").Type, 7, 0, Main.myPlayer, NPC.whoAmI, 180f);
                            }
                            if (Main.rand.Next(4) == 0 || Vector2.Distance(NPC.Center, P.Center) < 70)
                            {
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                                NPC.ai[3] = 0;
                            }
                        }
                    }
                    if (NPC.ai[1] == 2)
                    {
                        if (NPC.velocity.X * NPC.direction < 7)
                        {
                            NPC.velocity.X += NPC.direction * 0.12f;
                        }
                        if (NPC.position.Y > P.position.Y - 160)
                        {
                            NPC.directionY = -1;
                        }
                        if (NPC.Center.Y < P.position.Y - 160)
                        {
                            NPC.directionY = 1;
                        }
                        if (NPC.velocity.Y * NPC.directionY < 2)
                        {
                            NPC.velocity.Y += NPC.directionY * 0.7f;
                        }
                        if (NPC.frameCounter >= 8)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += 96;
                        }
                        if (NPC.frame.Y >= 384)
                        {
                            NPC.frame.Y = 0;
                        }
                        if (((Collision.CanHitLine(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) && NPC.ai[3] > 200) || NPC.ai[3] > 450) && Math.Abs(P.Center.X - NPC.Center.X) < 50 )
                        {
                            NPC.noTileCollide = false;
                            NPC.noGravity = false;
                            NPC.velocity.X = 0;
                            NPC.velocity.Y = 9;
                            NPC.ai[3] = 0;
                            NPC.ai[1] = 3;
                        }
                    }
                    if (NPC.ai[1] >= 3)
                    {
                        NPC.noTileCollide = false;
                        NPC.noGravity = false;
                        if (NPC.velocity.Y == 0)
                        {
                            NPC.knockBackResist = 0;
                            NPC.frame.Y = 672;
                            if (NPC.ai[1] == 3)
                            {
                                if (Main.netMode != 1)
                                {
                                    for (int i = 0; i < 14; i++)
                                    {
                                        Projectile.NewProjectile((NPC.Center.X - 455) + (i * 70), NPC.position.Y, 0, 16, Mod.Find<ModProjectile>("Root").Type, 20, 3, Main.myPlayer);
                                    }
                                }
                                NPC.ai[1] = 4;
                                NPC.ai[3] = 0;
                            }
                        }
                        else
                        {
                            NPC.frame.Y = 576;
                        }
                        if (NPC.ai[3] > 170)
                        {
                            NPC.noTileCollide = true;
                            NPC.noGravity = true;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            if (!Main.expertMode)
                            {
                                NPC.knockBackResist = 0.1f;
                            }
                        }
                    }
                }
            }
            NPC.netUpdate = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[1] == 3)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, NPC.height * 0.5f);
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (NPC.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color = NPC.GetAlpha(drawColor) * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * (NPC.frame.Y / 148), TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]));
                    spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, rect, color, NPC.rotation, drawOrigin, NPC.scale, spriteEffects, 0f);
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

