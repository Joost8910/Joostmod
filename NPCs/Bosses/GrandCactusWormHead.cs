using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class GrandCactusWormHead : ModNPC
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Grand Cactus Worm");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 15000;        
            npc.damage = 50;    
            npc.defense = 8;         
            npc.knockBackResist = 0f;
            npc.width = 86;
            npc.height = 106;       
            npc.noGravity = true;           
            npc.noTileCollide = true;     
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.behindTiles = true;          
            npc.value = Item.buyPrice(0, 6, 0, 0);
            npc.netAlways = true;
            npc.boss = true;
            npc.lavaImmune = true;
            bossBag = mod.ItemType("GrandCactusWormBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DeoremMua");
            musicPriority = MusicPriority.BossHigh;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[1] % 25 <= 12)
            {
                npc.frame.Y = 0;
            }
            else
            {
                npc.frame.Y = 108;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(target.Center, npc.Center) > 50)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale) + 1;
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)	
		{
			rotation = npc.rotation;
		}
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.RestorationPotion;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0 && JoostWorld.downedCactusWorm)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GrandCactusWormHead"), npc.scale);
            }
        }
        public override void NPCLoot()
        {
            if (!JoostWorld.downedCactusWorm)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("GrandCactusWorm"), 1, false);
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LusciousCactus"), 8+Main.rand.Next(8));
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DeoremMuaMusicBox"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GrandCactusWormMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GrandCactusWormTrophy"));
                }
            }
            JoostWorld.downedCactusWorm = true;
        }
        Vector2 targetPos = Vector2.Zero;
        double speed = 10f;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.WriteVector2(targetPos);
            writer.Write(speed);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            targetPos = reader.ReadVector2();
            speed = reader.ReadDouble();
        }
        public override bool PreAI()
        {
            Player P = Main.player[npc.target];
            if (Vector2.Distance(npc.Center, P.MountedCenter) > 4000 || npc.target < 0 || npc.target == 255 || P.dead || !P.active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead || Vector2.Distance(npc.Center, P.MountedCenter) > 4000)
                {
                    npc.ai[3] = 0;
                }
            }
            if (Main.netMode != 1)
            {
                if (npc.ai[0] == 0)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;
                    int cactusWormLength = 18;
                    for (int i = 0; i < cactusWormLength; ++i)
                    {
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GrandCactusWormBody"), npc.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    }
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GrandCactusWormTail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }
            npc.ai[2] = 0;
            if (npc.life < npc.lifeMax/7 && npc.ai[0] != 0 && Main.expertMode)
            {
                npc.ai[0] = 2;
            }
            else
            {
                npc.ai[0] = 1;
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.ai[3] == 0)
            {
                music = MusicID.Desert;
                npc.ai[1] = 0;
                speed = 5;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                targetPos = npc.Center + new Vector2((float)Math.Cos(npc.rotation - 1.585f) * 160, (float)Math.Sin(npc.rotation - 1.585f) * 160);
                npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                npc.ai[2] = 0;
                if (Vector2.Distance(npc.Center, P.MountedCenter) < 700 && P.active && !P.dead)
                {
                    targetPos = new Vector2(P.MountedCenter.X - 300, P.MountedCenter.Y - 300);
                    speed = 20;
                    if (Vector2.Distance(targetPos, npc.Center) < 40)
                    {
                        npc.ai[3] = 1;
                    }
                }
            }
            else
            {
                npc.direction = npc.velocity.X < 0 ? -1 : 1;
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DeoremMua");
                if (npc.ai[1] == 0)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y + 500);
                    speed = 20;
                }
                if (npc.ai[1] == 24)
                {
                    targetPos = new Vector2(npc.Center.X + 850, npc.Center.Y);
                    speed = 20;
                }
                if (npc.ai[1] == 66)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y - 850);
                    speed = 20;
                }
                if (npc.ai[1] == 110)
                {
                    targetPos = new Vector2(npc.Center.X - 350, npc.Center.Y);
                    speed = 16;
                }
                if (npc.ai[1] == 130)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y + 400);
                    speed = 4;
                }
                if (npc.ai[1] >= 225 && npc.ai[1] < 425)
                {
                    targetPos = new Vector2(P.MountedCenter.X + (npc.ai[1] % 100 < 50 ? 150 : -150), P.MountedCenter.Y - 200 + (npc.ai[1] % 60 < 30 ? 16 : -16));
                    speed = 4;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                }
                if (npc.ai[1] >= 425 && npc.ai[1] < 550)
                {
                    targetPos = new Vector2(P.MountedCenter.X, P.MountedCenter.Y - 300 + (npc.ai[1] % 20 < 10 ? 16 : -16));
                    speed = Math.Abs(npc.Center.X - targetPos.X) < 16 ? 1 : 6;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    if (Main.expertMode && npc.ai[1] > 500)
                    {
                        float sped = 15f;
                        Vector2 vel = new Vector2(P.velocity.X, 0);
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, npc.Center) / sped));
                        dir = npc.DirectionTo(predictedPos);
                    }
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                    if (npc.ai[1] % 25 == 0 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center + dir * 40, dir * 15, mod.ProjectileType("CactusWormBall"), 15, 5, Main.myPlayer);
                    }
                }
                if (npc.ai[1] >= 550 && npc.ai[1] < 575)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y + 400);
                    speed = -2;
                    npc.rotation = (float)Math.PI;
                }
                if (npc.ai[1] == 575)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y + 400);
                    npc.rotation = (float)Math.PI;
                    speed = 16;
                    npc.ai[2] = -1;
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] >= 600 && npc.ai[1] < 624)
                {
                    targetPos = new Vector2(P.MountedCenter.X, P.MountedCenter.Y + 300);
                    speed = 11;
                }
                if (npc.ai[1] >= 624 && npc.ai[1] < 820)
                {
                    targetPos = new Vector2(P.MountedCenter.X + (npc.ai[1] % 100 < 50 ? 150 : -150), P.MountedCenter.Y + 250 + (npc.ai[1] % 60 < 30 ? 16 : -16));
                    speed = 4;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                }
                if (npc.ai[1] >= 820 && npc.ai[1] < 945)
                {
                    targetPos = new Vector2(P.MountedCenter.X, P.MountedCenter.Y + 250 + (npc.ai[1] % 20 < 10 ? 16 : -16));
                    speed = Math.Abs(npc.Center.X - targetPos.X) < 16 ? 1 : 6;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    if (Main.expertMode && npc.ai[1] > 900)
                    {
                        float sped = 15f;
                        Vector2 vel = new Vector2(P.velocity.X, 0);
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, npc.Center) / sped));
                        dir = npc.DirectionTo(predictedPos);
                    }
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                    if (npc.ai[1] % 25 == 0 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center + dir * 40, dir * 15, mod.ProjectileType("CactusWormBall"), 15, 5, Main.myPlayer);
                    }
                }
                if (npc.ai[1] >= 945 && npc.ai[1] < 970)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y - 400);
                    speed = -2;
                    npc.rotation = 0;
                }
                if (npc.ai[1] == 970)
                {
                    targetPos = new Vector2(npc.Center.X, npc.Center.Y - 400);
                    speed = 16;
                    npc.rotation = 0;
                    npc.ai[2] = -1;
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] >= 995 && npc.ai[1] < 1020)
                {
                    targetPos = new Vector2(P.MountedCenter.X - 400, P.MountedCenter.Y - 300);
                    npc.ai[2] = 1;
                    speed = 10;
                }
                if (npc.ai[1] == 1020)
                {
                    targetPos = new Vector2(P.MountedCenter.X + 200 * npc.direction, P.MountedCenter.Y + 300);
                    speed = 14;
                    npc.ai[2] = -1;
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] >= 1050 && npc.ai[1] < 1080)
                {
                    targetPos = new Vector2(P.MountedCenter.X + 200 * npc.direction, P.MountedCenter.Y - 300);
                    npc.ai[2] = 1;
                    speed = 2;
                }
                if (npc.ai[1] == 1080)
                {
                    targetPos = new Vector2(P.MountedCenter.X + 200 * npc.direction, P.MountedCenter.Y - 300);
                    speed = 14;
                    npc.ai[2] = -1;
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] >= 1111 && npc.ai[1] < 1140)
                {
                    targetPos = new Vector2(P.MountedCenter.X + 100 * npc.direction, P.MountedCenter.Y);
                    npc.ai[2] = 1;
                    speed = 5;
                }
                if (npc.ai[1] == 1140)
                {
                    targetPos = new Vector2(P.MountedCenter.X + 200, P.MountedCenter.Y - 100);
                    npc.ai[2] = 1;
                    speed = 14;
                }
                if (npc.ai[1] == 1160)
                {
                    targetPos = new Vector2(P.MountedCenter.X - 200, P.MountedCenter.Y - 100);
                    npc.ai[2] = 1;
                    speed = 14;
                }
                if (npc.ai[1] == 1180)
                {
                    targetPos = P.MountedCenter;
                    speed = 12;
                }
                /*if (npc.ai[1] == 1215 || npc.ai[1] == 1265 || npc.ai[1] == 1315 || npc.ai[1] == 1365)
                {
                    targetPos = P.MountedCenter;
                    speed = -2;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                }*/
                if ((npc.ai[1] >= 1215 && npc.ai[1] < 1240) || (npc.ai[1] >= 1265 && npc.ai[1] < 1290) || (npc.ai[1] >= 1315 && npc.ai[1] < 1340) || (npc.ai[1] >= 1365 && npc.ai[1] < 1390) || (npc.ai[1] >= 1410 && npc.ai[1] < 1440))
                {
                    targetPos = P.MountedCenter;
                    speed = -2;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                    npc.ai[2] = -1;
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] == 1240 || npc.ai[1] == 1290 || npc.ai[1] == 1340 || npc.ai[1] == 1390)
                {
                    targetPos = P.MountedCenter;
                    speed = 12;
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] >= 1240 && npc.ai[1] <= 1390)
                {
                    npc.ai[2] = -1;
                }
                if (npc.ai[1] == 1440)
                {
                    targetPos = P.MountedCenter;
                    speed = 14; 
                    npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
                }
                if (npc.ai[1] >= 1440 && npc.ai[1] < 1460)
                {
                    npc.ai[2] = -1;
                }
                if (npc.ai[1] >= 1460 && npc.ai[1] <= 1620)
                {
                    targetPos = P.MountedCenter;
                    speed = 5;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    if (Main.expertMode)
                    {
                        float sped = 15f;
                        Vector2 vel = new Vector2(P.velocity.X, 0);
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, npc.Center) / sped));
                        dir = npc.DirectionTo(predictedPos);
                        targetPos = predictedPos;
                    }
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                    if (Main.netMode != 1 && (npc.ai[1] == 1494 || npc.ai[1] == 1524 || npc.ai[1] == 1620))
                    {
                        Projectile.NewProjectile(npc.Center+ npc.DirectionTo(targetPos)*40, dir*15, mod.ProjectileType("CactusWormBall"), 15, 5, Main.myPlayer);
                    }
                }

                if (npc.ai[1] > 1620 && npc.ai[1] < 1820)
                {
                    //targetPos = new Vector2(npc.Center.X + 120 * npc.direction, npc.Center.Y + (npc.ai[1] % 60 < 5 ? 90 : npc.ai[1] % 60 < 10 ? 60 : npc.ai[1] % 60 < 15 ? 30 : npc.ai[1] % 60 < 20 ? 0 : npc.ai[1] % 60 < 25 ? -30 : npc.ai[1] % 60 < 30 ? -60 : npc.ai[1] % 60 < 35 ? -90 : npc.ai[1] % 60 < 40 ? -60 : npc.ai[1] % 60 < 45 ? -30 : npc.ai[1] % 60 < 50 ? 0 : npc.ai[1] % 60 < 55 ? 30 : 60));
                    float rotation = npc.ai[1] * 0.0174f * 9;
                    targetPos = P.MountedCenter + (new Vector2((float)(Math.Cos(rotation) * 100), (float)(Math.Sin(rotation) * 100)));
                    speed = 4;
                    npc.ai[2] = 1;
                }
                if (npc.ai[1] >= 1820 && npc.ai[1] < 1945)
                {
                    targetPos = P.MountedCenter;
                    speed = 6;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    if (Main.expertMode && npc.ai[1] % 50 > 20)
                    {
                        float sped = 15f;
                        Vector2 vel = new Vector2(P.velocity.X, 0);
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, npc.Center) / sped));
                        dir = npc.DirectionTo(predictedPos);
                    }
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                    if (npc.ai[1] % 25 == 20 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center + npc.DirectionTo(targetPos) * 40, dir * 15, mod.ProjectileType("CactusWormBall"), 15, 5, Main.myPlayer);
                    }
                }
                if (npc.ai[1] >= 1945 && npc.ai[1] < 1970)
                {
                    targetPos = P.MountedCenter;
                    speed = Vector2.Distance(targetPos, npc.Center) < 16 ? 0 : 1;
                }
                if (npc.ai[1] == 1970)
                {
                    targetPos = P.MountedCenter;
                    speed = 12;
                }
                if (npc.ai[1] >= 2019 && npc.ai[1] < 2215)
                {
                    //targetPos = new Vector2(npc.Center.X + 120 * npc.direction, npc.Center.Y + (npc.ai[1] % 60 < 5 ? 90 : npc.ai[1] % 60 < 10 ? 60 : npc.ai[1] % 60 < 15 ? 30 : npc.ai[1] % 60 < 20 ? 0 : npc.ai[1] % 60 < 25 ? -30 : npc.ai[1] % 60 < 30 ? -60 : npc.ai[1] % 60 < 35 ? -90 : npc.ai[1] % 60 < 40 ? -60 : npc.ai[1] % 60 < 45 ? -30 : npc.ai[1] % 60 < 50 ? 0 : npc.ai[1] % 60 < 55 ? 30 : 60));
                    float rotation = npc.ai[1] * 0.0174f * 9;
                    targetPos = P.MountedCenter + (new Vector2((float)(Math.Cos(rotation) * 100), (float)(Math.Sin(rotation) * 100)));
                    speed = 4;
                    npc.ai[2] = 1;
                }
                if (npc.ai[1] >= 2215 && npc.ai[1] < 2340)
                {
                    targetPos = P.MountedCenter;
                    speed = 6;
                    Vector2 dir = npc.DirectionTo(P.MountedCenter);
                    if (Main.expertMode && npc.ai[1] % 50 > 15)
                    {
                        float sped = 15f;
                        Vector2 vel = new Vector2(P.velocity.X, 0);
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, npc.Center) / sped));
                        dir = npc.DirectionTo(predictedPos);
                    }
                    npc.rotation = (float)Math.Atan2(dir.Y, dir.X) + 1.57f;
                    if (npc.ai[1] % 25 == 15 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center + npc.DirectionTo(targetPos) * 40, dir * 15, mod.ProjectileType("CactusWormBall"), 15, 5, Main.myPlayer);
                    }
                }
                if (npc.ai[1] >= 2340 && npc.ai[1] < 2365)
                {
                    targetPos = new Vector2(P.MountedCenter.X - 300, P.MountedCenter.Y - 300);
                    speed = 10;
                }
                if (npc.ai[1] >= 2365)
                {
                    targetPos = new Vector2(P.MountedCenter.X - 300, P.MountedCenter.Y - 300);
                    speed = 25;
                }
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DeoremMua");
                npc.ai[1]++;
                if (npc.ai[1] > 2410)
                {
                    npc.ai[1] = 0;
                }
            }
            if (npc.ai[2] == 0 && (targetPos == P.MountedCenter || npc.Distance(targetPos) < 100))
            {
                npc.ai[2] = 1;
            }
            if (npc.ai[2] > 0)
            {
                float home = 20f;
                Vector2 move = targetPos - npc.Center;
                if (move.Length() > speed && speed > 0)
                {
                    move *= (float)(speed / move.Length());
                }
                npc.velocity = ((home - 1f) * npc.velocity + move) / home;
                if (npc.velocity.Length() < speed && speed > 0)
                {
                    npc.velocity *= (float)(speed / npc.velocity.Length());
                }
            }
            else if (npc.ai[2] == 0)
            {
                npc.velocity = npc.DirectionTo(targetPos) * (float)speed;
            }
            npc.netUpdate = true;
            return false;
        }
 
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (texture.Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 108), texture.Width, texture.Height / Main.npcFrameCount[npc.type]));
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, rect, drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/GrandCactusWormEyes");
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            int eyeFrame = 0;
            Color color = Color.YellowGreen;
            if ((npc.ai[1] >= 550 && npc.ai[1] <= 575) || (npc.ai[1] >= 945 && npc.ai[1] < 970) || (npc.ai[1] >= 995 && npc.ai[1] < 1020) || (npc.ai[1] >= 1110 && npc.ai[1] < 1115) || (npc.ai[1] >= 1120 && npc.ai[1] < 1125) || (npc.ai[1] >= 1130 && npc.ai[1] < 1135) || (npc.ai[1] >= 1215 && npc.ai[1] < 1240) || (npc.ai[1] >= 1265 && npc.ai[1] < 1290) || (npc.ai[1] >= 1315 && npc.ai[1] < 1340) || (npc.ai[1] >= 1365 && npc.ai[1] < 1390) || (npc.ai[1] >= 1410 && npc.ai[1] < 1440) || (npc.ai[1] >= 1945 && npc.ai[1] < 1970) || (npc.ai[1] >= 2340 && npc.ai[1] < 2365))
            {
                eyeFrame = 1;
                color = Color.Red;
            }
            Rectangle? rect = new Rectangle?(new Rectangle(0, (texture.Height / 2) * eyeFrame, texture.Width, texture.Height / 2));
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, rect, color, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1f;
            return null;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}