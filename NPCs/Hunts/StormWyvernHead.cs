using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class StormWyvernHead : StormWyvern
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Wyvern");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.WyvernHead);
            npc.aiStyle = -1;
            npc.lifeMax = 15000;
            npc.damage = 80;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath8;
            npc.value = 0;
            npc.netAlways = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.worldSurface * 0.35f && Main.raining && !JoostWorld.downedStormWyvern && JoostWorld.activeQuest.Contains(npc.type) && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override void NPCLoot()
        {
            JoostWorld.downedStormWyvern = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("StormWyvern"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
        public override void Init()
        {
            base.Init();
            head = true;
            directional = true;
            wyvernStyle = true;
        }
        public override void CustomSegmentSpawn()
        {
            if (npc.ai[0] == 0)
            {
                npc.ai[3] = (float)npc.whoAmI;
                npc.realLife = npc.whoAmI;
                int lastSeg = npc.whoAmI;
                for (int i = 0; i < maxLength; i++)
                {
                    int segType = ModContent.NPCType<StormWyvernBody>();
                    if (i == 1)
                    {
                        segType = ModContent.NPCType<StormWyvernWings>();
                    }
                    if (i == 8)
                    {
                        segType = ModContent.NPCType<StormWyvernLegs>();
                    }
                    if (i == maxLength - 3)
                    {
                        segType = ModContent.NPCType<StormWyvernBody2>();
                    }
                    if (i == maxLength - 2)
                    {
                        segType = ModContent.NPCType<StormWyvernBody3>();
                    }
                    if (i == maxLength - 1)
                    {
                        segType = ModContent.NPCType<StormWyvernTail>();
                    }
                    int num10 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), segType, npc.whoAmI);
                    Main.npc[num10].ai[3] = (float)npc.whoAmI;
                    Main.npc[num10].realLife = npc.whoAmI;
                    Main.npc[num10].ai[1] = (float)lastSeg;
                    Main.npc[lastSeg].ai[0] = (float)num10;
                    npc.netUpdate = true;
                    NetMessage.SendData(23, -1, -1, null, num10, 0f, 0f, 0f, 0, 0, 0);
                    lastSeg = num10;
                }
            }

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale) + 1;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[2]);
            writer.Write((short)npc.localAI[3]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[2] = reader.ReadInt16();
            npc.localAI[3] = reader.ReadInt16();
        }

        //Only localAI[2] and localAI[3] available for use
        public override bool PreAI()
        {
            Main.raining = true;
            Main.maxRaining = 0.9f;
            if (Main.numClouds < Main.maxClouds)
            {
                Main.numClouds++;
            }
            if (Math.Abs(Main.windSpeed) < Math.Abs(Main.windSpeedSet))
            {
                Main.windSpeed += Math.Sign(Main.windSpeedSet) * 0.015f;
            }
            Main.rainTime = 60;
            Player target = Main.player[npc.target];
            if (Main.expertMode && Math.Abs(Main.windSpeed) > 0.8f)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.position.Y / 16 < Main.worldSurface && !p.behindBackWall)
                    {
                        p.AddBuff(194, 2, false);
                    }
                }
            }
            if (Vector2.Distance(npc.Center, target.Center) > 3500 || (target.position.Y / 16f) > Main.worldSurface || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
                target = Main.player[npc.target];
                if (!target.active || target.dead || Vector2.Distance(npc.Center, target.Center) > 3000 || (target.position.Y / 16f) > Main.worldSurface)
                {
                    npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                    if (npc.localAI[3] > 0)
                    {
                        npc.localAI[3]--;
                    }
                    if (npc.Center.Y < 666 && npc.velocity.Y < speed)
                    {
                        npc.velocity.Y += turnSpeed * 2;
                    }
                    if (npc.Center.Y / 16 >= Main.worldSurface && npc.velocity.Y > speed)
                    {
                        npc.velocity.Y -= turnSpeed * 2;
                    }
                    return true;
                }
            }

            //Lightning
            if (npc.localAI[3] > 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 55);
                if (npc.localAI[3] == 12 || npc.localAI[3] == 24)
                {
                    Main.PlaySound(42, npc.Center, 21);
                }
                if (npc.localAI[3] == 168)
                {
                    Main.PlaySound(42, npc.Center, 224);
                }
                npc.localAI[3]++;
                if (npc.localAI[3] < 240)
                {
                    float s = speed;
                    Vector2 vel = npc.velocity.RotatedBy(MathHelper.ToRadians(3 * npc.spriteDirection));
                    vel *= s / vel.Length();
                    npc.velocity = vel;
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                }
                else if (npc.localAI[3] < 288)
                {
                    float s = 4;
                    npc.velocity = npc.DirectionTo(target.Center) * s;
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                }
                if (npc.localAI[3] == 288)
                {
                    npc.velocity *= -1;
                    Main.PlaySound(42, (int)npc.Center.X, (int)npc.Center.Y, 226, 1, -0.5f);
                }
                if (npc.localAI[3] >= 288 && npc.localAI[3] < 318)
                {
                    float s = 3;
                    npc.velocity = npc.DirectionTo(target.Center) * -s;
                    npc.rotation = (float)Math.Atan2(-npc.velocity.Y, -npc.velocity.X) + 1.57f;
                }
                if (npc.localAI[3] == 318)
                {
                    npc.velocity *= -1;
                    Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 122, 1.5f);
                    
                    Vector2 vel = npc.velocity;
                    vel.Normalize();
                    Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("StormWyvernZap"), 65, 0, 0, npc.whoAmI);
                }
                if (npc.localAI[3] >= 338)
                {
                    npc.localAI[3] = 0;
                }
                return false;
            }
            else
            {
                //Periodic Dash
                npc.localAI[2]++;
                if (npc.localAI[2] % 400 == 300)
                {
                    Main.PlaySound(3, npc.Center, 56);
                }
                if (npc.localAI[2] % 400 > 300 && npc.localAI[2] % 400 < 340 || Vector2.Distance(npc.Center, target.Center) > 1800)
                {
                    Vector2 vel = npc.DirectionTo(target.Center) * speed;
                    if (vel.Length() > speed)
                    {
                        vel *= speed / vel.Length();
                    }
                    float home = 20f;
                    if (vel != Vector2.Zero)
                    {
                        npc.velocity = ((home - 1f) * npc.velocity + vel) / home;
                    }
                }
                if (npc.localAI[2] % 400 == 340)
                {
                    Main.PlaySound(42, npc.Center, 28);
                    float dashSpeed = speed * 2;
                    Vector2 targetPos = target.Center;
                    if (Main.expertMode)
                    {
                        targetPos += target.velocity * (Vector2.Distance(npc.Center, target.Center) / dashSpeed);
                    }
                    npc.velocity = npc.DirectionTo(targetPos) * dashSpeed;
                }
                if (npc.localAI[2] % 400 > 340)
                {
                    Vector2 targetPos = target.Center;
                    if (Main.expertMode)
                    {
                        targetPos += target.velocity * (Vector2.Distance(npc.Center, target.Center) / npc.velocity.Length());
                    }
                    Vector2 direction = new Vector2(targetPos.X > npc.Center.X ? 1 : -1, targetPos.Y > npc.Center.Y ? 1 : -1);
                    if (Math.Abs(npc.velocity.X) < speed)
                    {
                        npc.velocity.X += turnSpeed * direction.X;
                    }
                    if (Math.Abs(npc.velocity.Y) < speed)
                    {
                        npc.velocity.Y += turnSpeed * direction.Y;
                    }
                }
                if (npc.localAI[2] > 1500)
                {
                    npc.localAI[2] = 0;
                    npc.localAI[3] = 1;
                }
            }
            return true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = 0;
            if (npc.localAI[3] > 0)
            {
                if (npc.localAI[3] < 8)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.localAI[3] < 16)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else
                {
                    npc.frame.Y = frameHeight * 3;
                }
            }
            else if (npc.localAI[2] % 400 >= 300 && npc.localAI[2] % 400 < 340)
            {
                npc.frame.Y = frameHeight;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            int xFrameCount = 1;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle(npc.frame.X, npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[npc.type]) / 2f));
            spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (npc.width / 2f) - (texture.Width / xFrameCount) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)(npc.height / 2) - (float)(texture.Height / Main.npcFrameCount[npc.type]) / 2 + 4f + vect.Y), new Rectangle?(rect), drawColor, npc.rotation, vect, 1f, effects, 0f);

            if (npc.localAI[3] > 0 && npc.localAI[3] < 318)
            {
                float scale = npc.localAI[3] < 288 ? npc.localAI[3] / 288 : 1f;
                float rot = MathHelper.ToRadians(npc.localAI[3] * 11);
                texture = Main.projectileTexture[mod.ProjectileType("StormWyvernZap")];
                rect = new Rectangle((texture.Width / 3) * ((int)npc.localAI[3] / 2) % 3, 0, (texture.Width / 3), (texture.Height / 3));
                vect = new Vector2(((texture.Width / 3) / 2f), ((texture.Height / 3) / 2f));
                Vector2 offSet = (npc.rotation - 1.57f).ToRotationVector2() * 60;
                spriteBatch.Draw(texture, offSet + new Vector2(npc.position.X - Main.screenPosition.X + (npc.width / 2f) - (texture.Width / 3) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)(npc.height / 2) - (float)(texture.Height / 3) / 2 + 4f + vect.Y), new Rectangle?(rect), Color.White, rot, vect, scale, effects, 0f);

            }
            return false;
        }
    }
}