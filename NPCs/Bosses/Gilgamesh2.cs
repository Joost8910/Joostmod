using System;
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
        }
        public override void SetDefaults()
        {
            npc.width = 90;
            npc.height = 166;
            npc.damage = 140;
            npc.defense = 50;
            npc.lifeMax = 400000;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = Item.buyPrice(30, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = 0;
            npc.buffImmune[BuffID.Ichor] = true;
            bossBag = mod.ItemType("GilgBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ClashOnTheBigBridge");
            npc.frameCounter = 0;
            musicPriority = MusicPriority.BossHigh;
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
                    if (npc.frameCounter > 10)
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
            if (npc.ai[2] <= 0)
            {
                npc.ai[0]++;
                npc.localAI[0]++;
                npc.localAI[1]++;
                npc.localAI[2]++;
                npc.localAI[3]++;

                if (npc.Center.Y > P.Center.Y + 100 && npc.velocity.Y == 0)
                {
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                }
                if (npc.Center.Y < P.Center.Y - 150)
                {
                    npc.position.Y++;
                }
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && npc.ai[3] < 1 && moveSpeed > 3)
                {
                    npc.velocity.Y = -10f;
                }
                if (P.Center.X > npc.Center.X + 10)
                {
                    npc.velocity.X = moveSpeed;
                }
                if (P.Center.X < npc.Center.X - 10)
                {
                    npc.velocity.X = -moveSpeed;
                }
                
                if (npc.localAI[0] > 90 && npc.Distance(P.MountedCenter) < 300)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                    float Speed = 18f;
                    Vector2 arm = npc.Center + new Vector2(35 * npc.direction, -28);
                    Vector2 pos = P.MountedCenter;
                    float rotation = (float)Math.Atan2(npc.Center.Y - pos.Y, npc.Center.X - pos.X);
                    int type = mod.ProjectileType("GilgNaginata");
                    int damage = 55;
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
                    if (Main.netMode != 1 && flag)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 7f, Main.myPlayer, npc.whoAmI, 4f);
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
                if (npc.localAI[2] > 40)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (Main.projectile[i].type == mod.ProjectileType("GilgAxe") && projectile.active)
                        {
                            npc.localAI[2] = 40;
                        }
                    }
                }
                if (npc.localAI[2] > 90)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    float Speed = 18f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(39 * npc.direction, -38);
                    Vector2 pos =P.MountedCenter;
                    if (Main.expertMode)
                    {
                        pos = PredictiveAim(Speed, arm);
                    }
                    float rotation = (float)Math.Atan2(npc.Center.Y - pos.Y, npc.Center.X - pos.X);
                    int type = mod.ProjectileType("GilgAxe");
                    int damage = 50;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10f, Main.myPlayer, npc.whoAmI);
                    }
                    npc.localAI[2] = 0;
                }
                if (npc.localAI[3] > 90)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 19);
                    float Speed = 12f + npc.velocity.Length();
                    Vector2 arm = npc.Center + new Vector2(-27 * npc.direction, -37);
                    Vector2 pos = P.MountedCenter;
                    float rotation = (float)Math.Atan2(npc.Center.Y - pos.Y, npc.Center.X - pos.X);
                    int type = mod.ProjectileType("GilgKunai");
                    int damage = 40;
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
                            Projectile.NewProjectile(arm.X, arm.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, 5f, Main.myPlayer);
                        }
                    }
                    npc.localAI[3] = 0;
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
            if (npc.ai[2] > 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int damage = 200;
                    int type = mod.ProjectileType("BitterEnd");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 28);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, type, damage, 15f, Main.myPlayer);
                    }

                    if (npc.Center.X < P.Center.X)
                    {
                        npc.velocity.X = 15f;
                    }
                    else
                    {
                        npc.velocity.X = -15f;
                    }
                    npc.velocity.Y = -7f;
                }
                else
                {
                    npc.velocity.Y = -10f;
                    int damage = 200;
                    int type = mod.ProjectileType("UltimateIllusion");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 66);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 22f, 0f, type, damage, 15f, Main.myPlayer);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -22f, 0f, type, damage, 15f, Main.myPlayer);
                    }
                }
                npc.ai[2] = 0;
                npc.netUpdate = true;
            }

        }
        private Vector2 PredictiveAim(float speed, Vector2 origin)
        {
            Player P = Main.player[npc.target];
            Vector2 vel = (Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200 ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
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

            ribbonFrame = (int)((npc.ai[0] % 60) / 10);

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

            Player player = Main.player[npc.target];
            Vector2 arm = npc.Center + new Vector2(axeOffset.X * npc.direction, axeOffset.Y);
            Vector2 predictedPos = PredictiveAim(24f, arm);
            float rotation = (float)Math.Atan2(npc.Center.Y - predictedPos.Y, npc.Center.X - predictedPos.X);
            if (npc.direction == 1)
            {
                rotation += 3.14f;
            }

            if (npc.localAI[2] > 45 && npc.localAI[2] <= 90)
            {
                axeRotation = ((npc.localAI[2] - 45f) * 3 * 0.0174f * -npc.direction) + rotation;
                if (npc.localAI[2] > 80)
                {
                    axeFrame = 1;
                    axeRotation = ((105 * 0.0174f * -npc.direction) + rotation) - ((npc.localAI[2] - 80f) * 10.5f * 0.0174f * -npc.direction);
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

            spriteBatch.Draw(ribbonTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * ribbonOffset.X, npc.scale * ribbonOffset.Y), new Rectangle?(ribbonRect), color, ribbonRotation, ribbonVect, npc.scale, ribbonEffects, 0f);
            spriteBatch.Draw(flailTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * flailOffset.X, npc.scale * flailOffset.Y), new Rectangle?(flailRect), color, flailRotation, flailVect, npc.scale, effects, 0f);
            spriteBatch.Draw(naginataTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * naginataOffset.X, npc.scale * naginataOffset.Y), new Rectangle?(naginataRect), color, naginataRotation, naginataVect, npc.scale, effects, 0f);
            spriteBatch.Draw(axeTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * axeOffset.X, npc.scale * axeOffset.Y), new Rectangle?(axeRect), color, axeRotation, axeVect, npc.scale, effects, 0f);
            spriteBatch.Draw(shieldTex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * shieldOffset.X, npc.scale * shieldOffset.Y), new Rectangle?(shieldRect), color, shieldRotation, shieldVect, npc.scale, effects, 0f);

            return base.PreDraw(spriteBatch, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects kunaiEffects = SpriteEffects.None;
            int dir = npc.direction;
            if (npc.localAI[3] > 45 || npc.localAI[3] < 15)
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
            float rotation = (float)Math.Atan2(npc.Center.Y - pos.Y, npc.Center.X - pos.X);
            if (npc.direction == 1)
            {
                rotation += 3.14f;
            }

            if (npc.localAI[3] > 45 && npc.localAI[3] <= 90)
            {
                kunaiRotation = ((npc.localAI[3] - 45f) * 3 * 0.0174f * -npc.direction) + rotation;
                if (npc.localAI[3] > 80)
                {
                    kunaiFrame = 1;
                    kunaiRotation = ((105 * 0.0174f * -npc.direction) + rotation) - ((npc.localAI[3] - 80f) * 10.5f * 0.0174f * -npc.direction);
                }
            }
            if (npc.localAI[3] < 15)
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

            Rectangle excalipoorRect = new Rectangle(0, excalipoorFrame * (excalipoorTex.Height / totalExcalipoorFrames), (excalipoorTex.Width), (excalipoorTex.Height / totalExcalipoorFrames));
            Vector2 excalipoorVect = new Vector2((float)excalipoorTex.Width / 2, (float)excalipoorTex.Height / (2 * totalExcalipoorFrames));


            SpriteEffects busterSwordEffects = SpriteEffects.None;
            dir = npc.direction;
            if (dir == 1)
            {
                busterSwordEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                busterSwordEffects = SpriteEffects.None;
            }
            Texture2D busterSwordTex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_BusterSwordArm");
            int totalBusterSwordFrames = 4;
            int busterSwordFrame = 0;
            float busterSwordRotation = 0;
            Vector2 busterSwordOffset = new Vector2(-28, -47);

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
            int totalMasamuneFrames = 3;
            int masamuneFrame = 0;
            float masamuneRotation = 0;
            Vector2 masamuneOffset = new Vector2(-24, -43);

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

            Rectangle gunBladeRect = new Rectangle(0, gunBladeFrame * (gunBladeTex.Height / totalGunBladeFrames), (gunBladeTex.Width), (gunBladeTex.Height / totalGunBladeFrames));
            Vector2 gunBladeVect = new Vector2((float)gunBladeTex.Width / 2, (float)gunBladeTex.Height / (2 * totalGunBladeFrames));

            if (npc.frame.Y == 196 || npc.frame.Y == 196 * 4)
            {
                kunaiOffset.Y -= 8;
                excalipoorOffset.Y -= 8;
                masamuneOffset.Y -= 8;
                gunBladeOffset.Y -= 8;
            }
            if (npc.frame.Y == 196 * 2 || npc.frame.Y == 196 * 5)
            {
                kunaiOffset.Y -= 2;
                excalipoorOffset.Y -= 2;
                masamuneOffset.Y -= 2;
                gunBladeOffset.Y -= 2;
            }
            if (npc.frame.Y == 0 || npc.frame.Y == 196 * 3)
            {
                kunaiOffset.Y += 2;
                excalipoorOffset.Y += 2;
                masamuneOffset.Y += 2;
                gunBladeOffset.Y += 2;
            }
            if (npc.ai[3] < 4)
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


            base.PostDraw(spriteBatch, drawColor);
        }
        public override bool CheckDead()
        {
            if (NPC.AnyNPCs(mod.NPCType("Enkidu")))
            {
                Main.NewText("<Gilgamesh> Ack! Uh, up to you now Enkidu!", 225, 25, 25);
                Main.NewText("<Enkidu> Now you've gone and made me angry.", 25, 225, 25);
            }
            else
            {
                Main.NewText("<Gilgamesh> Ack! How could we have lost!?", 225, 25, 25);
            }
            for (int i = 0; i < npc.width / 8; i++)
            {
                for (int j = 0; j < npc.height / 8; j++)
                {
                    Dust.NewDust(npc.position + new Vector2(i, j), 8, 8, DustID.Smoke, 0, 0, 0, Color.OrangeRed, 2);
                }
            }
            return true;
        }
    }
}

