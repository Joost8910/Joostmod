using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Items.Placeable;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Materials;
using JoostMod.Projectiles.Hostile;

namespace JoostMod.NPCs
{
    public class SandGolem : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Golem");
            Main.npcFrameCount[NPC.type] = 17;
        }
        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 104;
            NPC.damage = 60;
            NPC.defense = 32;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.knockBackResist = 0.05f;
            NPC.aiStyle = -1;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DesertGolemBanner>();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 100, 250));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertCore>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FourthAnniversary>(), 10));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("SandGolem1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("SandGolem2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("SandGolem2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("SandGolem3").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("SandGolem3").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("SandGolem4").Type);
            }

            //The HitEffect hook is client side, these bits will need to be moved
            if (NPC.ai[3] <= 0)
            {
                NPC.ai[0]++;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && spawnInfo.Player.ZoneDesert && Main.hardMode ? 0.02f : 0f;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
            if (NPC.ai[0] > 700)
            {
                NPC.ai[0] = 0;
            }
            if (NPC.ai[1] > 700)
            {
                NPC.ai[1] = 0;
                NPC.direction *= -1;
                NPC.velocity.X += NPC.direction * 0.09f;
            }
            if (Collision.CanHitLine(NPC.Center, 1, 1, P.position, P.width, P.height) || (NPC.ai[0] > 0 && NPC.ai[3] <= 0))
            {
                NPC.ai[0]++;
            }

            if (NPC.ai[0] <= 0)
            {
                NPC.ai[1]++;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0 && NPC.ai[1] <= 550)
                {
                    if (Main.rand.NextBool(3))
                    {
                        NPC.direction *= -1;
                    }
                    else
                    {
                        NPC.velocity.Y = -6;
                    }
                }
                if (NPC.velocity.X * NPC.direction < 1.3f)
                {
                    NPC.velocity.X += NPC.direction * 0.09f;
                }
                if (NPC.velocity.X > 1.3f && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = 1.3f;
                }
                if (NPC.velocity.X < -1.3f && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = -1.3f;
                }
                if (NPC.ai[1] > 550)
                {
                    NPC.velocity.X = 0;
                }
            }
            else
            {
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0 && NPC.ai[3] <= 0)
                {
                    NPC.velocity.Y = -6;
                }
                if (NPC.ai[3] < 2)
                {
                    NPC.direction = (P.Center.X < NPC.Center.X ? -1 : 1);
                }
                NPC.ai[1]++;
                if (NPC.life < NPC.lifeMax / 2)
                {
                    NPC.ai[1]++;
                    if (NPC.velocity.X * NPC.direction < 3f)
                    {
                        NPC.velocity.X += NPC.direction * 0.09f;
                    }
                    if (NPC.velocity.X > 2.5f && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = 2.5f;
                    }
                    if (NPC.velocity.X < -2.5f && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = -2.5f;
                    }
                }
                else
                {
                    if (NPC.velocity.X * NPC.direction < 1.6f)
                    {
                        NPC.velocity.X += NPC.direction * 0.09f;
                    }
                    if (NPC.velocity.X > 1.3f && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = 1.3f;
                    }
                    if (NPC.velocity.X < -1.3f && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = -1.3f;
                    }
                }
                if (NPC.ai[1] >= 300)
                {
                    NPC.ai[3] = Main.rand.Next(2) + 1;
                    NPC.ai[1] = 0;
                }
                if (NPC.ai[3] == 1)
                {
                    NPC.ai[2]++;
                    NPC.velocity.X = 0;
                }
            }

            if (NPC.ai[3] == 1)
            {
                if (NPC.ai[1] == 14)
                {
                    float Speed = 9f;
                    Vector2 vector8 = new Vector2(NPC.Center.X, NPC.position.Y + 26);
                    int damage = 30;
                    int type = ModContent.ProjectileType<SandBall>();
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
                }
                if (NPC.ai[1] >= 20)
                {
                    NPC.ai[1] = 0;
                }
            }

            if (NPC.ai[3] == 2)
            {
                NPC.knockBackResist = 0;
                if (NPC.ai[2] < 15)
                {
                    if (NPC.velocity.Y == 0)
                    {
                        NPC.ai[2]++;
                        NPC.velocity.X = 0;
                        NPC.direction = (P.Center.X < NPC.Center.X ? -1 : 1);
                        if (NPC.ai[2] == 14)
                        {
                            NPC.velocity.X = NPC.direction * 6;
                            NPC.velocity.Y = -6;
                            NPC.ai[2] = 15;
                        }
                    }
                    else
                    {
                        NPC.ai[2] = 0;
                    }
                }
                else if (NPC.ai[2] < 20)
                {
                    NPC.ai[2]++;
                }
                if (NPC.ai[2] == 20 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = 0;
                    Vector2 pos = new Vector2(NPC.Center.X, NPC.position.Y + 26);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), pos.X, pos.Y, 4f * NPC.direction, 0, ModContent.ProjectileType<ShockWave>(), 75, 0f, Main.myPlayer);
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        int dustType = 32;
                        int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.velocity.X = dust.velocity.X + Main.rand.Next(-20, 20);
                        dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-20, -5);
                    }
                    NPC.ai[2] += 3;
                }
                if (NPC.ai[2] > 20)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    NPC.ai[2] += 3;
                }
            }
            else
            {
                NPC.knockBackResist = 0.05f;
            }
            if (NPC.ai[2] >= 120)
            {
                NPC.ai[3] = 0;
                NPC.ai[2] = 0;
                NPC.ai[1] = 0;
                NPC.velocity.X += NPC.direction * 0.09f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter += Math.Abs(NPC.velocity.X);
            if (NPC.ai[3] <= 0)
            {
                if (NPC.frameCounter >= 7)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 104);
                }
                if (NPC.frame.Y >= (104 * 7))
                {
                    NPC.frame.Y = 104;
                }
                if (NPC.velocity.X == 0)
                {
                    NPC.frame.Y = 0;
                }
            }
            if (NPC.ai[3] == 1)
            {
                if (NPC.ai[1] < 7)
                {
                    if (NPC.frame.Y < 104 * 7)
                    {
                        NPC.frame.Y = 104 * 7;
                    }
                    else
                    {
                        NPC.frame.Y = 104 * 10;
                    }
                }
                else if (NPC.ai[1] < 14)
                {
                    if (NPC.frame.Y < 104 * 8)
                    {
                        NPC.frame.Y = 104 * 8;
                    }
                    else
                    {
                        NPC.frame.Y = 104 * 11;
                    }
                }
                else if (NPC.ai[1] < 20)
                {
                    NPC.frame.Y = 104 * 9;
                }
            }
            if (NPC.ai[3] == 2)
            {
                if (NPC.ai[2] < 15)
                {
                    NPC.frame.Y = 104 * 12;
                }
                if (NPC.ai[2] >= 15)
                {
                    if (NPC.velocity.Y < 0)
                    {
                        if (NPC.ai[2] < 20)
                        {
                            NPC.frame.Y = 104 * 13;
                        }
                        else
                        {
                            NPC.frame.Y = 104 * 14;
                        }
                    }
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.frame.Y = 104 * 15;
                    }
                }
                if (NPC.ai[2] >= 20 && NPC.velocity.Y == 0)
                {
                    NPC.frame.Y = 104 * 16;
                }
            }
        }
    }
}

