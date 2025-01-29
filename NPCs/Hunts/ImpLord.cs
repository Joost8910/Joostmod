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
using JoostMod.Projectiles.Magic;

namespace JoostMod.NPCs.Hunts
{
	[AutoloadBossHead]
	public class ImpLord : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Imp Lord");
			Main.npcFrameCount[NPC.type] = 24;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }
		public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 46;
			NPC.damage = 30;
			NPC.defense = Main.remixWorld ? 8 : 16;
			NPC.lifeMax = Main.remixWorld ? 900 : 3000;
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
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.SpawnTileY >= Main.UnderworldLayer && (!Main.remixWorld || spawnInfo.SpawnTileX <= Main.maxTilesX * 0.39 + 50.0 || spawnInfo.SpawnTileX >= Main.maxTilesX * 0.61) && !JoostWorld.downedImpLord && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
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
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.ai[1] >= 0;
        }
        public override bool CanHitNPC(NPC target)
        {
            return NPC.ai[1] >= 0;
        }
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (NPC.ai[1] < 0)
            {
                modifiers.SetCrit();
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (NPC.ai[1] < 0 && Main.player[projectile.owner].heldProj == projectile.whoAmI)
            {
                modifiers.SetCrit();
            }
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ModContent.ProjectileType<FireballExplosion>())
            {
                NPC.ai[1] = -250;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
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
                if (Main.rand.NextBool(100))
                {
                    NPC.direction *= -1;
                }
                if (NPC.velocity.X * NPC.direction < 5)
                {
                    NPC.velocity.X += NPC.direction * 0.2f;
                }
                NPC.rotation = MathHelper.ToRadians(NPC.velocity.X);
                NPC.velocity.Y = (float)Math.Sin(NPC.position.X / 60);
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
                if (NPC.ai[1] < 0)
                {
                    if (NPC.ai[1] == -250)
                    {
                        NPC.velocity.X = NPC.direction * -5f;
                    }
                    if (NPC.ai[1] < -20)
                    {
                        NPC.rotation += MathHelper.ToRadians(NPC.velocity.X);
                        if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                        {
                            NPC.velocity.Y = 0;
                            NPC.velocity.X *= 0.95f;
                        }
                        else
                        {
                            if (NPC.velocity.Y < 10)
                            {
                                NPC.velocity.Y += 0.3f;
                            }
                        }
                    }
                    else
                    {
                        NPC.rotation *= 0.86f;
                        NPC.velocity.Y -= 0.1f;
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

                    if (NPC.velocity.X * dirx < (Main.remixWorld ? 5 : 6))
                    {
                        NPC.velocity.X += dirx * 0.3f;
                    }
                    if (NPC.velocity.Y * dir < (Main.remixWorld ? 3 : 4))
                    {
                        NPC.velocity.Y += dir * 0.3f;
                    }
                    NPC.rotation = MathHelper.ToRadians(NPC.velocity.X);
                }

                NPC.ai[1]++;
                int rate = Main.remixWorld ? 32 : 20;
                if (NPC.ai[1] == rate && Main.rand.Next(5) < 3)
                {
                    NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BurningSphere);
                    SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                    NPC.ai[2] = 1;
                }
                if (NPC.ai[1] == 2 * rate && (Main.rand.Next(5) < 4 || Vector2.Distance(NPC.Center, P.Center) > 600 || NPC.Center.Y >= P.position.Y))
                {
                    int damage = Main.remixWorld ? 15 : 20;
                    float shootSpeed = Main.remixWorld ? 9.5f : 12;
                    Projectile.NewProjectile(source, NPC.Center, NPC.DirectionTo(P.Center + new Vector2(P.velocity.X * (Vector2.Distance(P.Center, NPC.Center) / shootSpeed), 0)) * shootSpeed, ModContent.ProjectileType<ImpFireBolt>(), damage, 5, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item45, NPC.Center);
                    NPC.ai[2] = 1;
                }
                if (NPC.ai[1] >= 3 * rate)
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
                    foreach (NPC n in Main.ActiveNPCs)
                    {
                        f = n;
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
                if (NPC.ai[3] < 1 && NPC.ai[1] >= 0 && (Vector2.Distance(P.Center, NPC.Center) < 70 || (fireball && Vector2.Distance(f.Center + f.velocity*9, NPC.Center) < 80)))
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
                        if (!NPC.AnyNPCs(ModContent.NPCType<FireBall>()) && NPC.ai[1] >= 0)
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
                        if (NPC.frame.Y > 3 * frameHeight || NPC.ai[1] < -10)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                    else
                    {
                        if (NPC.frameCounter >= (Main.remixWorld ? 8 : 5))
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

