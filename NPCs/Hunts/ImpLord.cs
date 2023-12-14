using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Hostile;

namespace JoostMod.NPCs.Hunts
{
	[AutoloadBossHead]
	public class ImpLord : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Imp Lord");
			Main.npcFrameCount[NPC.type] = 24;
        }
		public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 46;
			NPC.damage = 30;
			NPC.defense = 16;
			NPC.lifeMax = 3000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0;
			NPC.aiStyle = -1;
			NPC.frameCounter = 0;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.buffImmune[BuffID.OnFire] = true;
        }
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.SpawnTileY >= Main.maxTilesY - 250 && !JoostWorld.downedImpLord && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EvilStone>(), 100));
        }
        public override void OnKill()
        {
            JoostWorld.downedImpLord = true;
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(NPC, ModContent.ItemType<Items.Quest.ImpLord>(), Main.rand, 1, 1, 1, false);
        }
		public override void HitEffect(int hitDirection, double damage)
		{
            if (NPC.ai[0] == 0)
            {
                NPC.ai[0]++;
            }
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpLord1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpLord2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpLord2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpLord3").Type);
            }
        }
        int dir = 1;
        int dirx = 1;
		public override void AI()
		{
            var source = NPC.GetSource_FromAI();
			Player P = Main.player[NPC.target];
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
				P = Main.player[NPC.target];
				if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2000)
				{
					NPC.ai[0] = 0;
				}
			}
			if (NPC.ai[0] < 1)
            {
                if (Main.rand.Next(100) == 0)
                {
                    NPC.direction *= -1;
                }
                if (NPC.velocity.X * NPC.direction < 5)
                {
                    NPC.velocity.X += NPC.direction * 0.2f;
                }
                NPC.velocity.Y = 0;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Vector2.Distance(NPC.Center, P.Center) < 1000 && P.active && !P.dead)
                {
                    NPC.ai[0]++;
                }
            }
			else
			{
                NPC.direction = P.Center.X < NPC.Center.X ? -1 : 1;
                if (P.position.Y > NPC.position.Y + 250)
                {
                    dir = 1;
                }
                if (P.position.Y < NPC.position.Y - 150)
                {
                    dir = -1;
                }
                if (P.Center.X < NPC.Center.X - 250)
                {
                    dirx = -1;
                }
                if (P.Center.X > NPC.Center.X + 250)
                {
                    dirx = 1;
                }

                if (NPC.velocity.X * dirx < 6)
                {
                    NPC.velocity.X += dirx * 0.3f;
                }
                if (NPC.velocity.Y * dir < 4)
                {
                    NPC.velocity.Y += dir * 0.3f;
                }
                NPC.ai[1]++;
                if (NPC.ai[1] == 20 && Main.rand.Next(5) < 3)
                {
                    NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BurningSphere);
                    SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                    NPC.ai[2] = 1;
                }
                if (NPC.ai[1] == 40 && (Main.rand.Next(5) < 4 || Vector2.Distance(NPC.Center, P.Center) > 600 || NPC.Center.Y >= P.position.Y))
                {
                    Projectile.NewProjectile(source, NPC.Center, NPC.DirectionTo(P.Center + new Vector2(P.velocity.X * (Vector2.Distance(P.Center, NPC.Center) / 12), 0)) * 12, ModContent.ProjectileType<ImpFireBolt>(), 20, 5, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item45, NPC.Center);
                    NPC.ai[2] = 1;
                }
                if (NPC.ai[1] >= 60)
                {
                    NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<FireBall>());
                    SoundEngine.PlaySound(SoundID.Item73, NPC.Center);
                    NPC.ai[2] = 1;
                }
                if (NPC.ai[2] > 0)
                {
                    NPC.ai[1] = 0;
                    NPC.ai[2]++;
                    if (NPC.ai[2] > 16)
                    {
                        NPC.ai[2] = 0;
                    }
                }
                int i = 0;
                NPC f = Main.npc[i];
                bool fireball = false;
                if (NPC.AnyNPCs(ModContent.NPCType<FireBall>()))
                {
                    for (i = 0; i < 200; i++)
                    {
                        f = Main.npc[i];
                        if (f.type == ModContent.NPCType<FireBall>())
                        {
                            if (f.friendly)
                            {
                                fireball = true;
                            }
                            break;
                        }
                    }
                    NPC.velocity = Vector2.Zero;
                    NPC.ai[1] = 0;
                }
                if (NPC.ai[3] < 1 && (Vector2.Distance(P.Center, NPC.Center) < 70 || (fireball && Vector2.Distance(f.Center + f.velocity*9, NPC.Center) < 80)))
                {
                    NPC.ai[3]++;
                    Projectile.NewProjectile(source, NPC.Center, NPC.velocity, ModContent.ProjectileType<ImpTail>(), 15, 8, 0, NPC.whoAmI);
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1"), NPC.Center); //230
                }
                if (NPC.ai[3] > 0)
                {
                    NPC.ai[3]++;
                    if (NPC.ai[3] > 16)
                    {
                        if (!NPC.AnyNPCs(ModContent.NPCType<FireBall>()))
                        {
                            Vector2 targetPos = new Vector2((P.Center.X - 250) + Main.rand.Next(500), (P.position.Y - 150) + Main.rand.Next(300));
                            NPC.Teleport(targetPos, 1);
                        }
                        NPC.ai[3] = 0;
                    }
                }
            }
            NPC.netUpdate = true;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 54;
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.ai[3] > 0)
            {
                if (NPC.frameCounter >= 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
                if (NPC.frame.Y < 20 * frameHeight || NPC.frame.Y > 23 * frameHeight)
                {
                    NPC.frame.Y = 20 * frameHeight;
                }
            }
            else
            {
                if (NPC.ai[2] > 0)
                {
                    if (NPC.frameCounter >= 4)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                    }
                    if (NPC.frame.Y > 19 * frameHeight || NPC.frame.Y < 16 * frameHeight)
                    {
                        NPC.frame.Y = 16 * frameHeight;
                    }
                }
                else
                {
                    if (NPC.ai[1] < 1)
                    {
                        if (NPC.frameCounter >= 6)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += frameHeight;
                        }
                        if (NPC.frame.Y > 3 * frameHeight)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                    else
                    {
                        if (NPC.frameCounter >= 5)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += frameHeight;
                        }
                        if (NPC.frame.Y > 15 * frameHeight || NPC.frame.Y < 4 * frameHeight)
                        {
                            NPC.frame.Y = 4 * frameHeight;
                        }
                    }
                }
            }
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            if (NPC.direction == -1)
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

