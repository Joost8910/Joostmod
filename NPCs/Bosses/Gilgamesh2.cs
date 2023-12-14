using System;
using System.IO;
using JoostMod.ItemDropRules.DropConditions;
using JoostMod.Items.Armor;
using JoostMod.Items.Consumables;
using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using JoostMod.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class Gilgamesh2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh");
            Main.npcFrameCount[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 90;
            NPC.height = 166;
            NPC.damage = 140;
            NPC.defense = 75;
            NPC.lifeMax = 400000;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = Item.buyPrice(20, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ClashOnTheBigBridge");
            NPC.frameCounter = 0;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            NPC.noGravity = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void OnKill()
        {
            /*
            for (int i = 0; i < 15; i++)
            {
                Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.Heart);
            }
            */

            if (!NPC.AnyNPCs(ModContent.NPCType<Enkidu>()))
            {
                if (!JoostWorld.downedGilgamesh && Main.netMode != NetmodeID.Server)
                    Main.NewText("With Gilgamesh and Enkidu's defeat, you can now fish the legendary stones from their respective biomes", 125, 25, 225);
                JoostWorld.downedGilgamesh = true;
                /*
                if (Main.expertMode)
                {
                    NPC.DropBossBags();
                }
                else
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GenjiToken>(), 1 + Main.rand.Next(2));
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<COTBBMusicBox>());
                    }
                    if (Main.rand.Next(7) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GilgameshMask>());
                    }
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GilgameshTrophy>());
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<FifthAnniversary>(), 1);
                }*/
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule rule = new LeadingConditionRule(new GilgameshDropCondition());
            rule.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<GilgBag>()));
            rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GilgameshTrophy>(), 10));
            rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FifthAnniversary>(), 10));
            rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GenjiToken>(), 1, 1, 3));
            rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<COTBBMusicBox>(), 4));
            rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GilgameshMask>(), 7));
            npcLoot.Add(rule);
            npcLoot.Add(new DropOneByOne(ItemID.Heart, new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 10,
                MaximumItemDropsCount = 15,
            }));
        }


        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.velocity.Y == 0)
            {
                if (NPC.velocity.X == 0)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 6)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + 196);
                    }
                    if (NPC.frame.Y >= 196 * 10 || NPC.frame.Y < 196 * 6)
                    {
                        NPC.frame.Y = 196 * 6;
                    }
                }
                else
                {
                    NPC.frameCounter += Math.Abs(NPC.velocity.X);
                    if (NPC.frameCounter > 46)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + 196);
                    }
                    if (NPC.frame.Y >= 196 * 6)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (NPC.velocity.Y < 0)
                {
                    NPC.frame.Y = 196 * 2;
                }
                else
                {
                    NPC.frame.Y = 196 * 3;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if ((int)NPC.ai[3] % 2 == 1)
            {
                damage = damage / 3;
                crit = false;
            }
            if (NPC.ai[2] > 0)
            {
                damage = (int)(damage * 0.75f);
            }
            /*
            if (npc.ai[3] >= 8)
            {
                Vector2 shieldPos = npc.Center + new Vector2(29 * npc.direction, -45);
                Vector2 vect = npc.ai[1].ToRotationVector2();
                Rectangle shield = new Rectangle((int)(shieldPos.X + (vect.X * 40 * npc.direction)) - 40, (int)(shieldPos.Y + (vect.Y * 40)) - 40, 80, 80);
                if (projectile.Hitbox.Intersects(shield))
                {
                    damage = 0;
                    crit = false;
                    Main.PlaySound(4, npc.Center, 3);
                    Main.PlaySound(SoundID.NPCHit4, npc.Center);
                }
            }*/
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if ((int)NPC.ai[3] % 2 == 1)
            {
                damage = damage / 3;
                crit = false;
            }
            if (NPC.ai[2] > 0)
            {
                damage = (int)(damage * 0.75f);
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (NPC.ai[2] == 3 && NPC.ai[0] >= 60 && NPC.ai[2] < 76)
            {
                damage = (int)(damage * 2f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (NPC.ai[2] == 3 && NPC.ai[0] >= 60 && NPC.ai[2] < 76)
            {
                damage = (int)(damage * 2f);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (NPC.ai[2] == 3 && NPC.localAI[3] < 14 && NPC.ai[3] >= 6)
            {
                NPC.localAI[3] = 14;
                target.immuneTime = 8;
                if (Main.expertMode)
                {
                    target.velocity = Vector2.Zero;
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if ((int)NPC.ai[3] % 2 == 1)
            {
                return false;
            }
            if (NPC.ai[2] == 3 && NPC.localAI[3] > 0 && NPC.ai[3] >= 6)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)NPC.localAI[0]);
            writer.Write((int)NPC.localAI[1]);
            writer.Write((int)NPC.localAI[2]);
            writer.Write((int)NPC.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadInt32();
            NPC.localAI[1] = reader.ReadInt32();
            NPC.localAI[2] = reader.ReadInt32();
            NPC.localAI[3] = reader.ReadInt32();
        }

        public override void AI()
        {
            var sauce = NPC.GetSource_FromAI();
            if (NPC.velocity.Y > 15)
            {
                NPC.velocity.Y = 15;
            }
            NPC.netUpdate = true;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
                P = Main.player[NPC.target];
                if (!P.active || P.dead)
                {
                    NPC.velocity = new Vector2(0f, -100f);
                    NPC.active = false;
                }
            }
            if (NPC.velocity.X > 0)
            {
                NPC.direction = 1;
            }
            if (NPC.velocity.X < 0)
            {
                NPC.direction = -1;
            }
            if (NPC.velocity.X == 0)
            {
                NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
            }
            float moveSpeed = 0;
            if (Math.Abs(P.Center.X - NPC.Center.X) > 80)
            {
                moveSpeed = NPC.Distance(P.Center) / 40;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<Enkidu>() && Main.npc[i].ai[1] >= 900)
                {
                    moveSpeed *= 0.7f;
                    break;
                }
            }
            if (NPC.velocity.Y < 0 && NPC.position.Y > P.position.Y)
            {
                NPC.noTileCollide = true;
            }
            else
            {
                NPC.noTileCollide = false;
            }
            #region Subphases
            if (NPC.ai[3] >= 1 && NPC.ai[3] < 2)
            {
                if (NPC.ai[0] < 300 && NPC.ai[0] >= 190)
                {
                    if (NPC.ai[0] == 210)
                    {
                        SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                        if (NPC.Center.Y < P.position.Y)
                        {
                            NPC.velocity.Y = (Math.Abs((P.position.Y) - (NPC.position.Y + NPC.height)) / 30) - (0.4f * 30 / 2);
                        }
                        else
                        {
                            NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs((P.position.Y) - (NPC.position.Y + NPC.height)));
                        }
                        float vel = moveSpeed + Math.Abs(P.velocity.X);
                        NPC.velocity.X = NPC.Center.X < P.Center.X ? vel : -vel;
                    }
                    if (NPC.ai[0] > 250 || NPC.ai[0] < 210)
                    {
                        if (P.Center.X > NPC.Center.X + 10)
                        {
                            NPC.velocity.X = moveSpeed;
                        }
                        if (P.Center.X < NPC.Center.X - 10)
                        {
                            NPC.velocity.X = -moveSpeed;
                        }
                    }
                    if (NPC.velocity.Y == 0 && NPC.ai[0] > 250)
                    {
                        NPC.ai[0] = 300;
                    }
                }
                else
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 15;
                    NPC.noTileCollide = false;
                }
                NPC.ai[0]++;
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                if (Main.netMode != NetmodeID.Server)
                {
                    if (NPC.ai[0] == 0)
                    {
                        Main.NewText("<Gilgamesh> Now that it's mine,", 225, 25, 25);
                    }
                    if (NPC.ai[0] == 80)
                    {
                        Main.NewText("let's see how good this Excalibur really is!", 225, 25, 25);
                    }


                    if (NPC.ai[0] == 400)
                    {
                        Main.NewText("Ehhh!? Why, I've been had!", 225, 25, 25);
                    }
                    if (NPC.ai[0] == 500)
                    {
                        Main.NewText("This is far from the strongest of swords!", 225, 25, 25);
                    }
                }
                if (NPC.ai[0] == 190)
                {
                    if (Main.netMode != NetmodeID.Server)
                        Main.NewText("Have at you!", 225, 25, 25);

                    NPC.velocity.Y = -20;
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                }
                if (NPC.ai[0] == 235)
                {
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                    float Speed = 24f + NPC.velocity.Length();
                    Vector2 arm = NPC.Center + new Vector2(-26 * NPC.direction, -46);
                    Vector2 pos = PredictiveAim(Speed, arm, false);
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = ModContent.ProjectileType<GilgExcalipoorBeam>();
                    int damage = 1;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                if (NPC.ai[0] == 600)
                {
                    if (Main.netMode != NetmodeID.Server)
                        Main.NewText("Nyeh!", 225, 25, 25);
                    float Speed = 12f;
                    Vector2 arm = NPC.Center + new Vector2(-26 * NPC.direction, -46);
                    Vector2 pos = P.MountedCenter;
                    Vector2 dir = new Vector2(NPC.direction * Speed, -1.5f);
                    int type = ModContent.ProjectileType<GilgExcalipoor>();
                    int damage = 1;
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm, dir, type, damage, 0f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                if (NPC.ai[0] > 630)
                {
                    NPC.ai[3] = 1.5f;
                }
                if (NPC.ai[0] > 700)
                {
                    NPC.ai[3] = 2;
                    NPC.ai[0] = 300;
                }
            }
            else if (NPC.ai[3] == 3)
            {
                NPC.ai[0]++;
                NPC.velocity.X = 0;
                NPC.noTileCollide = false;
                if (NPC.ai[0] > 100)
                {
                    NPC.ai[3] = 4;
                    NPC.ai[0] = 400;
                }
            }
            else if (NPC.ai[3] == 5)
            {
                NPC.ai[0]++;
                NPC.velocity.X = 0;
                NPC.noTileCollide = false;
                if (NPC.ai[0] > 95)
                {
                    NPC.ai[3] = 6;
                    NPC.ai[0] = 400;
                }
            }
            else if (NPC.ai[3] == 7)
            {
                NPC.ai[0]++;
                NPC.velocity.X = 0;
                NPC.noTileCollide = false;
                if (NPC.ai[0] > 95)
                {
                    NPC.ai[3] = 8;
                    NPC.ai[0] = 400;
                }
            }
            else if (NPC.ai[2] <= 0)
            {
                if (NPC.life < NPC.lifeMax * 0.9f && NPC.ai[3] < 1 && NPC.velocity.Y == 0)
                {
                    NPC.ai[3] = 1;
                    NPC.ai[0] = -30;
                    NPC.localAI[0] = 0;
                    NPC.localAI[1] = 0;
                    moveSpeed = 0;
                }
                if (NPC.life < NPC.lifeMax * 0.7f && NPC.ai[3] == 2 && NPC.velocity.Y == 0)
                {
                    NPC.ai[3] = 3;
                    NPC.ai[0] = 0;
                    NPC.localAI[0] = 0;
                    NPC.localAI[1] = 0;
                    moveSpeed = 0;
                }
                if (NPC.life < NPC.lifeMax * 0.5f && NPC.ai[3] == 4 && NPC.velocity.Y == 0)
                {
                    NPC.ai[3] = 5;
                    NPC.ai[0] = 0;
                    NPC.localAI[0] = 0;
                    NPC.localAI[1] = 0;
                    moveSpeed = 0;
                }
                if (NPC.life < NPC.lifeMax * 0.3f && NPC.ai[3] == 6 && NPC.velocity.Y == 0)
                {
                    NPC.ai[3] = 7;
                    NPC.ai[0] = 0;
                    NPC.localAI[0] = 0;
                    NPC.localAI[1] = 0;
                    moveSpeed = 0;
                }
                #endregion
                NPC.ai[0]++;
                NPC.localAI[0]++;
                NPC.localAI[1]++;

                if (NPC.Center.Y > P.Center.Y + 100 && NPC.velocity.Y == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                    NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                }
                if (NPC.position.Y + NPC.height + NPC.velocity.Y < P.position.Y && NPC.velocity.Y >= 0)
                {
                    NPC.position.Y++;
                }
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0 && moveSpeed > 3 && (int)NPC.ai[3] % 2 == 0)
                {
                    NPC.velocity.Y = -10f;
                    NPC.noTileCollide = true;
                }
                if (NPC.velocity.Y >= 0)
                {
                    NPC.noTileCollide = false;
                }
                if (P.Center.X > NPC.Center.X + 10)
                {
                    NPC.velocity.X = moveSpeed;
                }
                if (P.Center.X < NPC.Center.X - 10)
                {
                    NPC.velocity.X = -moveSpeed;
                }

                if (NPC.ai[3] >= 8)
                {
                    Vector2 shieldPos = NPC.Center + new Vector2(29, -45);
                    Vector2 dir = P.MountedCenter - shieldPos;
                    NPC.ai[1] = dir.ToRotation();
                    if (NPC.direction < 0)
                    {
                        NPC.ai[1] += 180f * 0.0174f;
                    }
                    bool shield = false;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<GilgameshShield>() && (int)Main.npc[i].ai[0] == NPC.whoAmI)
                        {
                            shield = true;
                            break;
                        }
                    }
                    if (!shield)
                    {
                        NPC.NewNPC(sauce, (int)shieldPos.X, (int)shieldPos.Y, ModContent.NPCType<GilgameshShield>(), 0, NPC.whoAmI);
                    }
                }
                #region Standard Attacks
                if (NPC.localAI[0] > 90 && NPC.Distance(P.MountedCenter) < 300)
                {
                    float Speed = 18f;
                    Vector2 arm = NPC.Center + new Vector2(35 * NPC.direction, -28);
                    Vector2 pos = P.MountedCenter;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - pos.Y, NPC.Center.X - pos.X);
                    int type = ModContent.ProjectileType<GilgNaginata>();
                    int damage = 50;
                    bool flag = true;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (projectile.type == ModContent.ProjectileType<GilgNaginata>() && projectile.active)
                        {
                            NPC.localAI[0] = 70;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 7f, Main.myPlayer, NPC.whoAmI, 4f);
                        }
                    }
                    NPC.localAI[0] = 0;
                }
                if (NPC.localAI[0] % 90 == 0 && NPC.Distance(P.MountedCenter) < 400)
                {
                    SoundEngine.PlaySound(SoundID.Item18, NPC.position);
                    float Speed = 18f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.MountedCenter.Y, NPC.Center.X - P.MountedCenter.X);
                    int type = ModContent.ProjectileType<GilgFlail>();
                    int damage = 45;
                    bool flag = true;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (Main.projectile[i].type == ModContent.ProjectileType<GilgFlail>() && projectile.active)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && flag)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 15f, Main.myPlayer, NPC.whoAmI, NPC.direction);
                    }
                }
                if (NPC.localAI[1] > 40)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (Main.projectile[i].type == ModContent.ProjectileType<GilgAxe>() && projectile.active)
                        {
                            NPC.localAI[1] = 40;
                        }
                    }
                }
                if (NPC.localAI[1] > 90)
                {
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                    float Speed = 18f + NPC.velocity.Length();
                    Vector2 arm = NPC.Center + new Vector2(39 * NPC.direction, -38);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm, Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = ModContent.ProjectileType<GilgAxe>();
                    int damage = 45;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10f, Main.myPlayer, NPC.whoAmI);
                    }
                    NPC.localAI[1] = 0;
                }
                if (NPC.ai[0] % 90 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item19, NPC.position);
                    float Speed = 12f + NPC.velocity.Length();
                    Vector2 arm = NPC.Center + new Vector2(-27 * NPC.direction, -37);
                    Vector2 pos = P.MountedCenter;
                    if (Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm, true);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = ModContent.ProjectileType<GilgKunai>();
                    int damage = 35;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float spread = 45f * 0.0174f;
                        float baseSpeed = (float)Math.Sqrt((float)((Math.Cos(rotation) * Speed) * -1) * (float)((Math.Cos(rotation) * Speed) * -1) + (float)((Math.Sin(rotation) * Speed) * -1) * (float)((Math.Sin(rotation) * Speed) * -1));
                        double startAngle = Math.Atan2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)) - spread / 3;
                        double deltaAngle = spread / 3;
                        double offsetAngle;
                        for (int i = 0; i < 3; i++)
                        {
                            offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(sauce, arm.X, arm.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, 1f, Main.myPlayer);
                        }
                    }
                }
                if (NPC.ai[0] % 130 == 100 && NPC.ai[3] >= 4)
                {
                    SoundEngine.PlaySound(SoundID.Item41, NPC.position);
                    float Speed = 12f + NPC.velocity.Length();
                    Vector2 arm = NPC.Center + new Vector2(-17 * NPC.direction, -43);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed * 3, arm, false);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = ModContent.ProjectileType<GilgBullet>();
                    int damage = 40;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 1f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                if (NPC.ai[0] % 250 == 168 && NPC.ai[3] >= 6)
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sonic_boom_blade_slash_0"), NPC.Center); //220
                    float Speed = 12f + NPC.velocity.Length();
                    Vector2 arm = NPC.Center + new Vector2(-28 * NPC.direction, -47);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm, true);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = ModContent.ProjectileType<GilgBusterBeam>();
                    int damage = 50;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 1f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                #endregion
                if (NPC.ai[0] == 550)
                {
                    SoundEngine.PlaySound(SoundID.MaxMana, NPC.Center);
                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, 15);
                    }
                }
                if (NPC.ai[0] > 600)
                {
                    int r = 2;
                    if (NPC.ai[3] >= 2)
                    {
                        r++;
                    }
                    if (NPC.ai[3] >= 4)
                    {
                        r++;
                    }
                    if (NPC.ai[3] >= 8)
                    {
                        r++;
                    }
                    int rand = Main.rand.Next(r) + 1;
                    if ((int)NPC.localAI[2] >= Math.Pow(2, r) - 1)
                    {
                        NPC.localAI[2] = 0;
                    }
                    var bit1 = (int)NPC.localAI[2] & (1 << 1 - 1);
                    var bit2 = (int)NPC.localAI[2] & (1 << 2 - 1);
                    var bit3 = (int)NPC.localAI[2] & (1 << 3 - 1);
                    var bit4 = (int)NPC.localAI[2] & (1 << 4 - 1);
                    var bit5 = (int)NPC.localAI[2] & (1 << 5 - 1);
                    if (bit1 > 0 && bit2 > 0 && bit3 > 0 && bit4 > 0 && bit5 > 0)
                    {
                        NPC.localAI[2] = 0;
                    }
                    else
                    {
                        int i = 0;
                        while (i < 100 && ((bit1 > 0 && rand == 1) || (bit2 > 0 && rand == 2) || (bit3 > 0 && rand == 3) || (bit4 > 0 && rand == 4) || (bit5 > 0 && rand == 5)))
                        {
                            rand = Main.rand.Next(r) + 1;
                            i++;
                        }
                    }

                    NPC.ai[2] = rand;
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                    NPC.netUpdate = true;
                }
            }
            #region Special Attacks
            //Bitter End
            if (NPC.ai[2] == 1) 
            {
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                if (NPC.velocity.Y == 0 || NPC.ai[0] > 0)
                {
                    NPC.ai[0]++;
                }
                if (NPC.ai[0] == 30)
                {
                    NPC.noTileCollide = true;
                    NPC.velocity.Y = -40;
                    NPC.velocity.X = 20 * NPC.direction;
                    int damage = 150;
                    int type = ModContent.ProjectileType<BitterEnd>();
                    SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.position.Y - 100, 0f, 0f, type, damage, 15f, Main.myPlayer);
                    }
                }
                if (NPC.ai[0] > 30 && NPC.ai[0] <= 80)
                {
                    NPC.noTileCollide = true;
                    NPC.velocity.Y = -0.399f;
                    NPC.rotation = 30 * 0.0174f * NPC.direction;
                    if (NPC.ai[0] > 60)
                    {
                        NPC.rotation = (30 - (NPC.ai[0] - 60) * 2.5f) * 0.0174f * NPC.direction;
                    }
                }
                if (NPC.ai[0] == 175 && NPC.ai[3] >= 6)
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sonic_boom_blade_slash_0"), NPC.Center); //220
                    float Speed = 12f;
                    Vector2 arm = NPC.Center + new Vector2(-28 * NPC.direction, -47);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed * 2, arm, true);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = ModContent.ProjectileType<GilgBusterLimitBeam>();
                    int damage = 50;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 1f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                if (NPC.ai[0] > 80)
                {
                    NPC.noTileCollide = false;
                    if (NPC.velocity.Y == 0)
                    {
                        if (Math.Abs(NPC.velocity.X) < 0.05f)
                        {
                            NPC.velocity.X = 0;
                            NPC.rotation = 0;
                        }
                        else
                        {
                            NPC.velocity.X *= 0.9f;
                            NPC.rotation = NPC.velocity.X * 0.0174f * -NPC.direction;
                        }
                    }
                }
                if (NPC.ai[0] > 300)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[0] = 0;
                    NPC.rotation = 0;
                    NPC.localAI[2] += 1;
                }
            }
            //Ultimate Illusion
            if (NPC.ai[2] == 2)
            {
                if (NPC.ai[0] != 120)
                {
                    NPC.ai[0]++;
                }
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                if (NPC.ai[0] < 15)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y++;
                }
                if (NPC.ai[0] == 15)
                {
                    NPC.velocity.Y = -31;
                    SoundEngine.PlaySound(SoundID.Item7, NPC.Center);
                    if (NPC.ai[3] >= 6)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sonic_boom_blade_slash_2"), NPC.Center); //222
                        int type = ModContent.ProjectileType<GilgBusterMeleeSlash>();
                        int damage = 75;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, NPC.direction * 20, 0f, type, damage, 0f, Main.myPlayer, NPC.whoAmI, -0.5f);
                        }
                    }
                }
                if (NPC.ai[0] <= 120)
                {
                    if (P.Center.X > NPC.Center.X + 10)
                    {
                        NPC.velocity.X = moveSpeed;
                    }
                    if (P.Center.X < NPC.Center.X - 10)
                    {
                        NPC.velocity.X = -moveSpeed;
                    }
                    if (NPC.ai[3] >= 6)
                    {
                        if (NPC.ai[0] > 40 && NPC.ai[0] < 110)
                        {
                            NPC.velocity.Y = 25;
                            NPC.ai[0] = 110;
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sonic_boom_blade_slash_1"), NPC.Center); //221
                            int type = ModContent.ProjectileType<GilgBusterMeleeSlash>();
                            int damage = 75;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, NPC.direction * 20, 0f, type, damage, 0f, Main.myPlayer, NPC.whoAmI, 0.5f);
                            }
                        }
                        else if (NPC.ai[0] > 15)
                        {
                            if (NPC.ai[0] < 30)
                            {
                                NPC.velocity.Y = -35;
                            }
                            else if (NPC.ai[0] < 40)
                            {
                                NPC.velocity.Y += 1.5f;
                            }
                        }
                    }
                    else if (NPC.velocity.Y > 0 && NPC.ai[0] > 30)
                    {
                        NPC.velocity.Y = 20;
                        NPC.ai[0] = 120;
                    }
                }
                if (NPC.ai[0] == 120 && NPC.velocity.Y == 0)
                {
                    NPC.ai[0]++;
                    NPC.velocity.X = 0;
                    int damage = 150;
                    int type = ModContent.ProjectileType<UltimateIllusion>();
                    SoundEngine.PlaySound(SoundID.Item66, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 22f, 0f, type, damage, 15f, Main.myPlayer);
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, -22f, 0f, type, damage, 15f, Main.myPlayer);
                    }
                }
                if (NPC.ai[0] > 300)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[0] = 0;
                    NPC.localAI[2] += 2;
                }
            }
            //Masamune
            if (NPC.ai[2] == 3)
            {
                NPC.ai[0]++;
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                if (NPC.ai[0] == 10)
                {
                    NPC.velocity.Y = 0;
                }
                if (NPC.ai[0] < 60)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y -= 0.401f;
                }
                if (NPC.ai[0] == 60)
                {
                    float speed = NPC.Distance(P.MountedCenter) / 15;
                    NPC.velocity = NPC.DirectionTo(P.MountedCenter) * speed;
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_3"), NPC.Center); //216
                    NPC.noTileCollide = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 18f * NPC.direction, 0, ModContent.ProjectileType<GilgMasamuneSlash>(), 100, 7f, Main.myPlayer, NPC.whoAmI, -1);
                    }
                }
                if (NPC.Distance(P.MountedCenter) > 2000 && NPC.ai[0] > 80)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = NPC.velocity.Y < 0 ? 1 : NPC.velocity.Y;
                    NPC.noTileCollide = false;
                }
                if (NPC.Distance(P.MountedCenter) < 50 && NPC.localAI[3] < 14 && NPC.ai[0] > 60)
                {
                    NPC.localAI[3] = 14;
                }
                if (NPC.ai[3] >= 6)
                {
                    if (NPC.localAI[3] > 0)
                    {
                        NPC.localAI[3]++;
                    }
                    if (NPC.ai[0] > 60 && NPC.localAI[3] < 40 && NPC.localAI[3] > 0)
                    {
                        if (NPC.localAI[3] == 24)
                        {
                            int type = ModContent.ProjectileType<GilgBusterMeleeSlash>();
                            int damage = 75;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 20f, 0f, type, damage, 0f, Main.myPlayer, NPC.whoAmI);
                            }
                        }
                        if (NPC.localAI[3] == 32)
                        {
                            int type = ModContent.ProjectileType<GilgBusterMeleeSlash>();
                            int damage = 75;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, -20f, 0f, type, damage, 0f, Main.myPlayer, NPC.whoAmI, -1);
                            }
                        }
                        NPC.velocity.X = 0;
                        NPC.velocity.Y = 0.4f;
                        if (NPC.localAI[3] >= 24 && NPC.localAI[3] < 32)
                        {
                            NPC.direction = 1;
                        }
                        if (NPC.localAI[3] >= 32 && NPC.localAI[3] < 40)
                        {
                            NPC.direction = -1;
                        }
                    }
                }
                if (NPC.ai[0] > 60 && NPC.ai[0] < 80)
                {
                    NPC.noTileCollide = true;
                    NPC.velocity.Y -= 0.401f;
                }
                if (NPC.ai[0] >= 90)
                {
                    NPC.noTileCollide = false;
                    NPC.velocity.X *= 0.96f;
                    if (NPC.velocity.Y == 0)
                    {
                        NPC.ai[0] = 200;
                    }
                }
                if (NPC.ai[0] >= 200)
                {
                    NPC.ai[2] = 0;
                    NPC.localAI[3] = 0;
                    NPC.ai[0] = 0;
                    NPC.localAI[2] += 4;
                }
            }
            //A Thousand Swords
            if (NPC.ai[2] == 4)
            {
                NPC.ai[0]++;
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;

                if (Math.Abs(P.Center.X - NPC.Center.X) > 150)
                {
                    moveSpeed = (NPC.Distance(P.Center) - 150) / 60;
                }
                else
                {
                    moveSpeed = 0;
                }
                
                if (NPC.Center.Y > P.Center.Y + 100 && NPC.velocity.Y == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                    NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                }
                if (NPC.position.Y + NPC.height + NPC.velocity.Y < P.position.Y && NPC.velocity.Y >= 0)
                {
                    NPC.position.Y++;
                }
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0 && moveSpeed > 3)
                {
                    NPC.velocity.Y = -10f;
                    NPC.noTileCollide = true;
                }
                if (NPC.velocity.Y >= 0)
                {
                    NPC.noTileCollide = false;
                }
                if (P.Center.X > NPC.Center.X + 90)
                {
                    NPC.velocity.X = moveSpeed;
                }
                if (P.Center.X < NPC.Center.X - 90)
                {
                    NPC.velocity.X = -moveSpeed;
                }

                if (NPC.ai[0] == 30)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                    int damage = 15;
                    int type = ModContent.ProjectileType<GilgPortal>();
                    SoundEngine.PlaySound(SoundID.Item66, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 0f, 0f, type, damage, 1f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                if (NPC.ai[0] > 330)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[0] = 0;
                    NPC.localAI[2] += 8;
                }
            }
            //Gilgamesh's Wrath
            if (NPC.ai[2] == 5)
            {
                NPC.ai[0]++;
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                if (NPC.ai[0] < 100)
                {
                    NPC.velocity.X = 5 * NPC.direction;
                }
                if (NPC.ai[0] == 12)
                {
                    bool flag = true;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (projectile.type == ModContent.ProjectileType<GilgNaginata>() && projectile.active)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Vector2 arm = NPC.Center + new Vector2(35 * NPC.direction, -28);
                        SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(sauce, arm.X, arm.Y, 18f * NPC.direction, 0, ModContent.ProjectileType<GilgNaginata>(), 50, 7f, Main.myPlayer, NPC.whoAmI, 4f);
                        }
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 18f * NPC.direction, 0, ModContent.ProjectileType<GilgMasamuneSlash>(), 100, 7f, Main.myPlayer, NPC.whoAmI, 1);
                    }
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), NPC.Center); //214
                }
                if (NPC.ai[0] == 32)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 18f * NPC.direction, 0, ModContent.ProjectileType<GilgMasamuneSlash>(), 100, 7f, Main.myPlayer, NPC.whoAmI, -1);
                    }
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_3"), NPC.Center); //216
                }
                if (NPC.ai[0] > 12 && NPC.ai[0] < 28)
                {
                    NPC.velocity.X = 25 * NPC.direction;
                }
                else if (NPC.ai[0] > 32 && NPC.ai[0] < 48)
                {
                    NPC.velocity.X = 25 * NPC.direction;
                }
                else
                {
                    NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
                }
                if (NPC.ai[0] == 60)
                {
                    NPC.velocity.Y = -21;
                }
                if (NPC.ai[0] == 100)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                    int damage = 150;
                    int type = ModContent.ProjectileType<GilgWrath>();
                    SoundEngine.PlaySound(SoundID.Item66, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, 0f, 0f, type, damage, 1f, Main.myPlayer, NPC.whoAmI);
                    }
                }
                if (NPC.ai[0] > 100 && NPC.ai[0] < 200)
                {
                    NPC.velocity.X = (P.Center.X - NPC.Center.X) / 50f;
                    NPC.velocity.Y = -0.401f;
                    if (P.position.Y < NPC.Center.Y + 300)
                    {
                        if (P.velocity.Y < -1f * (Math.Abs(P.position.Y - NPC.position.Y - 400) / 30))
                        {
                            NPC.velocity.Y = P.velocity.Y;
                        }
                        else
                        {
                            NPC.velocity.Y = -1f * (Math.Abs(P.position.Y - NPC.position.Y - 400) / 30);
                        }
                    }
                }
                if (NPC.ai[0] == 200)
                {
                    NPC.velocity = NPC.DirectionTo(P.MountedCenter) * -7;
                }
                if (NPC.ai[0] > 200 && NPC.velocity.Y == 0)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[0] = 0;
                    NPC.localAI[2] += 16;
                }
            }
            if (NPC.ai[2] >= 6)
            {
                NPC.ai[2] = 0;
                NPC.ai[0] = 0;
            }
            #endregion
            NPC.noGravity = true;
            NPC.velocity.Y += 0.4f;
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin, bool ignoreY)
        {
            Player P = Main.player[NPC.target];
            Vector2 vel = (ignoreY ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Player P = Main.player[NPC.target];
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.direction == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));

            SpriteEffects ribbonEffects = SpriteEffects.None;
            int dir = NPC.direction;
            if (NPC.velocity == Vector2.Zero)
            {
                dir = -NPC.direction;
            }
            if (dir == 1)
            {
                ribbonEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                ribbonEffects = SpriteEffects.None;
            }
            Texture2D ribbonTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_Ribbons").Value;
            int totalRibbonFrames = 6;
            int ribbonFrame = 0;
            float ribbonRotation = 0;
            Vector2 ribbonOffset = new Vector2(0, 0);

            ribbonFrame = (int)(((int)(NPC.ai[0] * 1.2f) % 60) / 10);

            Rectangle ribbonRect = new Rectangle(0, ribbonFrame * (ribbonTex.Height / totalRibbonFrames), (ribbonTex.Width), (ribbonTex.Height / totalRibbonFrames));
            Vector2 ribbonVect = new Vector2((float)ribbonTex.Width / 2, (float)ribbonTex.Height / (2 * totalRibbonFrames));


            Texture2D flailTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_FlailArm").Value;
            int totalFlailFrames = 2;
            int flailFrame = 0;
            float flailRotation = 0;
            Vector2 flailOffset = new Vector2(40, -46);

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == ModContent.ProjectileType<GilgFlail>() && projectile.active)
                {
                    flailFrame = 1;
                    Vector2 pos = NPC.Center + new Vector2(flailOffset.X * NPC.direction, flailOffset.Y);
                    flailRotation = (float)Math.Atan2(pos.Y - projectile.Center.Y, pos.X - projectile.Center.X) + ((NPC.direction == 1 ? 145f : 35f) * 0.0174f);
                    break;
                }
            }

            Rectangle flailRect = new Rectangle(0, flailFrame * (flailTex.Height / totalFlailFrames), (flailTex.Width), (flailTex.Height / totalFlailFrames));
            Vector2 flailVect = new Vector2((float)flailTex.Width / 2, (float)flailTex.Height / (2 * totalFlailFrames));

            Texture2D naginataTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_NaginataArm").Value;
            int totalNaginataFrames = 3;
            int naginataFrame = 0;
            float naginataRotation = 0;
            Vector2 naginataOffset = new Vector2(34, -44);

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == ModContent.ProjectileType<GilgNaginata>() && projectile.active)
                {
                    Vector2 pos = NPC.Center + new Vector2(naginataOffset.X * NPC.direction, naginataOffset.Y);
                    naginataRotation = (float)Math.Atan2(pos.Y - projectile.Center.Y, pos.X - projectile.Center.X) + (NPC.direction == 1 ? 3.14f : 0);
                    if (projectile.timeLeft > 25 || projectile.timeLeft < 15)
                    {
                        naginataFrame = 1;
                    }
                    else
                    {
                        naginataFrame = 2;
                    }
                    break;
                }
            }
            Rectangle naginataRect = new Rectangle(0, naginataFrame * (naginataTex.Height / totalNaginataFrames), (naginataTex.Width), (naginataTex.Height / totalNaginataFrames));
            Vector2 naginataVect = new Vector2((float)naginataTex.Width / 2, (float)naginataTex.Height / (2 * totalNaginataFrames));

            Texture2D axeTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_AxeArm").Value;
            int totalAxeFrames = 3;
            int axeFrame = 0;
            float axeRotation = 0;
            Vector2 axeOffset = new Vector2(39, -53);
            
            Vector2 arm = NPC.Center + new Vector2(axeOffset.X * NPC.direction, axeOffset.Y);
            Vector2 predictedPos = P.MountedCenter;
            if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
            {
                predictedPos = PredictiveAim(24f, arm, Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200);
            }
            float rotation = (float)Math.Atan2(NPC.Center.Y - predictedPos.Y, NPC.Center.X - predictedPos.X);
            if (NPC.direction == 1)
            {
                rotation += 3.14f;
            }

            if (NPC.localAI[1] > 45 && NPC.localAI[1] <= 90)
            {
                axeRotation = ((NPC.localAI[1] - 45f) * 3 * 0.0174f * -NPC.direction) + rotation;
                if (NPC.localAI[1] > 80)
                {
                    axeFrame = 1;
                    axeRotation = ((105 * 0.0174f * -NPC.direction) + rotation) - ((NPC.localAI[1] - 80f) * 10.5f * 0.0174f * -NPC.direction);
                }
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == ModContent.ProjectileType<GilgAxe>() && projectile.active)
                {
                    axeFrame = 2;
                    axeRotation = (float)Math.Atan2(arm.Y - projectile.Center.Y, arm.X - projectile.Center.X) + (NPC.direction == 1 ? 3.14f : 0);
                    break;
                }
            }

            Rectangle axeRect = new Rectangle(0, axeFrame * (axeTex.Height / totalAxeFrames), (axeTex.Width), (axeTex.Height / totalAxeFrames));
            Vector2 axeVect = new Vector2((float)axeTex.Width / 2, (float)axeTex.Height / (2 * totalAxeFrames));

            Texture2D shieldTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_ShieldArm").Value;
            int totalShieldFrames = 3;
            int shieldFrame = 0;
            float shieldRotation = 0;
            Vector2 shieldOffset = new Vector2(29, -45);

            if (NPC.ai[3] == 7)
            {
                if (NPC.ai[0] > 30)
                {
                    shieldRotation = ((135f * 0.014f) + (NPC.ai[0] - 30) * 0.0174f * 6f) * -NPC.direction;
                    shieldFrame = 1;
                }
                if (NPC.ai[0] > 45)
                {
                    shieldRotation = ((225 * 0.0174f) - (NPC.ai[0] - 45) * 0.0174f * 15f) * -NPC.direction;
                    shieldFrame = 2;
                }
                if (NPC.ai[0] > 51)
                {
                    shieldRotation = 135f * 0.0174f * -NPC.direction;
                    shieldFrame = 2;
                }
                if (NPC.ai[0] > 85)
                {
                    shieldRotation = ((135f * 0.0174f) - (NPC.ai[0] - 85) * 0.0174f * 9f) * -NPC.direction;
                    shieldFrame = 2;
                }
            }
            if(NPC.ai[3] >= 8)
            {
                shieldRotation = NPC.ai[1];
                shieldFrame = 2;
            }

            Rectangle shieldRect = new Rectangle(0, shieldFrame * (shieldTex.Height / totalShieldFrames), (shieldTex.Width), (shieldTex.Height / totalShieldFrames));
            Vector2 shieldVect = new Vector2((float)shieldTex.Width / 2, (float)shieldTex.Height / (2 * totalShieldFrames));


            if (NPC.frame.Y == 196 || NPC.frame.Y == 196 * 4)
            {
                ribbonOffset.Y -= 8;
                flailOffset.Y -= 8;
                naginataOffset.Y -= 8;
                axeOffset.Y -= 8;
                shieldOffset.Y -= 8;
            }
            if (NPC.frame.Y == 196 * 2 || NPC.frame.Y == 196 * 5)
            {
                ribbonOffset.Y -= 2;
                flailOffset.Y -= 2;
                naginataOffset.Y -= 2;
                axeOffset.Y -= 2;
                shieldOffset.Y -= 2;
            }
            if (NPC.frame.Y == 0 || NPC.frame.Y == 196 * 3)
            {
                ribbonOffset.Y += 2;
                flailOffset.Y += 2;
                naginataOffset.Y += 2;
                axeOffset.Y += 2;
                shieldOffset.Y += 2;
            }

            if (NPC.ai[2] >= 1)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, (TextureAssets.Npc[NPC.type].Value.Height * 0.5f) / Main.npcFrameCount[NPC.type]);
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, 0);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * (NPC.frame.Y / 148), TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type])); spriteBatch.Draw(ribbonTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * ribbonOffset.X, NPC.scale * ribbonOffset.Y), new Rectangle?(ribbonRect), color, ribbonRotation, ribbonVect, NPC.scale, ribbonEffects, 0f);

                    Vector2 ribbonOffset2 = ribbonOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 flailOffset2 = flailOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 naginataOffset2 = naginataOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 axeOffset2 = axeOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 shieldOffset2 = shieldOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);

                    float ribbonRotation2 = ribbonRotation + NPC.oldRot[i];
                    float flailRotation2 = flailRotation + NPC.oldRot[i];
                    float naginataRotation2 = naginataRotation + NPC.oldRot[i];
                    float axeRotation2 = axeRotation + NPC.oldRot[i];
                    float shieldRotation2 = shieldRotation + NPC.oldRot[i];

                    spriteBatch.Draw(ribbonTex, drawPos + new Vector2(NPC.scale * NPC.direction * ribbonOffset2.X, NPC.scale * ribbonOffset2.Y), new Rectangle?(ribbonRect), color2, ribbonRotation2, ribbonVect, NPC.scale, ribbonEffects, 0f);
                    spriteBatch.Draw(flailTex, drawPos + new Vector2(NPC.scale * NPC.direction * flailOffset2.X, NPC.scale * flailOffset2.Y), new Rectangle?(flailRect), color2, flailRotation2, flailVect, NPC.scale, effects, 0f);
                    spriteBatch.Draw(naginataTex, drawPos + new Vector2(NPC.scale * NPC.direction * naginataOffset2.X, NPC.scale * naginataOffset2.Y), new Rectangle?(naginataRect), color2, naginataRotation2, naginataVect, NPC.scale, effects, 0f);
                    spriteBatch.Draw(axeTex, drawPos + new Vector2(NPC.scale * NPC.direction * axeOffset2.X, NPC.scale * axeOffset2.Y), new Rectangle?(axeRect), color2, axeRotation2, axeVect, NPC.scale, effects, 0f);
                    spriteBatch.Draw(shieldTex, drawPos + new Vector2(NPC.scale * NPC.direction * shieldOffset2.X, NPC.scale * shieldOffset2.Y), new Rectangle?(shieldRect), color2, shieldRotation2, shieldVect, NPC.scale, effects, 0f);
                    spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos + new Vector2(0f, -7), rect, color2, NPC.oldRot[i], drawOrigin, NPC.scale, effects, 0f);
                }
            }

            ribbonOffset = ribbonOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            flailOffset = flailOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            naginataOffset = naginataOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            axeOffset = axeOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            shieldOffset = shieldOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);

            ribbonRotation += NPC.rotation;
            flailRotation += NPC.rotation;
            naginataRotation += NPC.rotation;
            axeRotation += NPC.rotation;
            shieldRotation += NPC.rotation;

            spriteBatch.Draw(ribbonTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * ribbonOffset.X, NPC.scale * ribbonOffset.Y), new Rectangle?(ribbonRect), color, ribbonRotation, ribbonVect, NPC.scale, ribbonEffects, 0f);
            spriteBatch.Draw(flailTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * flailOffset.X, NPC.scale * flailOffset.Y), new Rectangle?(flailRect), color, flailRotation, flailVect, NPC.scale, effects, 0f);
            spriteBatch.Draw(naginataTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * naginataOffset.X, NPC.scale * naginataOffset.Y), new Rectangle?(naginataRect), color, naginataRotation, naginataVect, NPC.scale, effects, 0f);
            spriteBatch.Draw(axeTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * axeOffset.X, NPC.scale * axeOffset.Y), new Rectangle?(axeRect), color, axeRotation, axeVect, NPC.scale, effects, 0f);
            spriteBatch.Draw(shieldTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * shieldOffset.X, NPC.scale * shieldOffset.Y), new Rectangle?(shieldRect), color, shieldRotation, shieldVect, NPC.scale, effects, 0f);

            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Player P = Main.player[NPC.target];
            SpriteEffects kunaiEffects = SpriteEffects.None;
            int dir = NPC.direction;
            float kun = NPC.ai[0] % 90;
            if ((int)NPC.ai[3] % 2 == 1 || NPC.ai[2] > 0)
            {
                kun = 15;
            }
            if (kun > 45 || kun < 15)
            {
                dir *= -1;
            }
            if (dir == 1)
            {
                kunaiEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                kunaiEffects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));

            Texture2D kunaiTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_KunaiArm").Value;
            int totalKunaiFrames = 3;
            int kunaiFrame = 0;
            float kunaiRotation = 0;
            Vector2 kunaiOffset = new Vector2(-26, -52);

            Player player = Main.player[NPC.target];
            Vector2 arm = NPC.Center + new Vector2(kunaiOffset.X * NPC.direction, kunaiOffset.Y);
            Vector2 pos = player.MountedCenter;
            float Speed = 12f + NPC.velocity.Length();
            if (Math.Abs(NPC.Center.Y - player.MountedCenter.Y) < 200)
            {
                pos = PredictiveAim(Speed, arm, Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200);
            }
            float rotation = (float)Math.Atan2(NPC.Center.Y - pos.Y, NPC.Center.X - pos.X);
            if (NPC.direction == 1)
            { 
                rotation += 3.14f;
            }
            if (kun > 45 && kun <= 90)
            {
                kunaiRotation = ((kun - 45f) * 3 * 0.0174f * -NPC.direction) + rotation;
                if (kun > 80)
                {
                    kunaiFrame = 1;
                    kunaiRotation = ((105 * 0.0174f * -NPC.direction) + rotation) - ((kun - 80f) * 10.5f * 0.0174f * -NPC.direction);
                }
            }
            if (kun < 15)
            {
                kunaiRotation = rotation;
                kunaiFrame = 2;
            }

            Rectangle kunaiRect = new Rectangle(0, kunaiFrame * (kunaiTex.Height / totalKunaiFrames), (kunaiTex.Width), (kunaiTex.Height / totalKunaiFrames));
            Vector2 kunaiVect = new Vector2((float)kunaiTex.Width / 2, (float)kunaiTex.Height / (2 * totalKunaiFrames));


            SpriteEffects excalipoorEffects = SpriteEffects.None;
            dir = NPC.direction;
            if (dir == 1)
            {
                excalipoorEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                excalipoorEffects = SpriteEffects.None;
            }
            Texture2D excalipoorTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_ExcalipoorArms").Value;
            int totalExcalipoorFrames = 2;
            int excalipoorFrame = 0;
            float excalipoorRotation = 0;
            Vector2 excalipoorOffset = new Vector2(-26, -46);

            if (NPC.ai[3] >= 1 && NPC.ai[3] < 1.5f)
            {
                if (NPC.ai[0] > 210)
                {
                    excalipoorRotation = (NPC.ai[0] - 210) * 0.0174f * 8 * -NPC.direction;
                }
                if (NPC.ai[0] > 235)
                {
                    excalipoorRotation = ((25 * 0.0174f * 8) - (NPC.ai[0] - 235) * 0.0174f * 13f) * -NPC.direction;
                }
                if (NPC.ai[0] > 250)
                {
                    excalipoorRotation = 0;
                }
                if (NPC.ai[0] > 335)
                {
                    excalipoorRotation = (NPC.ai[0] - 335) * 0.0174f * 6f * -NPC.direction;
                }
                if (NPC.ai[0] > 350)
                {
                    excalipoorRotation = 90 * 0.0174f * -NPC.direction;
                }

                if (NPC.ai[0] > 580)
                {
                    excalipoorRotation = ((90 * 0.0174f) + (NPC.ai[0] - 580) * 0.0174f * 11) * -NPC.direction;
                }
                if (NPC.ai[0] > 590)
                {
                    excalipoorRotation = ((25 * 0.0174f * 10) - (NPC.ai[0] - 590) * 0.0174f * 11.5f) * -NPC.direction;
                }
                if (NPC.ai[0] > 600)
                {
                    excalipoorRotation = 135f * 0.0174f * -NPC.direction;
                    excalipoorFrame = 1;
                }
            }


            Rectangle excalipoorRect = new Rectangle(0, excalipoorFrame * (excalipoorTex.Height / totalExcalipoorFrames), (excalipoorTex.Width), (excalipoorTex.Height / totalExcalipoorFrames));
            Vector2 excalipoorVect = new Vector2((float)excalipoorTex.Width / 2, (float)excalipoorTex.Height / (2 * totalExcalipoorFrames));


            dir = NPC.direction;
            SpriteEffects busterSwordEffects = SpriteEffects.None;

            Texture2D busterSwordTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_BusterSwordArm").Value;
            int totalBusterSwordFrames = 5;
            int busterSwordFrame = 0;
            float busterSwordRotation = 0;
            Vector2 busterSwordOffset = new Vector2(-28, -47);

            if (NPC.ai[3] == 5)
            {
                if (NPC.ai[0] > 30)
                {
                   busterSwordRotation = ((135f * 0.014f) + (NPC.ai[0] - 30) * 0.0174f * 6f) * -NPC.direction;
                   busterSwordFrame = 1;
                }
                if (NPC.ai[0] > 45)
                {
                   busterSwordRotation = ((225 * 0.0174f) - (NPC.ai[0] - 45) * 0.0174f * 15f) * -NPC.direction;
                   busterSwordFrame = 2;
                }
                if (NPC.ai[0] > 51)
                {
                   busterSwordRotation = 135f * 0.0174f * -NPC.direction;
                   busterSwordFrame = 2;
                }
                if (NPC.ai[0] > 85)
                {
                   busterSwordRotation = ((135f * 0.0174f) - (NPC.ai[0] - 85) * 0.0174f * 9f) * -NPC.direction;
                   busterSwordFrame = 2;
                }
            }
            if (NPC.ai[3] >= 6)
            {
                busterSwordFrame = 2;
                busterSwordRotation = 45f * 0.0174f * -NPC.direction;
                if (NPC.ai[2] <= 0)
                {
                    if (NPC.ai[0] % 250 > 145 && NPC.ai[0] % 250 < 200)
                    {
                        busterSwordFrame = 3;
                        if (NPC.ai[0] % 250 <= 160)
                        {
                            busterSwordRotation = ((45f * 0.0174f) + (((NPC.ai[0] % 250) - 145) * 0.0174f * -6f)) * -NPC.direction;
                        }
                        if (NPC.ai[0] % 250 > 160)
                        {
                            dir *= -1;
                            busterSwordRotation = (((NPC.ai[0] % 250) - 160) * 0.0174f * 10f) * -NPC.direction;
                        }
                        if (NPC.ai[0] % 250 > 176)
                        {
                            busterSwordRotation = 160f * 0.0174f * -NPC.direction;
                        }
                        if (NPC.ai[0] % 250 > 186)
                        {
                            busterSwordFrame = 2;
                            dir *= -1;
                            busterSwordRotation = ((115f * 0.0174f) - (((NPC.ai[0] % 250) - 186) * 0.0174f * 5f)) * -NPC.direction;
                        }
                    }
                }
                if (NPC.ai[2] > 0 || NPC.ai[0] > 550)
                {
                    busterSwordFrame = 4;
                }
                if (NPC.ai[2] == 1)
                {
                    int b = 150;
                    if (NPC.ai[0] > b && NPC.ai[0] % 250 < 200)
                    {
                        if (NPC.ai[0] <= b + 15)
                        {
                            busterSwordRotation = ((45f * 0.0174f) + ((NPC.ai[0] - b) * 0.0174f * -6f)) * -NPC.direction;
                        }
                        if (NPC.ai[0] > b + 15)
                        {
                            dir *= -1;
                            busterSwordRotation = ((NPC.ai[0] - (b + 15)) * 0.0174f * 10f) * -NPC.direction;
                        }
                        if (NPC.ai[0] > b + 31)
                        {
                            busterSwordRotation = 160f * 0.0174f * -NPC.direction;
                        }
                        if (NPC.ai[0] > b + 41)
                        {
                            busterSwordFrame = 2;
                            dir *= -1;
                            busterSwordRotation = ((115f * 0.0174f) - ((NPC.ai[0] - (b + 41)) * 0.0174f * 5f)) * -NPC.direction;
                        }
                        if (NPC.ai[0] > b + 55)
                        {
                            busterSwordRotation = 45f * 0.0174f * -NPC.direction;
                        }
                    }
                }
                if (NPC.ai[2] == 2)
                {
                    if (NPC.ai[0] <= 15)
                    {
                        busterSwordRotation = ((45f * 0.0174f) + (NPC.ai[0] * 0.0174f * -6f)) * -NPC.direction;
                    }
                    if (NPC.ai[0] > 15)
                    {
                        dir *= -1;
                        busterSwordRotation = ((NPC.ai[0] - 15) * 0.0174f * 12f) * -NPC.direction;
                    }
                    if (NPC.ai[0] > 30)
                    {
                        busterSwordRotation = 180f * 0.0174f * -NPC.direction;
                    }
                    if (NPC.ai[0] >= 110)
                    {
                        dir *= -1;
                        busterSwordRotation = ((180 * 0.0174f) - ((NPC.ai[0] - 110) * 0.0174f * 12f)) * -NPC.direction;
                    }
                    if (NPC.ai[0] >= 120)
                    {
                        busterSwordRotation = (30f * 0.0174f) * -NPC.direction;
                    }
                }
                if (NPC.ai[2] == 3)
                {
                    if (NPC.localAI[3] >= 14 && NPC.localAI[3] < 24)
                    {
                        busterSwordRotation = ((45f * 0.0174f) + ((NPC.localAI[3] - 8) * 0.0174f * 9f)) * -NPC.direction;
                    }
                    if (NPC.localAI[3] >= 24)
                    {
                        busterSwordRotation = ((135f * 0.0174f) + ((NPC.localAI[3] - 24) * 0.0174f * -22.5f)) * -NPC.direction;
                    }
                    if (NPC.localAI[3] >= 32)
                    {
                        dir *= -1;
                        busterSwordRotation = ((-45f * 0.0174f) + ((NPC.localAI[3] - 32) * 0.0174f * 22.5f)) * -NPC.direction;
                    }
                    if (NPC.localAI[3] >= 40)
                    {
                        dir *= -1;
                        busterSwordFrame = 2;
                        busterSwordRotation = 135f * -NPC.direction;
                    }
                    if (NPC.localAI[3] >= 50)
                    {
                        busterSwordRotation = ((135f * 0.0174f) + ((NPC.localAI[3] - 50) * 0.0174f * -6f)) * -NPC.direction;
                    }
                    if (NPC.localAI[3] >= 65)
                    {
                        busterSwordRotation = 45f * 0.0174f * -NPC.direction;
                    }
                }
            }
            if (dir == 1)
            {
                busterSwordEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                busterSwordEffects = SpriteEffects.None;
            }
            Rectangle busterSwordRect = new Rectangle(0, busterSwordFrame * (busterSwordTex.Height / totalBusterSwordFrames), (busterSwordTex.Width), (busterSwordTex.Height / totalBusterSwordFrames));
            Vector2 busterSwordVect = new Vector2((float)busterSwordTex.Width / 2, (float)busterSwordTex.Height / (2 * totalBusterSwordFrames));

            SpriteEffects masamuneEffects = SpriteEffects.None;
            dir = NPC.direction;
            if (dir == 1)
            {
                masamuneEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                masamuneEffects = SpriteEffects.None;
            }
            Texture2D masamuneTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_MasamuneArm").Value;
            int totalMasamuneFrames = 5;
            int masamuneFrame = 0;
            float masamuneRotation = 0;
            Vector2 masamuneOffset = new Vector2(-24, -43);

            if (NPC.ai[3] >= 1.5f && NPC.ai[3] < 2)
            {
                if (NPC.ai[0] > 630)
                {
                    masamuneRotation = ((135f * 0.014f) + (NPC.ai[0] - 630) * 0.0174f * 6f) * -NPC.direction;
                    masamuneFrame = 1;
                }
                if (NPC.ai[0] > 645)
                {
                    masamuneRotation = ((225 * 0.0174f) - (NPC.ai[0] - 645) * 0.0174f * 15f) * -NPC.direction;
                    masamuneFrame = 2;
                }
                if (NPC.ai[0] > 651)
                {
                    masamuneRotation = 135f * 0.0174f * -NPC.direction;
                    masamuneFrame = 2;
                }
                if (NPC.ai[0] > 685)
                {
                    masamuneRotation = ((135f * 0.0174f) - (NPC.ai[0] - 685) * 0.0174f * 9f) * -NPC.direction;
                    masamuneFrame = 2;
                }
            }
            if (NPC.ai[3] >= 2)
            {
                masamuneFrame = 2;
            }
            if(NPC.ai[2] == 3)
            {
                if (NPC.ai[0] <= 15)
                {
                    masamuneRotation = (NPC.ai[0] * 0.0174f * 6f) * NPC.direction;
                }
                if (NPC.ai[0] > 15)
                {
                    masamuneFrame = 3;
                }
                if (NPC.ai[0] > 60)
                {
                    masamuneFrame = 3;
                    masamuneRotation = ((NPC.ai[0] - 60) * 0.0174f * 13.125f) * -NPC.direction;
                }
                if (NPC.ai[0] > 68)
                {
                    masamuneFrame = 4;
                    masamuneRotation = (-135f * 0.0174f) + ((NPC.ai[0] - 68) * 0.0174f * 13.125f) * -NPC.direction;
                }
                if (NPC.ai[0] > 76)
                {
                    masamuneFrame = 4;
                    masamuneRotation = (-33.75f * 0.0174f) * -NPC.direction;
                }
            }
            if (NPC.ai[2] == 5)
            {
                if (NPC.ai[0] <= 12)
                {
                    masamuneRotation = (NPC.ai[0] * 0.0174f * 7.5f) * -NPC.direction;
                }
                if (NPC.ai[0] > 12)
                {
                    masamuneRotation = ((90f * 0.0174f) - ((NPC.ai[0] - 12) * 0.0174f * 11.25f)) * -NPC.direction;
                }
                if (NPC.ai[0] > 28)
                {
                    masamuneRotation = (-90 * 0.0174f) * -NPC.direction;
                    masamuneFrame = 3;
                }
                if (NPC.ai[0] > 32)
                {
                    masamuneFrame = 3;
                    masamuneRotation = (NPC.ai[0] - 32) * 0.0174f * 13.125f * -NPC.direction;
                }
                if (NPC.ai[0] > 40)
                {
                    masamuneFrame = 4;
                    masamuneRotation = ((-135f * 0.0174f) + ((NPC.ai[0] - 40) * 0.0174f * 13.125f)) * -NPC.direction;
                }
                if (NPC.ai[0] > 48)
                {
                    masamuneFrame = 4;
                    masamuneRotation = (-33.75f * 0.0174f) * -NPC.direction;
                }
                if (NPC.ai[0] > 60)
                {
                    masamuneFrame = 2;
                    masamuneRotation = 0;
                }
                if (NPC.ai[0] > 80)
                {
                    masamuneRotation = ((NPC.ai[0] - 80) * 0.0174f * 10f) * -NPC.direction;
                }
                if (NPC.ai[0] > 100)
                {
                    masamuneRotation = 200 * 0.0174f * -NPC.direction;
                }
                if (NPC.ai[0] > 200)
                {
                    masamuneRotation = ((200 * 0.0174f) - ((NPC.ai[0] - 200) * 0.0174f * 10f)) * -NPC.direction;
                }
                if (NPC.ai[0] > 220)
                { 
                    masamuneRotation = 0;
                }

            }

            Rectangle masamuneRect = new Rectangle(0, masamuneFrame * (masamuneTex.Height / totalMasamuneFrames), (masamuneTex.Width), (masamuneTex.Height / totalMasamuneFrames));
            Vector2 masamuneVect = new Vector2((float)masamuneTex.Width / 2, (float)masamuneTex.Height / (2 * totalMasamuneFrames));


            SpriteEffects gunBladeEffects = SpriteEffects.None;
            dir = NPC.direction;
            if (dir == 1)
            {
                gunBladeEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                gunBladeEffects = SpriteEffects.None;
            }
            Texture2D gunBladeTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_GunBladeArm").Value;
            int totalGunBladeFrames = 3;
            int gunBladeFrame = 0;
            float gunBladeRotation = 0;
            Vector2 gunBladeOffset = new Vector2(-17, -43);

            if (NPC.ai[3] == 3)
            {
                if (NPC.ai[0] > 30)
                {
                    gunBladeRotation = ((135f * 0.014f) + (NPC.ai[0] - 30) * 0.0174f * 6f) * -NPC.direction;
                    gunBladeFrame = 1;
                }
                if (NPC.ai[0] > 45)
                {
                    gunBladeRotation = ((225 * 0.0174f) - (NPC.ai[0] - 45) * 0.0174f * 15f) * -NPC.direction;
                    gunBladeFrame = 2;
                }
                if (NPC.ai[0] > 51)
                {
                    gunBladeRotation = 135f * 0.0174f * -NPC.direction;
                    gunBladeFrame = 2;
                }
                if (NPC.ai[0] > 85)
                {
                    gunBladeRotation = ((135f * 0.0174f) - (NPC.ai[0] - 85) * 0.0174f * 9f) * -NPC.direction;
                    gunBladeFrame = 2;
                }
            }
            if (NPC.ai[3] >= 4)
            {
                gunBladeFrame = 2;
                if ((int)NPC.ai[3] % 2 == 0 && NPC.ai[2] <= 0)
                {
                    Vector2 gunArm = NPC.Center + new Vector2(gunBladeOffset.X * NPC.direction, gunBladeOffset.Y);
                    Vector2 predictedPos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        predictedPos = PredictiveAim(24f, arm, Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200);
                    }
                    float shootRot = (float)Math.Atan2(NPC.Center.Y - predictedPos.Y, NPC.Center.X - predictedPos.X) + (135f * 0.0174f);
                    if (NPC.direction == -1)
                    {
                        shootRot -= (90 * 0.0174f);
                    }
                    
                    if (NPC.ai[0] % 130 > 70)
                    {
                        gunBladeRotation = shootRot;
                    }
                    if (NPC.ai[0] % 130 > 100)
                    {
                        gunBladeRotation = (((NPC.ai[0] % 130) - 100f) * 9 * 0.0174f * -NPC.direction) + shootRot;
                    }
                    if (NPC.ai[0] % 130 > 110)
                    {
                        gunBladeRotation = (90 * 0.0174f * -NPC.direction);
                    }
                }
            }

            Rectangle gunBladeRect = new Rectangle(0, gunBladeFrame * (gunBladeTex.Height / totalGunBladeFrames), (gunBladeTex.Width), (gunBladeTex.Height / totalGunBladeFrames));
            Vector2 gunBladeVect = new Vector2((float)gunBladeTex.Width / 2, (float)gunBladeTex.Height / (2 * totalGunBladeFrames));

            SpriteEffects eyesEffects = SpriteEffects.None;
            dir = NPC.direction;
            if (dir == 1)
            {
                eyesEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                eyesEffects = SpriteEffects.None;
            }
            Texture2D eyesTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_Eyes").Value;
            int totaleyesFrames = 1;
            int eyesFrame = 0;
            float eyesRotation = 0;
            Vector2 eyesOffset = new Vector2(0, -11);

            Rectangle eyesRect = new Rectangle(0, eyesFrame * (eyesTex.Height / totaleyesFrames), (eyesTex.Width), (eyesTex.Height / totaleyesFrames));
            Vector2 eyesVect = new Vector2((float)eyesTex.Width / 2, (float)eyesTex.Height / (2 * totaleyesFrames));

            if (NPC.frame.Y == 196 || NPC.frame.Y == 196 * 4)
            {
                kunaiOffset.Y -= 8;
                excalipoorOffset.Y -= 8;
                masamuneOffset.Y -= 8;
                gunBladeOffset.Y -= 8;
                busterSwordOffset.Y -= 8;
                eyesOffset.Y -= 8;
            }
            if (NPC.frame.Y == 196 * 2 || NPC.frame.Y == 196 * 5)
            {
                kunaiOffset.Y -= 2;
                excalipoorOffset.Y -= 2;
                masamuneOffset.Y -= 2;
                gunBladeOffset.Y -= 2;
                busterSwordOffset.Y -= 2;
                eyesOffset.Y -= 2;
            }
            if (NPC.frame.Y == 0 || NPC.frame.Y == 196 * 3)
            {
                kunaiOffset.Y += 2;
                excalipoorOffset.Y += 2;
                masamuneOffset.Y += 2;
                gunBladeOffset.Y += 2;
                busterSwordOffset.Y += 2;
                eyesOffset.Y += 2;
            }

            if (NPC.ai[2] >= 1)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, (TextureAssets.Npc[NPC.type].Value.Height * 0.5f) / Main.npcFrameCount[NPC.type]);
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, 0);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * (NPC.frame.Y / 148), TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]));
                    
                    Vector2 kunaiOffset2 = kunaiOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 excalipoorOffset2 = excalipoorOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 masamuneOffset2 = masamuneOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 gunBladeOffset2 = gunBladeOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 busterSwordOffset2 = busterSwordOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);
                    Vector2 eyesOffset2 = eyesOffset.RotatedBy(NPC.oldRot[i] * NPC.direction, Vector2.Zero);

                    float kunaiRotation2 = kunaiRotation + NPC.oldRot[i];
                    float excalipoorRotation2 = excalipoorRotation + NPC.oldRot[i];
                    float masamuneRotation2 = masamuneRotation + NPC.oldRot[i];
                    float gunBladeRotation2 = gunBladeRotation + NPC.oldRot[i];
                    float busterSwordRotation2 = busterSwordRotation + NPC.oldRot[i];
                    float eyesRotation2 = eyesRotation + NPC.oldRot[i];

                    if (NPC.ai[3] < 1.5f)
                    {
                        spriteBatch.Draw(excalipoorTex, drawPos + new Vector2(NPC.scale * NPC.direction * excalipoorOffset2.X, NPC.scale * excalipoorOffset2.Y), new Rectangle?(excalipoorRect), color2, excalipoorRotation2, excalipoorVect, NPC.scale, excalipoorEffects, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(busterSwordTex, drawPos + new Vector2(NPC.scale * NPC.direction * busterSwordOffset2.X, NPC.scale * busterSwordOffset2.Y), new Rectangle?(busterSwordRect), color2, busterSwordRotation2, busterSwordVect, NPC.scale, busterSwordEffects, 0f);
                        spriteBatch.Draw(masamuneTex, drawPos + new Vector2(NPC.scale * NPC.direction * masamuneOffset2.X, NPC.scale * masamuneOffset2.Y), new Rectangle?(masamuneRect), color2, masamuneRotation2, masamuneVect, NPC.scale, masamuneEffects, 0f);
                    }
                    spriteBatch.Draw(gunBladeTex, drawPos + new Vector2(NPC.scale * NPC.direction * gunBladeOffset2.X, NPC.scale * gunBladeOffset2.Y), new Rectangle?(gunBladeRect), color2, gunBladeRotation2, gunBladeVect, NPC.scale, gunBladeEffects, 0f);
                    spriteBatch.Draw(kunaiTex, drawPos + new Vector2(NPC.scale * NPC.direction * kunaiOffset2.X, NPC.scale * kunaiOffset2.Y), new Rectangle?(kunaiRect), color2, kunaiRotation2, kunaiVect, NPC.scale, kunaiEffects, 0f);
                    //spriteBatch.Draw(eyesTex, drawPos + new Vector2(npc.scale * npc.direction * eyesOffset2.X, npc.scale * eyesOffset2.Y), new Rectangle?(eyesRect), Color.White, eyesRotation2, eyesVect, npc.scale, eyesEffects, 0f);
                }
                Vector2 eyeGlowOffset = eyesOffset + new Vector2(8 * NPC.direction, -41.5f);

                eyesOffset = eyesOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
                eyeGlowOffset = eyeGlowOffset.RotatedBy(NPC.rotation, Vector2.Zero);
                eyesRotation += NPC.rotation;
                spriteBatch.Draw(eyesTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * eyesOffset.X, NPC.scale * eyesOffset.Y), new Rectangle?(eyesRect), Color.White, eyesRotation, eyesVect, NPC.scale, eyesEffects, 0f);

                Vector2 vector = new Vector2((float)(3 * NPC.direction - ((NPC.direction == 1) ? 1 : 0)), -11.5f) + Vector2.UnitY * NPC.gfxOffY + NPC.Size / 2f;
                Vector2 vector2 = new Vector2((float)(3 * NPC.direction - ((NPC.direction == 1) ? 1 : 0)), -11.5f) + NPC.Size / 2f;
                Vector2 vector3 = Vector2.Zero;
                float num3 = 0f;
                Vector2 vector4 = NPC.position + eyeGlowOffset + vector + vector3;
                Vector2 vector5 = NPC.oldPosition + eyeGlowOffset + vector2 + vector3;
                vector5.Y -= num3 / 2f;
                vector4.Y -= num3 / 2f;
                float num4 = 1f;

                int num5 = (int)Vector2.Distance(vector4, vector5) / 3 + 1;
                if (Vector2.Distance(vector4, vector5) % 3f != 0f)
                {
                    num5++;
                }
                
                for (float num6 = 1f; num6 <= (float)num5; num6 += 1f)
                {
                    Dust expr_3D9 = Main.dust[Dust.NewDust(NPC.Center + eyeGlowOffset, 0, 0, 182, 0f, 0f, 0, default(Color), 1f)];
                    expr_3D9.position = Vector2.Lerp(vector5, vector4, num6 / (float)num5);
                    expr_3D9.noGravity = true;
                    expr_3D9.velocity = Vector2.Zero;
                    expr_3D9.customData = NPC;
                    expr_3D9.scale = num4;
                    expr_3D9.color = Color.Red;
                }
                
            }

            kunaiOffset = kunaiOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            excalipoorOffset = excalipoorOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            masamuneOffset = masamuneOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            gunBladeOffset = gunBladeOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);
            busterSwordOffset = busterSwordOffset.RotatedBy(NPC.rotation * NPC.direction, Vector2.Zero);

            kunaiRotation += NPC.rotation;
            excalipoorRotation += NPC.rotation;
            masamuneRotation += NPC.rotation;
            gunBladeRotation += NPC.rotation;
            busterSwordRotation += NPC.rotation;

            if (NPC.ai[3] < 1.5f)
            {
                spriteBatch.Draw(excalipoorTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * excalipoorOffset.X, NPC.scale * excalipoorOffset.Y), new Rectangle?(excalipoorRect), color, excalipoorRotation, excalipoorVect, NPC.scale, excalipoorEffects, 0f);
            }
            else
            {
                spriteBatch.Draw(busterSwordTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * busterSwordOffset.X, NPC.scale * busterSwordOffset.Y), new Rectangle?(busterSwordRect), color, busterSwordRotation, busterSwordVect, NPC.scale, busterSwordEffects, 0f);
                spriteBatch.Draw(masamuneTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * masamuneOffset.X, NPC.scale * masamuneOffset.Y), new Rectangle?(masamuneRect), color, masamuneRotation, masamuneVect, NPC.scale, masamuneEffects, 0f);
            }
            spriteBatch.Draw(gunBladeTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * gunBladeOffset.X, NPC.scale * gunBladeOffset.Y), new Rectangle?(gunBladeRect), color, gunBladeRotation, gunBladeVect, NPC.scale, gunBladeEffects, 0f);
            spriteBatch.Draw(kunaiTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * kunaiOffset.X, NPC.scale * kunaiOffset.Y), new Rectangle?(kunaiRect), color, kunaiRotation, kunaiVect, NPC.scale, kunaiEffects, 0f);
        }
        public override bool CheckDead()
        {
            if (NPC.ai[3] != 100)
            {
                SoundEngine.PlaySound(new SoundStyle("Sounds/Custom/Gilgamesh"), NPC.Center);
                NPC.ai[0] = 120;
                NPC.frame.Y = 0;
                NPC.velocity.X = NPC.direction * -32;
                NPC.velocity.Y = -8;
                NPC.damage = 0;
                NPC.ai[3] = 100;
                NPC.life = 1;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                NPC.NPCLoot();
                return false;
            }
            else
            {
                if (Main.netMode != NetmodeID.Server && NPC.AnyNPCs(ModContent.NPCType<Enkidu>()))
                {
                    //Main.NewText("<Gilgamesh> Ack! Uh, up to you now Enkidu!", 225, 25, 25);
                    Main.NewText("<Enkidu> Now you've gone and made me angry.", 25, 225, 25);
                }/*
                else
                {
                    Main.NewText("<Gilgamesh> Ack! How could we have lost!?", 225, 25, 25);
                }*/
                for (int i = 0; i < 80; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0, 0, 0, Color.OrangeRed, 2);
                }
                NPC.active = false;
            }
            return false;
        }
        public override bool PreAI()
        {
            if (NPC.ai[3] == 100)
            {
                NPC.dontTakeDamage = true;
                NPC.ai[0]--;
                NPC.velocity.X = NPC.direction * -32;
                NPC.velocity.Y = -8;
                NPC.rotation = 12 * 0.0174f * NPC.ai[0] * NPC.direction;
                if (NPC.ai[0] < 0)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
                return false;
            }
            return base.PreAI();
        }
    }
}

