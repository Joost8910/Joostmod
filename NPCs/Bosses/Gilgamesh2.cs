using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            Main.npcFrameCount[npc.type] = 10;
            NPCID.Sets.TrailingMode[npc.type] = 3;
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 90;
            npc.height = 166;
            npc.damage = 140;
            npc.defense = 75;
            npc.lifeMax = 400000;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = Item.buyPrice(30, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.buffImmune[BuffID.Confused] = true;
            bossBag = mod.ItemType("GilgBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ClashOnTheBigBridge");
            npc.frameCounter = 0;
            musicPriority = MusicPriority.BossHigh;
            npc.noGravity = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void NPCLoot()
        {
            for (int i = 0; i < 15; i++)
            {
                Item.NewItem(npc.getRect(), ItemID.Heart);
            }
            if (!NPC.AnyNPCs(mod.NPCType("Enkidu")))
            {
                if (!JoostWorld.downedGilgamesh)
                    Main.NewText("With Gilgamesh and Enkidu's defeat, you can now fish the legendary stones from their respective biomes", 125, 25, 225);
                JoostWorld.downedGilgamesh = true;
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
                else
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GenjiToken"), 1 + Main.rand.Next(2));
                    if (Main.rand.Next(2) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("COTBBMusicBox"));
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GilgameshMask"));
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GilgameshTrophy"));
                    }
                }
            }
        }


        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.velocity.Y == 0)
            {
                if (npc.velocity.X == 0)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter > 8)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + 196);
                    }
                    if (npc.frame.Y >= 196 * 10 || npc.frame.Y < 196 * 6)
                    {
                        npc.frame.Y = 196 * 6;
                    }
                }
                else
                {
                    npc.frameCounter += Math.Abs(npc.velocity.X);
                    if (npc.frameCounter > 46)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + 196);
                    }
                    if (npc.frame.Y >= 196 * 6)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (npc.velocity.Y < 0)
                {
                    npc.frame.Y = 196 * 2;
                }
                else
                {
                    npc.frame.Y = 196 * 3;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if ((int)npc.ai[3] % 2 == 1)
            {
                damage = damage / 3;
                crit = false;
            }
            if (npc.ai[2] > 0)
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
            if ((int)npc.ai[3] % 2 == 1)
            {
                damage = damage / 3;
                crit = false;
            }
            if (npc.ai[2] > 0)
            {
                damage = (int)(damage * 0.75f);
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (npc.ai[2] == 3 && npc.ai[0] >= 60 && npc.ai[2] < 76)
            {
                damage = (int)(damage * 2f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.ai[2] == 3 && npc.ai[0] >= 60 && npc.ai[2] < 76)
            {
                damage = (int)(damage * 2f);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (npc.ai[2] == 3 && npc.localAI[3] < 14 && npc.ai[3] >= 6)
            {
                npc.localAI[3] = 14;
                target.immuneTime = 8;
                if (Main.expertMode)
                {
                    target.velocity = Vector2.Zero;
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if ((int)npc.ai[3] % 2 == 1)
            {
                return false;
            }
            if (npc.ai[2] == 3 && npc.localAI[3] > 0 && npc.ai[3] >= 6)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)npc.localAI[0]);
            writer.Write((int)npc.localAI[1]);
            writer.Write((int)npc.localAI[2]);
            writer.Write((int)npc.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt32();
            npc.localAI[1] = reader.ReadInt32();
            npc.localAI[2] = reader.ReadInt32();
            npc.localAI[3] = reader.ReadInt32();
        }

        public override void AI()
        {
            npc.netUpdate = true;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -100f);
                    npc.active = false;
                }
            }
            if (npc.velocity.X > 0)
            {
                npc.direction = 1;
            }
            if (npc.velocity.X < 0)
            {
                npc.direction = -1;
            }
            if (npc.velocity.X == 0)
            {
                npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
            }
            float moveSpeed = 0;
            if (Math.Abs(P.Center.X - npc.Center.X) > 80)
            {
                moveSpeed = npc.Distance(P.Center) / 40;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == mod.NPCType("Enkidu") && Main.npc[i].ai[1] >= 900)
                {
                    moveSpeed *= 0.7f;
                    break;
                }
            }
            if (npc.velocity.Y < 0 && npc.position.Y > P.position.Y)
            {
                npc.noTileCollide = true;
            }
            else
            {
                npc.noTileCollide = false;
            }
            if (npc.ai[3] >= 1 && npc.ai[3] < 2)
            {
                if (npc.ai[0] < 300 && npc.ai[0] >= 190)
                {
                    if (npc.ai[0] == 210)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                        if (npc.Center.Y < P.position.Y)
                        {
                            npc.velocity.Y = (Math.Abs((P.position.Y) - (npc.position.Y + npc.height)) / 30) - (0.4f * 30 / 2);
                        }
                        else
                        {
                            npc.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs((P.position.Y) - (npc.position.Y + npc.height)));
                        }
                        float vel = moveSpeed + Math.Abs(P.velocity.X);
                        npc.velocity.X = npc.Center.X < P.Center.X ? vel : -vel;
                    }
                    if (npc.ai[0] > 250 || npc.ai[0] < 210)
                    {
                        if (P.Center.X > npc.Center.X + 10)
                        {
                            npc.velocity.X = moveSpeed;
                        }
                        if (P.Center.X < npc.Center.X - 10)
                        {
                            npc.velocity.X = -moveSpeed;
                        }
                    }
                    if (npc.velocity.Y == 0 && npc.ai[0] > 250)
                    {
                        npc.ai[0] = 300;
                    }
                }
                else
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = 15;
                    npc.noTileCollide = false;
                }
                npc.ai[0]++;
                npc.localAI[0] = 0;
                npc.localAI[1] = 0;
                if (npc.ai[0] == 0)
                {
                    Main.NewText("<Gilgamesh> Now that it's mine,", 225, 25, 25);
                }
                if (npc.ai[0] == 80)
                {
                    Main.NewText("let's see how good this Excalibur really is!", 225, 25, 25);
                }
                if (npc.ai[0] == 190)
                {
                    Main.NewText("Have at you!", 225, 25, 25);
                    npc.velocity.Y = -20;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                }
                if (npc.ai[0] == 235)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    float Speed = 24f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(-26 * npc.direction, -46);
                    Vector2 pos = PredictiveAim(Speed, arm, false);
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = mod.ProjectileType("GilgExcalipoorBeam");
                    int damage = 1;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] == 400)
                {
                    Main.NewText("Ehhh!? Why, I've been had!", 225, 25, 25);
                }
                if (npc.ai[0] == 500)
                {
                    Main.NewText("This is far from the strongest of swords!", 225, 25, 25);
                }
                if (npc.ai[0] == 600)
                {
                    Main.NewText("Nyeh!", 225, 25, 25);
                    float Speed = 12f;
                    Vector2 arm = npc.Center + new Vector2(-26 * npc.direction, -46);
                    Vector2 pos = P.MountedCenter;
                    Vector2 dir = new Vector2(npc.direction * Speed, -1.5f);
                    int type = mod.ProjectileType("GilgExcalipoor");
                    int damage = 1;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm, dir, type, damage, 0f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] > 630)
                {
                    npc.ai[3] = 1.5f;
                }
                if (npc.ai[0] > 700)
                {
                    npc.ai[3] = 2;
                    npc.ai[0] = 300;
                }
            }
            else if (npc.ai[3] == 3)
            {
                npc.ai[0]++;
                npc.velocity.X = 0;
                npc.noTileCollide = false;
                if (npc.ai[0] > 100)
                {
                    npc.ai[3] = 4;
                    npc.ai[0] = 400;
                }
            }
            else if (npc.ai[3] == 5)
            {
                npc.ai[0]++;
                npc.velocity.X = 0;
                npc.noTileCollide = false;
                if (npc.ai[0] > 95)
                {
                    npc.ai[3] = 6;
                    npc.ai[0] = 400;
                }
            }
            else if (npc.ai[3] == 7)
            {
                npc.ai[0]++;
                npc.velocity.X = 0;
                npc.noTileCollide = false;
                if (npc.ai[0] > 95)
                {
                    npc.ai[3] = 8;
                    npc.ai[0] = 400;
                }
            }
            else if (npc.ai[2] <= 0)
            {
                if (npc.life < npc.lifeMax * 0.9f && npc.ai[3] < 1 && npc.velocity.Y == 0)
                {
                    npc.ai[3] = 1;
                    npc.ai[0] = -30;
                    npc.localAI[0] = 0;
                    npc.localAI[1] = 0;
                }
                if (npc.life < npc.lifeMax * 0.7f && npc.ai[3] == 2 && npc.velocity.Y == 0)
                {
                    npc.ai[3] = 3;
                    npc.ai[0] = 0;
                    npc.localAI[0] = 0;
                    npc.localAI[1] = 0;
                }
                if (npc.life < npc.lifeMax * 0.5f && npc.ai[3] == 4 && npc.velocity.Y == 0)
                {
                    npc.ai[3] = 5;
                    npc.ai[0] = 0;
                    npc.localAI[0] = 0;
                    npc.localAI[1] = 0;
                }
                if (npc.life < npc.lifeMax * 0.3f && npc.ai[3] == 6 && npc.velocity.Y == 0)
                {
                    npc.ai[3] = 7;
                    npc.ai[0] = 0;
                    npc.localAI[0] = 0;
                    npc.localAI[1] = 0;
                }
                npc.ai[0]++;
                npc.localAI[0]++;
                npc.localAI[1]++;

                if (npc.Center.Y > P.Center.Y + 100 && npc.velocity.Y == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                }
                if (npc.position.Y + npc.height + npc.velocity.Y < P.position.Y && npc.velocity.Y >= 0)
                {
                    npc.position.Y++;
                }
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && moveSpeed > 3)
                {
                    npc.velocity.Y = -10f;
                    npc.noTileCollide = true;
                }
                if (npc.velocity.Y >= 0)
                {
                    npc.noTileCollide = false;
                }
                if (P.Center.X > npc.Center.X + 10)
                {
                    npc.velocity.X = moveSpeed;
                }
                if (P.Center.X < npc.Center.X - 10)
                {
                    npc.velocity.X = -moveSpeed;
                }

                if (npc.ai[3] >= 8)
                {
                    Vector2 shieldPos = npc.Center + new Vector2(29, -45);
                    Vector2 dir = P.MountedCenter - shieldPos;
                    npc.ai[1] = dir.ToRotation();
                    if (npc.direction < 0)
                    {
                        npc.ai[1] += 180f * 0.0174f;
                    }
                    bool shield = false;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (Main.npc[i].type == mod.NPCType("GilgameshShield") && (int)Main.npc[i].ai[0] == npc.whoAmI)
                        {
                            shield = true;
                            break;
                        }
                    }
                    if (!shield)
                    {
                        NPC.NewNPC((int)shieldPos.X, (int)shieldPos.Y, mod.NPCType("GilgameshShield"), 0, npc.whoAmI);
                    }
                }

                if (npc.localAI[0] > 90 && npc.Distance(P.MountedCenter) < 300)
                {
                    float Speed = 18f;
                    Vector2 arm = npc.Center + new Vector2(35 * npc.direction, -28);
                    Vector2 pos = P.MountedCenter;
                    float rotation = (float)Math.Atan2(npc.Center.Y - pos.Y, npc.Center.X - pos.X);
                    int type = mod.ProjectileType("GilgNaginata");
                    int damage = 50;
                    bool flag = true;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (projectile.type == mod.ProjectileType("GilgNaginata") && projectile.active)
                        {
                            npc.localAI[0] = 70;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 7f, Main.myPlayer, npc.whoAmI, 4f);
                        }
                    }
                    npc.localAI[0] = 0;
                }
                if (npc.localAI[0] % 90 == 0 && npc.Distance(P.MountedCenter) < 400)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 18);
                    float Speed = 18f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.MountedCenter.Y, npc.Center.X - P.MountedCenter.X);
                    int type = mod.ProjectileType("GilgFlail");
                    int damage = 45;
                    bool flag = true;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (Main.projectile[i].type == mod.ProjectileType("GilgFlail") && projectile.active)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (Main.netMode != 1 && flag)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 15f, Main.myPlayer, npc.whoAmI, npc.direction);
                    }
                }
                if (npc.localAI[1] > 40)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (Main.projectile[i].type == mod.ProjectileType("GilgAxe") && projectile.active)
                        {
                            npc.localAI[1] = 40;
                        }
                    }
                }
                if (npc.localAI[1] > 90)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    float Speed = 18f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(39 * npc.direction, -38);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm, Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = mod.ProjectileType("GilgAxe");
                    int damage = 45;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10f, Main.myPlayer, npc.whoAmI);
                    }
                    npc.localAI[1] = 0;
                }
                if (npc.ai[0] % 90 == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 19);
                    float Speed = 12f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(-27 * npc.direction, -37);
                    Vector2 pos = P.MountedCenter;
                    if (Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm, true);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = mod.ProjectileType("GilgKunai");
                    int damage = 35;
                    if (Main.netMode != 1)
                    {
                        float spread = 45f * 0.0174f;
                        float baseSpeed = (float)Math.Sqrt((float)((Math.Cos(rotation) * Speed) * -1) * (float)((Math.Cos(rotation) * Speed) * -1) + (float)((Math.Sin(rotation) * Speed) * -1) * (float)((Math.Sin(rotation) * Speed) * -1));
                        double startAngle = Math.Atan2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)) - spread / 3;
                        double deltaAngle = spread / 3;
                        double offsetAngle;
                        for (int i = 0; i < 3; i++)
                        {
                            offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(arm.X, arm.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, 1f, Main.myPlayer);
                        }
                    }
                }
                if (npc.ai[0] % 130 == 100 && npc.ai[3] >= 4)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 41);
                    float Speed = 12f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(-17 * npc.direction, -43);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed * 3, arm, false);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = mod.ProjectileType("GilgBullet");
                    int damage = 40;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 1f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] % 250 == 168 && npc.ai[3] >= 6)
                {
                    Main.PlaySound(42, npc.Center, 220);
                    float Speed = 12f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(-28 * npc.direction, -47);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm, true);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = mod.ProjectileType("GilgBusterBeam");
                    int damage = 50;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 1f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] == 550)
                {
                    Main.PlaySound(25, npc.Center, 1);
                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, 15);
                    }
                }
                if (npc.ai[0] > 600)
                {
                    int r = 2;
                    if (npc.ai[3] >= 2)
                    {
                        r++;
                    }
                    if (npc.ai[3] >= 4)
                    {
                        r++;
                    }
                    if (npc.ai[3] >= 8)
                    {
                        r++;
                    }
                    int rand = Main.rand.Next(r) + 1;
                    if ((int)npc.localAI[2] >= Math.Pow(2, r) - 1)
                    {
                        npc.localAI[2] = 0;
                    }
                    var bit1 = (int)npc.localAI[2] & (1 << 1 - 1);
                    var bit2 = (int)npc.localAI[2] & (1 << 2 - 1);
                    var bit3 = (int)npc.localAI[2] & (1 << 3 - 1);
                    var bit4 = (int)npc.localAI[2] & (1 << 4 - 1);
                    var bit5 = (int)npc.localAI[2] & (1 << 5 - 1);
                    if (bit1 > 0 && bit2 > 0 && bit3 > 0 && bit4 > 0 && bit5 > 0)
                    {
                        npc.localAI[2] = 0;
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

                    npc.ai[2] = rand;
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.netUpdate = true;
                }
            }
            if (npc.ai[2] == 1)
            {
                npc.localAI[0] = 0;
                npc.localAI[1] = 0;
                if (npc.velocity.Y == 0 || npc.ai[0] > 0)
                {
                    npc.ai[0]++;
                }
                if (npc.ai[0] == 30)
                {
                    npc.noTileCollide = true;
                    npc.velocity.Y = -40;
                    npc.velocity.X = 20 * npc.direction;
                    int damage = 150;
                    int type = mod.ProjectileType("BitterEnd");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 28);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.position.Y - 100, 0f, 0f, type, damage, 15f, Main.myPlayer);
                    }
                }
                if (npc.ai[0] > 30 && npc.ai[0] <= 80)
                {
                    npc.noTileCollide = true;
                    npc.velocity.Y = -0.399f;
                    npc.rotation = 30 * 0.0174f * npc.direction;
                    if (npc.ai[0] > 60)
                    {
                        npc.rotation = (30 - (npc.ai[0] - 60) * 2.5f) * 0.0174f * npc.direction;
                    }
                }
                if (npc.ai[0] == 175 && npc.ai[3] >= 6)
                {
                    Main.PlaySound(42, npc.Center, 220);
                    float Speed = 12f;
                    Vector2 arm = npc.Center + new Vector2(-28 * npc.direction, -47);
                    Vector2 pos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed * 2, arm, true);
                    }
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    int type = mod.ProjectileType("GilgBusterLimitBeam");
                    int damage = 50;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 1f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] > 80)
                {
                    npc.noTileCollide = false;
                    if (npc.velocity.Y == 0)
                    {
                        if (Math.Abs(npc.velocity.X) < 0.05f)
                        {
                            npc.velocity.X = 0;
                            npc.rotation = 0;
                        }
                        else
                        {
                            npc.velocity.X *= 0.9f;
                            npc.rotation = npc.velocity.X * 0.0174f * -npc.direction;
                        }
                    }
                }
                if (npc.ai[0] > 300)
                {
                    npc.ai[2] = 0;
                    npc.ai[0] = 0;
                    npc.rotation = 0;
                    npc.localAI[2] += 1;
                }
            }
            if (npc.ai[2] == 2)
            {
                if (npc.ai[0] != 120)
                {
                    npc.ai[0]++;
                }
                npc.localAI[0] = 0;
                npc.localAI[1] = 0;
                if (npc.ai[0] < 15)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y++;
                }
                if (npc.ai[0] == 15)
                {
                    npc.velocity.Y = -31;
                    Main.PlaySound(2, npc.Center, 7);
                    if (npc.ai[3] >= 6)
                    {
                        Main.PlaySound(42, npc.Center, 222);
                    }
                }
                if (npc.ai[0] <= 120)
                {
                    if (P.Center.X > npc.Center.X + 10)
                    {
                        npc.velocity.X = moveSpeed;
                    }
                    if (P.Center.X < npc.Center.X - 10)
                    {
                        npc.velocity.X = -moveSpeed;
                    }
                    if (npc.ai[3] >= 6)
                    {
                        if (npc.ai[0] > 40 && npc.ai[0] < 110)
                        {
                            npc.velocity.Y = 20;
                            npc.ai[0] = 110;
                            Main.PlaySound(42, npc.Center, 221);
                        }
                        else if (npc.ai[0] > 15)
                        {
                            if (npc.ai[0] < 30)
                            {
                                npc.velocity.Y = -31;
                            }
                            else if (npc.ai[0] < 40)
                            {
                                npc.velocity.Y += 1.5f;
                            }
                        }
                    }
                    else if (npc.velocity.Y > 0 && npc.ai[0] > 30)
                    {
                        npc.velocity.Y = 20;
                        npc.ai[0] = 120;
                    }
                }
                if (npc.ai[0] == 120 && npc.velocity.Y == 0)
                {
                    npc.ai[0]++;
                    npc.velocity.X = 0;
                    int damage = 150;
                    int type = mod.ProjectileType("UltimateIllusion");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 66);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 22f, 0f, type, damage, 15f, Main.myPlayer);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -22f, 0f, type, damage, 15f, Main.myPlayer);
                    }
                }
                if (npc.ai[0] > 300)
                {
                    npc.ai[2] = 0;
                    npc.ai[0] = 0;
                    npc.localAI[2] += 2;
                }
            }
            if (npc.ai[2] == 3)
            {
                npc.ai[0]++;
                npc.localAI[0] = 0;
                npc.localAI[1] = 0;
                if (npc.ai[0] == 10)
                {
                    npc.velocity.Y = 0;
                }
                if (npc.ai[0] < 60)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y -= 0.401f;
                }
                if (npc.ai[0] == 60)
                {
                    float speed = npc.Distance(P.MountedCenter) / 15;
                    npc.velocity = npc.DirectionTo(P.MountedCenter) * speed;
                    Main.PlaySound(42, npc.Center, 216);
                    npc.noTileCollide = true;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 18f * npc.direction, 0, mod.ProjectileType("GilgMasamuneSlash"), 100, 7f, Main.myPlayer, npc.whoAmI, -1);
                    }
                }
                if (npc.Distance(P.MountedCenter) > 2000 && npc.ai[0] > 80)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = npc.velocity.Y < 0 ? 1 : npc.velocity.Y;
                    npc.noTileCollide = false;
                }
                if (npc.Distance(P.MountedCenter) < 50 && npc.localAI[3] < 14 && npc.ai[0] > 60)
                {
                    npc.localAI[3] = 14;
                }
                if (npc.ai[3] >= 6)
                {
                    if (npc.localAI[3] > 0)
                    {
                        npc.localAI[3]++;
                    }
                    if (npc.ai[0] > 60 && npc.localAI[3] < 40 && npc.localAI[3] > 0)
                    {
                        if (npc.localAI[3] == 24)
                        {
                            int type = mod.ProjectileType("GilgBusterFinishingTouch");
                            int damage = 75;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(npc.Center.X + 20, npc.Center.Y, 1f, 0f, type, damage, 0f, Main.myPlayer, 10);
                                Projectile.NewProjectile(npc.Center.X - 20, npc.Center.Y, -1f, 0f, type, damage, 0f, Main.myPlayer, 8);
                            }
                        }
                        npc.velocity.X = 0;
                        npc.velocity.Y = 0.4f;
                        if (npc.localAI[3] >= 24 && npc.localAI[3] < 32)
                        {
                            npc.direction = 1;
                        }
                        if (npc.localAI[3] >= 32 && npc.localAI[3] < 40)
                        {
                            npc.direction = -1;
                        }
                    }
                }
                if (npc.ai[0] > 60 && npc.ai[0] < 80)
                {
                    npc.noTileCollide = true;
                    npc.velocity.Y -= 0.401f;
                }
                if (npc.ai[0] >= 90)
                {
                    npc.noTileCollide = false;
                    npc.velocity.X *= 0.96f;
                    if (npc.velocity.Y == 0)
                    {
                        npc.ai[0] = 200;
                    }
                }
                if (npc.ai[0] >= 200)
                {
                    npc.ai[2] = 0;
                    npc.localAI[3] = 0;
                    npc.ai[0] = 0;
                    npc.localAI[2] += 4;
                }
            }
            if (npc.ai[2] == 4)
            {
                npc.ai[0]++;
                npc.localAI[0] = 0;
                npc.localAI[1] = 0;

                if (Math.Abs(P.Center.X - npc.Center.X) > 150)
                {
                    moveSpeed = (npc.Distance(P.Center) - 150) / 60;
                }
                else
                {
                    moveSpeed = 0;
                }
                
                if (npc.Center.Y > P.Center.Y + 100 && npc.velocity.Y == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                }
                if (npc.position.Y + npc.height + npc.velocity.Y < P.position.Y && npc.velocity.Y >= 0)
                {
                    npc.position.Y++;
                }
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && moveSpeed > 3)
                {
                    npc.velocity.Y = -10f;
                    npc.noTileCollide = true;
                }
                if (npc.velocity.Y >= 0)
                {
                    npc.noTileCollide = false;
                }
                if (P.Center.X > npc.Center.X + 90)
                {
                    npc.velocity.X = moveSpeed;
                }
                if (P.Center.X < npc.Center.X - 90)
                {
                    npc.velocity.X = -moveSpeed;
                }

                if (npc.ai[0] == 30)
                {
                    Main.PlaySound(2, npc.Center, 8);
                    int damage = 15;
                    int type = mod.ProjectileType("GilgPortal");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 66);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, type, damage, 1f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] > 330)
                {
                    npc.ai[2] = 0;
                    npc.ai[0] = 0;
                    npc.localAI[2] += 8;
                }
            }
            if (npc.ai[2] == 5)
            {
                npc.ai[0]++;
                npc.localAI[0] = 0;
                npc.localAI[1] = 0;
                if (npc.ai[0] < 100)
                {
                    npc.velocity.X = 5 * npc.direction;
                }
                if (npc.ai[0] == 12)
                {
                    bool flag = true;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (projectile.type == mod.ProjectileType("GilgNaginata") && projectile.active)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Vector2 arm = npc.Center + new Vector2(35 * npc.direction, -28);
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(arm.X, arm.Y, 18f * npc.direction, 0, mod.ProjectileType("GilgNaginata"), 50, 7f, Main.myPlayer, npc.whoAmI, 4f);
                        }
                    }
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 18f * npc.direction, 0, mod.ProjectileType("GilgMasamuneSlash"), 100, 7f, Main.myPlayer, npc.whoAmI, 1);
                    }
                    Main.PlaySound(42, npc.Center, 214);
                }
                if (npc.ai[0] == 32)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 18f * npc.direction, 0, mod.ProjectileType("GilgMasamuneSlash"), 100, 7f, Main.myPlayer, npc.whoAmI, -1);
                    }
                    Main.PlaySound(42, npc.Center, 216);
                }
                if (npc.ai[0] > 12 && npc.ai[0] < 28)
                {
                    npc.velocity.X = 25 * npc.direction;
                }
                else if (npc.ai[0] > 32 && npc.ai[0] < 48)
                {
                    npc.velocity.X = 25 * npc.direction;
                }
                else
                {
                    npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
                }
                if (npc.ai[0] == 60)
                {
                    npc.velocity.Y = -21;
                }
                if (npc.ai[0] == 100)
                {
                    Main.PlaySound(2, npc.Center, 8);
                    int damage = 150;
                    int type = mod.ProjectileType("GilgWrath");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 66);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, type, damage, 1f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[0] > 100 && npc.ai[0] < 200)
                {
                    npc.velocity.X = (P.Center.X - npc.Center.X) / 50f;
                    npc.velocity.Y = -0.401f;
                    if (P.position.Y < npc.Center.Y + 300)
                    {
                        if (P.velocity.Y < -1f * (Math.Abs(P.position.Y - npc.position.Y - 400) / 30))
                        {
                            npc.velocity.Y = P.velocity.Y;
                        }
                        else
                        {
                            npc.velocity.Y = -1f * (Math.Abs(P.position.Y - npc.position.Y - 400) / 30);
                        }
                    }
                }
                if (npc.ai[0] == 200)
                {
                    npc.velocity = npc.DirectionTo(P.MountedCenter) * -7;
                }
                if (npc.ai[0] > 200 && npc.velocity.Y == 0)
                {
                    npc.ai[2] = 0;
                    npc.ai[0] = 0;
                    npc.localAI[2] += 16;
                }
            }
            if (npc.ai[2] >= 6)
            {
                npc.ai[2] = 0;
                npc.ai[0] = 0;
            }
            npc.noGravity = true;
            npc.velocity.Y += 0.4f;
            if (npc.velocity.Y > 15)
            {
                npc.velocity.Y = 15;
            }
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin, bool ignoreY)
        {
            Player P = Main.player[npc.target];
            Vector2 vel = (ignoreY ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Player P = Main.player[npc.target];
            SpriteEffects effects = SpriteEffects.None;
            if (npc.direction == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16));

            SpriteEffects ribbonEffects = SpriteEffects.None;
            int dir = npc.direction;
            if (npc.velocity == Vector2.Zero)
            {
                dir = -npc.direction;
            }
            if (dir == 1)
            {
                ribbonEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                ribbonEffects = SpriteEffects.None;
            }
            Texture2D ribbonTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_Ribbons");
            int totalRibbonFrames = 6;
            int ribbonFrame = 0;
            float ribbonRotation = 0;
            Vector2 ribbonOffset = new Vector2(0, 0);

            ribbonFrame = (int)(((int)(npc.ai[0] * 1.2f) % 60) / 10);

            Rectangle ribbonRect = new Rectangle(0, ribbonFrame * (ribbonTex.Height / totalRibbonFrames), (ribbonTex.Width), (ribbonTex.Height / totalRibbonFrames));
            Vector2 ribbonVect = new Vector2((float)ribbonTex.Width / 2, (float)ribbonTex.Height / (2 * totalRibbonFrames));


            Texture2D flailTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_FlailArm");
            int totalFlailFrames = 2;
            int flailFrame = 0;
            float flailRotation = 0;
            Vector2 flailOffset = new Vector2(40, -46);

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == mod.ProjectileType("GilgFlail") && projectile.active)
                {
                    flailFrame = 1;
                    Vector2 pos = npc.Center + new Vector2(flailOffset.X * npc.direction, flailOffset.Y);
                    flailRotation = (float)Math.Atan2(pos.Y - projectile.Center.Y, pos.X - projectile.Center.X) + ((npc.direction == 1 ? 145f : 35f) * 0.0174f);
                    break;
                }
            }

            Rectangle flailRect = new Rectangle(0, flailFrame * (flailTex.Height / totalFlailFrames), (flailTex.Width), (flailTex.Height / totalFlailFrames));
            Vector2 flailVect = new Vector2((float)flailTex.Width / 2, (float)flailTex.Height / (2 * totalFlailFrames));

            Texture2D naginataTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_NaginataArm");
            int totalNaginataFrames = 3;
            int naginataFrame = 0;
            float naginataRotation = 0;
            Vector2 naginataOffset = new Vector2(34, -44);

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == mod.ProjectileType("GilgNaginata") && projectile.active)
                {
                    Vector2 pos = npc.Center + new Vector2(naginataOffset.X * npc.direction, naginataOffset.Y);
                    naginataRotation = (float)Math.Atan2(pos.Y - projectile.Center.Y, pos.X - projectile.Center.X) + (npc.direction == 1 ? 3.14f : 0);
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

            Texture2D axeTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_AxeArm");
            int totalAxeFrames = 3;
            int axeFrame = 0;
            float axeRotation = 0;
            Vector2 axeOffset = new Vector2(39, -53);
            
            Vector2 arm = npc.Center + new Vector2(axeOffset.X * npc.direction, axeOffset.Y);
            Vector2 predictedPos = P.MountedCenter;
            if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
            {
                predictedPos = PredictiveAim(24f, arm, Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200);
            }
            float rotation = (float)Math.Atan2(npc.Center.Y - predictedPos.Y, npc.Center.X - predictedPos.X);
            if (npc.direction == 1)
            {
                rotation += 3.14f;
            }

            if (npc.localAI[1] > 45 && npc.localAI[1] <= 90)
            {
                axeRotation = ((npc.localAI[1] - 45f) * 3 * 0.0174f * -npc.direction) + rotation;
                if (npc.localAI[1] > 80)
                {
                    axeFrame = 1;
                    axeRotation = ((105 * 0.0174f * -npc.direction) + rotation) - ((npc.localAI[1] - 80f) * 10.5f * 0.0174f * -npc.direction);
                }
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == mod.ProjectileType("GilgAxe") && projectile.active)
                {
                    axeFrame = 2;
                    axeRotation = (float)Math.Atan2(arm.Y - projectile.Center.Y, arm.X - projectile.Center.X) + (npc.direction == 1 ? 3.14f : 0);
                    break;
                }
            }

            Rectangle axeRect = new Rectangle(0, axeFrame * (axeTex.Height / totalAxeFrames), (axeTex.Width), (axeTex.Height / totalAxeFrames));
            Vector2 axeVect = new Vector2((float)axeTex.Width / 2, (float)axeTex.Height / (2 * totalAxeFrames));

            Texture2D shieldTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_ShieldArm");
            int totalShieldFrames = 3;
            int shieldFrame = 0;
            float shieldRotation = 0;
            Vector2 shieldOffset = new Vector2(29, -45);

            if (npc.ai[3] == 7)
            {
                if (npc.ai[0] > 30)
                {
                    shieldRotation = ((135f * 0.014f) + (npc.ai[0] - 30) * 0.0174f * 6f) * -npc.direction;
                    shieldFrame = 1;
                }
                if (npc.ai[0] > 45)
                {
                    shieldRotation = ((225 * 0.0174f) - (npc.ai[0] - 45) * 0.0174f * 15f) * -npc.direction;
                    shieldFrame = 2;
                }
                if (npc.ai[0] > 51)
                {
                    shieldRotation = 135f * 0.0174f * -npc.direction;
                    shieldFrame = 2;
                }
                if (npc.ai[0] > 85)
                {
                    shieldRotation = ((135f * 0.0174f) - (npc.ai[0] - 85) * 0.0174f * 9f) * -npc.direction;
                    shieldFrame = 2;
                }
            }
            if(npc.ai[3] >= 8)
            {
                shieldRotation = npc.ai[1];
                shieldFrame = 2;
            }

            Rectangle shieldRect = new Rectangle(0, shieldFrame * (shieldTex.Height / totalShieldFrames), (shieldTex.Width), (shieldTex.Height / totalShieldFrames));
            Vector2 shieldVect = new Vector2((float)shieldTex.Width / 2, (float)shieldTex.Height / (2 * totalShieldFrames));


            if (npc.frame.Y == 196 || npc.frame.Y == 196 * 4)
            {
                ribbonOffset.Y -= 8;
                flailOffset.Y -= 8;
                naginataOffset.Y -= 8;
                axeOffset.Y -= 8;
                shieldOffset.Y -= 8;
            }
            if (npc.frame.Y == 196 * 2 || npc.frame.Y == 196 * 5)
            {
                ribbonOffset.Y -= 2;
                flailOffset.Y -= 2;
                naginataOffset.Y -= 2;
                axeOffset.Y -= 2;
                shieldOffset.Y -= 2;
            }
            if (npc.frame.Y == 0 || npc.frame.Y == 196 * 3)
            {
                ribbonOffset.Y += 2;
                flailOffset.Y += 2;
                naginataOffset.Y += 2;
                axeOffset.Y += 2;
                shieldOffset.Y += 2;
            }

            if (npc.ai[2] >= 1)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height * 0.5f) / Main.npcFrameCount[npc.type]);
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, 0);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 148), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type])); spriteBatch.Draw(ribbonTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * ribbonOffset.X, npc.scale * ribbonOffset.Y), new Rectangle?(ribbonRect), color, ribbonRotation, ribbonVect, npc.scale, ribbonEffects, 0f);

                    Vector2 ribbonOffset2 = ribbonOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 flailOffset2 = flailOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 naginataOffset2 = naginataOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 axeOffset2 = axeOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 shieldOffset2 = shieldOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);

                    float ribbonRotation2 = ribbonRotation + npc.oldRot[i];
                    float flailRotation2 = flailRotation + npc.oldRot[i];
                    float naginataRotation2 = naginataRotation + npc.oldRot[i];
                    float axeRotation2 = axeRotation + npc.oldRot[i];
                    float shieldRotation2 = shieldRotation + npc.oldRot[i];

                    spriteBatch.Draw(ribbonTex, drawPos + new Vector2(npc.scale * npc.direction * ribbonOffset2.X, npc.scale * ribbonOffset2.Y), new Rectangle?(ribbonRect), color2, ribbonRotation2, ribbonVect, npc.scale, ribbonEffects, 0f);
                    spriteBatch.Draw(flailTex, drawPos + new Vector2(npc.scale * npc.direction * flailOffset2.X, npc.scale * flailOffset2.Y), new Rectangle?(flailRect), color2, flailRotation2, flailVect, npc.scale, effects, 0f);
                    spriteBatch.Draw(naginataTex, drawPos + new Vector2(npc.scale * npc.direction * naginataOffset2.X, npc.scale * naginataOffset2.Y), new Rectangle?(naginataRect), color2, naginataRotation2, naginataVect, npc.scale, effects, 0f);
                    spriteBatch.Draw(axeTex, drawPos + new Vector2(npc.scale * npc.direction * axeOffset2.X, npc.scale * axeOffset2.Y), new Rectangle?(axeRect), color2, axeRotation2, axeVect, npc.scale, effects, 0f);
                    spriteBatch.Draw(shieldTex, drawPos + new Vector2(npc.scale * npc.direction * shieldOffset2.X, npc.scale * shieldOffset2.Y), new Rectangle?(shieldRect), color2, shieldRotation2, shieldVect, npc.scale, effects, 0f);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos + new Vector2(0f, -7), rect, color2, npc.oldRot[i], drawOrigin, npc.scale, effects, 0f);
                }
            }

            ribbonOffset = ribbonOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            flailOffset = flailOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            naginataOffset = naginataOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            axeOffset = axeOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            shieldOffset = shieldOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);

            ribbonRotation += npc.rotation;
            flailRotation += npc.rotation;
            naginataRotation += npc.rotation;
            axeRotation += npc.rotation;
            shieldRotation += npc.rotation;

            spriteBatch.Draw(ribbonTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * ribbonOffset.X, npc.scale * ribbonOffset.Y), new Rectangle?(ribbonRect), color, ribbonRotation, ribbonVect, npc.scale, ribbonEffects, 0f);
            spriteBatch.Draw(flailTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * flailOffset.X, npc.scale * flailOffset.Y), new Rectangle?(flailRect), color, flailRotation, flailVect, npc.scale, effects, 0f);
            spriteBatch.Draw(naginataTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * naginataOffset.X, npc.scale * naginataOffset.Y), new Rectangle?(naginataRect), color, naginataRotation, naginataVect, npc.scale, effects, 0f);
            spriteBatch.Draw(axeTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * axeOffset.X, npc.scale * axeOffset.Y), new Rectangle?(axeRect), color, axeRotation, axeVect, npc.scale, effects, 0f);
            spriteBatch.Draw(shieldTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * shieldOffset.X, npc.scale * shieldOffset.Y), new Rectangle?(shieldRect), color, shieldRotation, shieldVect, npc.scale, effects, 0f);

            return base.PreDraw(spriteBatch, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Player P = Main.player[npc.target];
            SpriteEffects kunaiEffects = SpriteEffects.None;
            int dir = npc.direction;
            float kun = npc.ai[0] % 90;
            if ((int)npc.ai[3] % 2 == 1 || npc.ai[2] > 0)
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
            Color color = Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16));

            Texture2D kunaiTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_KunaiArm");
            int totalKunaiFrames = 3;
            int kunaiFrame = 0;
            float kunaiRotation = 0;
            Vector2 kunaiOffset = new Vector2(-26, -52);

            Player player = Main.player[npc.target];
            Vector2 arm = npc.Center + new Vector2(kunaiOffset.X * npc.direction, kunaiOffset.Y);
            Vector2 pos = player.MountedCenter;
            float Speed = 12f + npc.velocity.Length();
            if (Math.Abs(npc.Center.Y - player.MountedCenter.Y) < 200)
            {
                pos = PredictiveAim(Speed, arm, Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200);
            }
            float rotation = (float)Math.Atan2(npc.Center.Y - pos.Y, npc.Center.X - pos.X);
            if (npc.direction == 1)
            { 
                rotation += 3.14f;
            }
            if (kun > 45 && kun <= 90)
            {
                kunaiRotation = ((kun - 45f) * 3 * 0.0174f * -npc.direction) + rotation;
                if (kun > 80)
                {
                    kunaiFrame = 1;
                    kunaiRotation = ((105 * 0.0174f * -npc.direction) + rotation) - ((kun - 80f) * 10.5f * 0.0174f * -npc.direction);
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
            dir = npc.direction;
            if (dir == 1)
            {
                excalipoorEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                excalipoorEffects = SpriteEffects.None;
            }
            Texture2D excalipoorTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_ExcalipoorArms");
            int totalExcalipoorFrames = 2;
            int excalipoorFrame = 0;
            float excalipoorRotation = 0;
            Vector2 excalipoorOffset = new Vector2(-26, -46);

            if (npc.ai[3] >= 1 && npc.ai[3] < 1.5f)
            {
                if (npc.ai[0] > 210)
                {
                    excalipoorRotation = (npc.ai[0] - 210) * 0.0174f * 8 * -npc.direction;
                }
                if (npc.ai[0] > 235)
                {
                    excalipoorRotation = ((25 * 0.0174f * 8) - (npc.ai[0] - 235) * 0.0174f * 13f) * -npc.direction;
                }
                if (npc.ai[0] > 250)
                {
                    excalipoorRotation = 0;
                }
                if (npc.ai[0] > 335)
                {
                    excalipoorRotation = (npc.ai[0] - 335) * 0.0174f * 6f * -npc.direction;
                }
                if (npc.ai[0] > 350)
                {
                    excalipoorRotation = 90 * 0.0174f * -npc.direction;
                }

                if (npc.ai[0] > 580)
                {
                    excalipoorRotation = ((90 * 0.0174f) + (npc.ai[0] - 580) * 0.0174f * 11) * -npc.direction;
                }
                if (npc.ai[0] > 590)
                {
                    excalipoorRotation = ((25 * 0.0174f * 10) - (npc.ai[0] - 590) * 0.0174f * 11.5f) * -npc.direction;
                }
                if (npc.ai[0] > 600)
                {
                    excalipoorRotation = 135f * 0.0174f * -npc.direction;
                    excalipoorFrame = 1;
                }
            }


            Rectangle excalipoorRect = new Rectangle(0, excalipoorFrame * (excalipoorTex.Height / totalExcalipoorFrames), (excalipoorTex.Width), (excalipoorTex.Height / totalExcalipoorFrames));
            Vector2 excalipoorVect = new Vector2((float)excalipoorTex.Width / 2, (float)excalipoorTex.Height / (2 * totalExcalipoorFrames));


            dir = npc.direction;
            SpriteEffects busterSwordEffects = SpriteEffects.None;

            Texture2D busterSwordTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_BusterSwordArm");
            int totalBusterSwordFrames = 5;
            int busterSwordFrame = 0;
            float busterSwordRotation = 0;
            Vector2 busterSwordOffset = new Vector2(-28, -47);

            if (npc.ai[3] == 5)
            {
                if (npc.ai[0] > 30)
                {
                   busterSwordRotation = ((135f * 0.014f) + (npc.ai[0] - 30) * 0.0174f * 6f) * -npc.direction;
                   busterSwordFrame = 1;
                }
                if (npc.ai[0] > 45)
                {
                   busterSwordRotation = ((225 * 0.0174f) - (npc.ai[0] - 45) * 0.0174f * 15f) * -npc.direction;
                   busterSwordFrame = 2;
                }
                if (npc.ai[0] > 51)
                {
                   busterSwordRotation = 135f * 0.0174f * -npc.direction;
                   busterSwordFrame = 2;
                }
                if (npc.ai[0] > 85)
                {
                   busterSwordRotation = ((135f * 0.0174f) - (npc.ai[0] - 85) * 0.0174f * 9f) * -npc.direction;
                   busterSwordFrame = 2;
                }
            }
            if (npc.ai[3] >= 6)
            {
                busterSwordFrame = 2;
                busterSwordRotation = 45f * 0.0174f * -npc.direction;
                if (npc.ai[2] <= 0)
                {
                    if (npc.ai[0] % 250 > 145 && npc.ai[0] % 250 < 200)
                    {
                        busterSwordFrame = 3;
                        if (npc.ai[0] % 250 <= 160)
                        {
                            busterSwordRotation = ((45f * 0.0174f) + (((npc.ai[0] % 250) - 145) * 0.0174f * -6f)) * -npc.direction;
                        }
                        if (npc.ai[0] % 250 > 160)
                        {
                            dir *= -1;
                            busterSwordRotation = (((npc.ai[0] % 250) - 160) * 0.0174f * 10f) * -npc.direction;
                        }
                        if (npc.ai[0] % 250 > 176)
                        {
                            busterSwordRotation = 160f * 0.0174f * -npc.direction;
                        }
                        if (npc.ai[0] % 250 > 186)
                        {
                            busterSwordFrame = 2;
                            dir *= -1;
                            busterSwordRotation = ((115f * 0.0174f) - (((npc.ai[0] % 250) - 186) * 0.0174f * 5f)) * -npc.direction;
                        }
                    }
                }
                if (npc.ai[2] > 0 || npc.ai[0] > 550)
                {
                    busterSwordFrame = 4;
                }
                if (npc.ai[2] == 1)
                {
                    int b = 150;
                    if (npc.ai[0] > b && npc.ai[0] % 250 < 200)
                    {
                        if (npc.ai[0] <= b + 15)
                        {
                            busterSwordRotation = ((45f * 0.0174f) + ((npc.ai[0] - b) * 0.0174f * -6f)) * -npc.direction;
                        }
                        if (npc.ai[0] > b + 15)
                        {
                            dir *= -1;
                            busterSwordRotation = ((npc.ai[0] - (b + 15)) * 0.0174f * 10f) * -npc.direction;
                        }
                        if (npc.ai[0] > b + 31)
                        {
                            busterSwordRotation = 160f * 0.0174f * -npc.direction;
                        }
                        if (npc.ai[0] > b + 41)
                        {
                            busterSwordFrame = 2;
                            dir *= -1;
                            busterSwordRotation = ((115f * 0.0174f) - ((npc.ai[0] - (b + 41)) * 0.0174f * 5f)) * -npc.direction;
                        }
                        if (npc.ai[0] > b + 55)
                        {
                            busterSwordRotation = 45f * 0.0174f * -npc.direction;
                        }
                    }
                }
                if (npc.ai[2] == 2)
                {
                    if (npc.ai[0] <= 15)
                    {
                        busterSwordRotation = ((45f * 0.0174f) + (npc.ai[0] * 0.0174f * -6f)) * -npc.direction;
                    }
                    if (npc.ai[0] > 15)
                    {
                        dir *= -1;
                        busterSwordRotation = ((npc.ai[0] - 15) * 0.0174f * 12f) * -npc.direction;
                    }
                    if (npc.ai[0] > 30)
                    {
                        busterSwordRotation = 180f * 0.0174f * -npc.direction;
                    }
                    if (npc.ai[0] >= 110)
                    {
                        dir *= -1;
                        busterSwordRotation = ((180 * 0.0174f) - ((npc.ai[0] - 110) * 0.0174f * 12f)) * -npc.direction;
                    }
                    if (npc.ai[0] >= 120)
                    {
                        busterSwordRotation = (30f * 0.0174f) * -npc.direction;
                    }
                }
                if (npc.ai[2] == 3)
                {
                    if (npc.localAI[3] >= 14 && npc.localAI[3] < 24)
                    {
                        busterSwordRotation = ((45f * 0.0174f) + ((npc.localAI[3] - 8) * 0.0174f * 9f)) * -npc.direction;
                    }
                    if (npc.localAI[3] >= 24)
                    {
                        busterSwordRotation = ((135f * 0.0174f) + ((npc.localAI[3] - 24) * 0.0174f * -22.5f)) * -npc.direction;
                    }
                    if (npc.localAI[3] >= 32)
                    {
                        dir *= -1;
                        busterSwordRotation = ((-45f * 0.0174f) + ((npc.localAI[3] - 32) * 0.0174f * 22.5f)) * -npc.direction;
                    }
                    if (npc.localAI[3] >= 40)
                    {
                        dir *= -1;
                        busterSwordFrame = 2;
                        busterSwordRotation = 135f * -npc.direction;
                    }
                    if (npc.localAI[3] >= 50)
                    {
                        busterSwordRotation = ((135f * 0.0174f) + ((npc.localAI[3] - 50) * 0.0174f * -6f)) * -npc.direction;
                    }
                    if (npc.localAI[3] >= 65)
                    {
                        busterSwordRotation = 45f * 0.0174f * -npc.direction;
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
            dir = npc.direction;
            if (dir == 1)
            {
                masamuneEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                masamuneEffects = SpriteEffects.None;
            }
            Texture2D masamuneTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_MasamuneArm");
            int totalMasamuneFrames = 5;
            int masamuneFrame = 0;
            float masamuneRotation = 0;
            Vector2 masamuneOffset = new Vector2(-24, -43);

            if (npc.ai[3] >= 1.5f && npc.ai[3] < 2)
            {
                if (npc.ai[0] > 630)
                {
                    masamuneRotation = ((135f * 0.014f) + (npc.ai[0] - 630) * 0.0174f * 6f) * -npc.direction;
                    masamuneFrame = 1;
                }
                if (npc.ai[0] > 645)
                {
                    masamuneRotation = ((225 * 0.0174f) - (npc.ai[0] - 645) * 0.0174f * 15f) * -npc.direction;
                    masamuneFrame = 2;
                }
                if (npc.ai[0] > 651)
                {
                    masamuneRotation = 135f * 0.0174f * -npc.direction;
                    masamuneFrame = 2;
                }
                if (npc.ai[0] > 685)
                {
                    masamuneRotation = ((135f * 0.0174f) - (npc.ai[0] - 685) * 0.0174f * 9f) * -npc.direction;
                    masamuneFrame = 2;
                }
            }
            if (npc.ai[3] >= 2)
            {
                masamuneFrame = 2;
            }
            if(npc.ai[2] == 3)
            {
                if (npc.ai[0] <= 15)
                {
                    masamuneRotation = (npc.ai[0] * 0.0174f * 6f) * npc.direction;
                }
                if (npc.ai[0] > 15)
                {
                    masamuneFrame = 3;
                }
                if (npc.ai[0] > 60)
                {
                    masamuneFrame = 3;
                    masamuneRotation = ((npc.ai[0] - 60) * 0.0174f * 13.125f) * -npc.direction;
                }
                if (npc.ai[0] > 68)
                {
                    masamuneFrame = 4;
                    masamuneRotation = (-135f * 0.0174f) + ((npc.ai[0] - 68) * 0.0174f * 13.125f) * -npc.direction;
                }
                if (npc.ai[0] > 76)
                {
                    masamuneFrame = 4;
                    masamuneRotation = (-33.75f * 0.0174f) * -npc.direction;
                }
            }
            if (npc.ai[2] == 5)
            {
                if (npc.ai[0] <= 12)
                {
                    masamuneRotation = (npc.ai[0] * 0.0174f * 7.5f) * -npc.direction;
                }
                if (npc.ai[0] > 12)
                {
                    masamuneRotation = ((90f * 0.0174f) - ((npc.ai[0] - 12) * 0.0174f * 11.25f)) * -npc.direction;
                }
                if (npc.ai[0] > 28)
                {
                    masamuneRotation = (-90 * 0.0174f) * -npc.direction;
                    masamuneFrame = 3;
                }
                if (npc.ai[0] > 32)
                {
                    masamuneFrame = 3;
                    masamuneRotation = (npc.ai[0] - 32) * 0.0174f * 13.125f * -npc.direction;
                }
                if (npc.ai[0] > 40)
                {
                    masamuneFrame = 4;
                    masamuneRotation = ((-135f * 0.0174f) + ((npc.ai[0] - 40) * 0.0174f * 13.125f)) * -npc.direction;
                }
                if (npc.ai[0] > 48)
                {
                    masamuneFrame = 4;
                    masamuneRotation = (-33.75f * 0.0174f) * -npc.direction;
                }
                if (npc.ai[0] > 60)
                {
                    masamuneFrame = 2;
                    masamuneRotation = 0;
                }
                if (npc.ai[0] > 80)
                {
                    masamuneRotation = ((npc.ai[0] - 80) * 0.0174f * 10f) * -npc.direction;
                }
                if (npc.ai[0] > 100)
                {
                    masamuneRotation = 200 * 0.0174f * -npc.direction;
                }
                if (npc.ai[0] > 200)
                {
                    masamuneRotation = ((200 * 0.0174f) - ((npc.ai[0] - 200) * 0.0174f * 10f)) * -npc.direction;
                }
                if (npc.ai[0] > 220)
                { 
                    masamuneRotation = 0;
                }

            }

            Rectangle masamuneRect = new Rectangle(0, masamuneFrame * (masamuneTex.Height / totalMasamuneFrames), (masamuneTex.Width), (masamuneTex.Height / totalMasamuneFrames));
            Vector2 masamuneVect = new Vector2((float)masamuneTex.Width / 2, (float)masamuneTex.Height / (2 * totalMasamuneFrames));


            SpriteEffects gunBladeEffects = SpriteEffects.None;
            dir = npc.direction;
            if (dir == 1)
            {
                gunBladeEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                gunBladeEffects = SpriteEffects.None;
            }
            Texture2D gunBladeTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_GunBladeArm");
            int totalGunBladeFrames = 3;
            int gunBladeFrame = 0;
            float gunBladeRotation = 0;
            Vector2 gunBladeOffset = new Vector2(-17, -43);

            if (npc.ai[3] == 3)
            {
                if (npc.ai[0] > 30)
                {
                    gunBladeRotation = ((135f * 0.014f) + (npc.ai[0] - 30) * 0.0174f * 6f) * -npc.direction;
                    gunBladeFrame = 1;
                }
                if (npc.ai[0] > 45)
                {
                    gunBladeRotation = ((225 * 0.0174f) - (npc.ai[0] - 45) * 0.0174f * 15f) * -npc.direction;
                    gunBladeFrame = 2;
                }
                if (npc.ai[0] > 51)
                {
                    gunBladeRotation = 135f * 0.0174f * -npc.direction;
                    gunBladeFrame = 2;
                }
                if (npc.ai[0] > 85)
                {
                    gunBladeRotation = ((135f * 0.0174f) - (npc.ai[0] - 85) * 0.0174f * 9f) * -npc.direction;
                    gunBladeFrame = 2;
                }
            }
            if (npc.ai[3] >= 4)
            {
                gunBladeFrame = 2;
                if ((int)npc.ai[3] % 2 == 0 && npc.ai[2] <= 0)
                {
                    Vector2 gunArm = npc.Center + new Vector2(gunBladeOffset.X * npc.direction, gunBladeOffset.Y);
                    Vector2 predictedPos = P.MountedCenter;
                    if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        predictedPos = PredictiveAim(24f, arm, Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200);
                    }
                    float shootRot = (float)Math.Atan2(npc.Center.Y - predictedPos.Y, npc.Center.X - predictedPos.X) + (135f * 0.0174f);
                    if (npc.direction == -1)
                    {
                        shootRot -= (90 * 0.0174f);
                    }
                    
                    if (npc.ai[0] % 130 > 70)
                    {
                        gunBladeRotation = shootRot;
                    }
                    if (npc.ai[0] % 130 > 100)
                    {
                        gunBladeRotation = (((npc.ai[0] % 130) - 100f) * 9 * 0.0174f * -npc.direction) + shootRot;
                    }
                    if (npc.ai[0] % 130 > 110)
                    {
                        gunBladeRotation = (90 * 0.0174f * -npc.direction);
                    }
                }
            }

            Rectangle gunBladeRect = new Rectangle(0, gunBladeFrame * (gunBladeTex.Height / totalGunBladeFrames), (gunBladeTex.Width), (gunBladeTex.Height / totalGunBladeFrames));
            Vector2 gunBladeVect = new Vector2((float)gunBladeTex.Width / 2, (float)gunBladeTex.Height / (2 * totalGunBladeFrames));

            SpriteEffects eyesEffects = SpriteEffects.None;
            dir = npc.direction;
            if (dir == 1)
            {
                eyesEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                eyesEffects = SpriteEffects.None;
            }
            Texture2D eyesTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_Eyes");
            int totaleyesFrames = 1;
            int eyesFrame = 0;
            float eyesRotation = 0;
            Vector2 eyesOffset = new Vector2(0, -11);

            Rectangle eyesRect = new Rectangle(0, eyesFrame * (eyesTex.Height / totaleyesFrames), (eyesTex.Width), (eyesTex.Height / totaleyesFrames));
            Vector2 eyesVect = new Vector2((float)eyesTex.Width / 2, (float)eyesTex.Height / (2 * totaleyesFrames));

            if (npc.frame.Y == 196 || npc.frame.Y == 196 * 4)
            {
                kunaiOffset.Y -= 8;
                excalipoorOffset.Y -= 8;
                masamuneOffset.Y -= 8;
                gunBladeOffset.Y -= 8;
                busterSwordOffset.Y -= 8;
                eyesOffset.Y -= 8;
            }
            if (npc.frame.Y == 196 * 2 || npc.frame.Y == 196 * 5)
            {
                kunaiOffset.Y -= 2;
                excalipoorOffset.Y -= 2;
                masamuneOffset.Y -= 2;
                gunBladeOffset.Y -= 2;
                busterSwordOffset.Y -= 2;
                eyesOffset.Y -= 2;
            }
            if (npc.frame.Y == 0 || npc.frame.Y == 196 * 3)
            {
                kunaiOffset.Y += 2;
                excalipoorOffset.Y += 2;
                masamuneOffset.Y += 2;
                gunBladeOffset.Y += 2;
                busterSwordOffset.Y += 2;
                eyesOffset.Y += 2;
            }

            if (npc.ai[2] >= 1)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height * 0.5f) / Main.npcFrameCount[npc.type]);
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, 0);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 148), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]));
                    
                    Vector2 kunaiOffset2 = kunaiOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 excalipoorOffset2 = excalipoorOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 masamuneOffset2 = masamuneOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 gunBladeOffset2 = gunBladeOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 busterSwordOffset2 = busterSwordOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);
                    Vector2 eyesOffset2 = eyesOffset.RotatedBy(npc.oldRot[i] * npc.direction, Vector2.Zero);

                    float kunaiRotation2 = kunaiRotation + npc.oldRot[i];
                    float excalipoorRotation2 = excalipoorRotation + npc.oldRot[i];
                    float masamuneRotation2 = masamuneRotation + npc.oldRot[i];
                    float gunBladeRotation2 = gunBladeRotation + npc.oldRot[i];
                    float busterSwordRotation2 = busterSwordRotation + npc.oldRot[i];
                    float eyesRotation2 = eyesRotation + npc.oldRot[i];

                    if (npc.ai[3] < 1.5f)
                    {
                        spriteBatch.Draw(excalipoorTex, drawPos + new Vector2(npc.scale * npc.direction * excalipoorOffset2.X, npc.scale * excalipoorOffset2.Y), new Rectangle?(excalipoorRect), color2, excalipoorRotation2, excalipoorVect, npc.scale, excalipoorEffects, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(busterSwordTex, drawPos + new Vector2(npc.scale * npc.direction * busterSwordOffset2.X, npc.scale * busterSwordOffset2.Y), new Rectangle?(busterSwordRect), color2, busterSwordRotation2, busterSwordVect, npc.scale, busterSwordEffects, 0f);
                        spriteBatch.Draw(masamuneTex, drawPos + new Vector2(npc.scale * npc.direction * masamuneOffset2.X, npc.scale * masamuneOffset2.Y), new Rectangle?(masamuneRect), color2, masamuneRotation2, masamuneVect, npc.scale, masamuneEffects, 0f);
                    }
                    spriteBatch.Draw(gunBladeTex, drawPos + new Vector2(npc.scale * npc.direction * gunBladeOffset2.X, npc.scale * gunBladeOffset2.Y), new Rectangle?(gunBladeRect), color2, gunBladeRotation2, gunBladeVect, npc.scale, gunBladeEffects, 0f);
                    spriteBatch.Draw(kunaiTex, drawPos + new Vector2(npc.scale * npc.direction * kunaiOffset2.X, npc.scale * kunaiOffset2.Y), new Rectangle?(kunaiRect), color2, kunaiRotation2, kunaiVect, npc.scale, kunaiEffects, 0f);
                    spriteBatch.Draw(eyesTex, drawPos + new Vector2(npc.scale * npc.direction * eyesOffset2.X, npc.scale * eyesOffset2.Y), new Rectangle?(eyesRect), Color.White, eyesRotation2, eyesVect, npc.scale, eyesEffects, 0f);
                }
                eyesOffset = eyesOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
                eyesRotation += npc.rotation;
                spriteBatch.Draw(eyesTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * eyesOffset.X, npc.scale * eyesOffset.Y), new Rectangle?(eyesRect), Color.White, eyesRotation, eyesVect, npc.scale, eyesEffects, 0f);
            }

            kunaiOffset = kunaiOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            excalipoorOffset = excalipoorOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            masamuneOffset = masamuneOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            gunBladeOffset = gunBladeOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            busterSwordOffset = busterSwordOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);

            kunaiRotation += npc.rotation;
            excalipoorRotation += npc.rotation;
            masamuneRotation += npc.rotation;
            gunBladeRotation += npc.rotation;
            busterSwordRotation += npc.rotation;

            if (npc.ai[3] < 1.5f)
            {
                spriteBatch.Draw(excalipoorTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * excalipoorOffset.X, npc.scale * excalipoorOffset.Y), new Rectangle?(excalipoorRect), color, excalipoorRotation, excalipoorVect, npc.scale, excalipoorEffects, 0f);
            }
            else
            {
                spriteBatch.Draw(busterSwordTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * busterSwordOffset.X, npc.scale * busterSwordOffset.Y), new Rectangle?(busterSwordRect), color, busterSwordRotation, busterSwordVect, npc.scale, busterSwordEffects, 0f);
                spriteBatch.Draw(masamuneTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * masamuneOffset.X, npc.scale * masamuneOffset.Y), new Rectangle?(masamuneRect), color, masamuneRotation, masamuneVect, npc.scale, masamuneEffects, 0f);
            }
            spriteBatch.Draw(gunBladeTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * gunBladeOffset.X, npc.scale * gunBladeOffset.Y), new Rectangle?(gunBladeRect), color, gunBladeRotation, gunBladeVect, npc.scale, gunBladeEffects, 0f);
            spriteBatch.Draw(kunaiTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * kunaiOffset.X, npc.scale * kunaiOffset.Y), new Rectangle?(kunaiRect), color, kunaiRotation, kunaiVect, npc.scale, kunaiEffects, 0f);
        }
        public override bool CheckDead()
        {
            if (npc.ai[3] != 100)
            {
                Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Gilgamesh"));
                npc.ai[0] = 120;
                npc.frame.Y = 0;
                npc.velocity.X = npc.direction * -32;
                npc.velocity.Y = -8;
                npc.damage = 0;
                npc.ai[3] = 100;
                npc.life = 1;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                npc.NPCLoot();
                return false;
            }
            else
            {
                if (NPC.AnyNPCs(mod.NPCType("Enkidu")))
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
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Smoke, 0, 0, 0, Color.OrangeRed, 2);
                }
                npc.active = false;
            }
            return false;
        }
        public override bool PreAI()
        {
            if (npc.ai[3] == 100)
            {
                npc.dontTakeDamage = true;
                npc.ai[0]--;
                npc.velocity.X = npc.direction * -32;
                npc.velocity.Y = -8;
                npc.rotation = 12 * 0.0174f * npc.ai[0] * npc.direction;
                if (npc.ai[0] < 0)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
                return false;
            }
            return base.PreAI();
        }
    }
}

