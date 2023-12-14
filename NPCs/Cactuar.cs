using JoostMod.Items.Placeable;
using JoostMod.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.NPCs.Town;
using JoostMod.NPCs.Bosses;

namespace JoostMod.NPCs
{
    public class Cactuar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactuar");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 54;
            NPC.damage = 40;
            NPC.defense = 18;
            NPC.lifeMax = 300;
            if (NPC.downedMoonlord)
            {
                NPC.damage = 80;
                NPC.defense = 36;
                NPC.lifeMax = 1200;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 5, 0);
            NPC.knockBackResist = 0.75f;
            NPC.aiStyle = -1;
            NPC.noGravity = false;
            Banner = ModContent.NPCType<Cactuar>();
            BannerItem = ModContent.ItemType<CactuarBanner>();
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.ai[3] < 200)
            {
                if (NPC.velocity.Y == 0)
                {
                    NPC.frameCounter += Math.Abs(NPC.velocity.X);
                    if (NPC.frameCounter > 30)
                    {
                        if (NPC.frame.Y == 0)
                        {
                            NPC.frame.Y = 62;
                        }
                        else
                        {
                            NPC.frame.Y = 0;
                        }
                        NPC.frameCounter = 0;
                    }
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    if (NPC.frame.Y != 62)
                    {
                        NPC.frame.Y = 62;
                    }
                    else
                    {
                        NPC.frame.Y = 124;
                    }
                    NPC.frameCounter = 0;
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == ModContent.NPCType<CactusPerson>() && !NPC.AnyNPCs(ModContent.NPCType<JumboCactuar>()))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Cactus, 1, 8, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Anniversary>(), 50));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("Cactuar1").Type);
                for (int i =  0; i < 4; i++)
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("Cactuar2").Type);
            }

            //The HitEffect hook is client side, these bits will need to be moved
            NPC.ai[3]++;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown && ((!spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.SpawnTileY > Main.worldSurface) && spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneHallow && Main.hardMode ? 0.15f : 0f;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                NPC.ai[3] = 0;
                NPC.netUpdate = true;
            }
            if (NPC.velocity.Y == 0)
            {
                NPC.rotation = 0;
            }
            bool cactusPersonNear = false;
            int cactusPerson = -1;
            if (!NPC.AnyNPCs(ModContent.NPCType<JumboCactuar>()))
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC cactu = Main.npc[k];
                    if (cactu.active && cactu.type == ModContent.NPCType<CactusPerson>() && NPC.Distance(cactu.Center) < 800)
                    {
                        cactusPersonNear = true;
                        cactusPerson = cactu.whoAmI;
                        break;
                    }
                }
            }
            if (((Collision.CanHitLine(NPC.Center, 1, 1, P.position, P.width, P.height) && NPC.Distance(P.Center) < 550) || NPC.ai[3] > 0) && NPC.velocity.Y == 0 && (!cactusPersonNear || NPC.ai[3] > 0))
            {
                NPC.ai[3]++;
            }
            if (NPC.ai[3] <= 0)
            {
                if (cactusPersonNear)
                {
                    NPC.damage = 0;
                }
                else
                {
                    NPC.damage = 40;
                    if (NPC.downedMoonlord)
                    {
                        NPC.damage = 80;
                    }
                    if (Main.expertMode)
                    {
                        NPC.damage *= 2;
                    }
                }
                if (NPC.direction == 0)
                {
                    NPC.direction = 1;
                }
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0 && NPC.localAI[1] < 500)
                {
                    NPC.velocity.Y = -8;
                    if (Main.rand.NextBool(4))
                    {
                        NPC.direction *= -1;
                        NPC.netUpdate = true;
                    }
                }
                if (NPC.velocity.X * NPC.direction < 3)
                {
                    NPC.velocity.X += NPC.direction * 0.11f;
                }
                if (NPC.velocity.X > 3 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = 3;
                }
                if (NPC.velocity.X < -3 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = -3;
                }
                NPC.localAI[1]++;
                if (NPC.localAI[1] > 500)
                {
                    NPC.velocity.X = 0;
                }
                if (NPC.localAI[1] > 600)
                {
                    NPC.localAI[1] = 0;
                    NPC.direction *= -1;
                    NPC.velocity.X += NPC.direction;
                }
                if (NPC.velocity.Y != 0)
                {
                    NPC.rotation = NPC.ai[1] * 0.0174f * NPC.direction;
                    NPC.ai[1] += 20;
                }
                else
                {
                    NPC.ai[1] = 0;
                }
            }
            else if (NPC.ai[3] < 200)
            {
                if (cactusPersonNear && NPC.life >= NPC.lifeMax)
                {
                    NPC.ai[3] = 0;
                }
                NPC.damage = 40;
                if (NPC.downedMoonlord)
                {
                    NPC.damage = 80;
                }
                if (Main.expertMode)
                {
                    NPC.damage *= 2;
                }
                if (NPC.life > NPC.lifeMax * 0.5f || NPC.AnyNPCs(ModContent.NPCType<JumboCactuar>()))
                {
                    NPC.direction = (P.Center.X < NPC.Center.X ? -1 : 1);
                    if (NPC.velocity.Y == 0 && NPC.velocity.X == 0)
                    {
                        NPC.velocity.Y = -8;
                    }
                    if (NPC.velocity.X * NPC.direction < 5.5f)
                    {
                        NPC.velocity.X += NPC.direction * 0.12f;
                    }
                    if (NPC.velocity.X > 5.5f && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = 5.5f;
                    }
                    if (NPC.velocity.X < -5.5f && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = -5.5f;
                    }
                    if (NPC.velocity.Y != 0)
                    {
                        NPC.rotation = NPC.ai[1] * 0.0174f * NPC.direction;
                        NPC.ai[1] += 20;
                    }
                    else
                    {
                        NPC.ai[1] = 0;
                    }
                    NPC.ai[2]++;
                    if (NPC.Distance(P.Center) < 200 && NPC.velocity.Y == 0 && NPC.ai[2] > 80)
                    {
                        NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                        NPC.velocity.X = 7 * NPC.direction;
                        NPC.ai[2] = 0;
                    }
                }
                else
                {
                    float speed = 7f;
                    if (NPC.Distance(P.Center) < 100 && NPC.ai[3] < 200)
                    {
                        NPC.ai[3]--;
                        speed = 9f;
                    }
                    NPC.direction = (P.Center.X < NPC.Center.X ? 1 : -1);
                    if (NPC.velocity.Y == 0 && NPC.velocity.X == 0)
                    {
                        NPC.velocity.Y = -8;
                    }
                    if (NPC.velocity.X * NPC.direction < speed)
                    {
                        NPC.velocity.X += NPC.direction * 0.33f;
                    }
                    if (NPC.velocity.Y != 0)
                    {
                        NPC.rotation = NPC.ai[1] * 0.0174f * NPC.direction;
                        NPC.ai[1] += 20;
                    }
                    else
                    {
                        NPC.ai[1] = 0;
                    }
                }
            }

            if (NPC.ai[3] >= 200 && NPC.velocity.Y == 0)
            {
                NPC.direction = (P.Center.X < NPC.Center.X ? -1 : 1);
                NPC.velocity.X = 0;
                if (NPC.ai[3] >= 210)
                {
                    float speed = 16f;
                    int type = ModContent.ProjectileType<CactusNeedle2>();
                    if (NPC.ai[3] % 5 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item7, NPC.Center);
                    }
                    Vector2 dir = NPC.DirectionTo(P.Center);
                    if (Main.expertMode)
                    {
                        dir = NPC.DirectionTo(P.Center + (P.velocity * (NPC.Distance(P.Center) / speed)));
                    }
                    Vector2 vel = new Vector2(dir.X, dir.Y).RotatedByRandom(MathHelper.ToRadians(3));
                    dir = vel * speed;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, dir.X, dir.Y, type, 1, 0f, Main.myPlayer);
                    }
                }
                if ((NPC.ai[3] >= 220 && !Main.expertMode) || (Main.expertMode && NPC.ai[3] >= 230))
                {
                    NPC.ai[3] = 0;
                    NPC.velocity.X += NPC.direction;
                }
            }

        }
    }
}

