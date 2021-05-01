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
    public class StormWyvernTail : StormWyvern
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Wyvern");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.WyvernBody);
            npc.aiStyle = -1;
            npc.lifeMax = 10000;
            npc.damage = 40;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath8;
            npc.value = 0;
            npc.netAlways = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[2]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[2] = reader.ReadInt16();
        }

        public override bool PreAI()
        {
            if (Main.npc[(int)npc.ai[1]].localAI[3] >= 24)
            {
                npc.spriteDirection = Main.npc[(int)npc.ai[1]].spriteDirection;
                Dust.NewDust(npc.position, npc.width, npc.height, 55, 0, 0, 0, default(Color), 0.5f);
                if (npc.localAI[3] == 12 || npc.localAI[3] == 24)
                {
                    Main.PlaySound(42, npc.Center, 21);
                }
                if (npc.localAI[3] <= 24)
                {
                    npc.localAI[3]++;
                }
            }
            else
            {
                npc.localAI[3] = 0;
            }
            Vector2 dir = (npc.rotation - 1.57f).ToRotationVector2() * -1;
            npc.TargetClosest(false);
            bool facing = Math.Abs(npc.DirectionFrom(npc.targetRect.Center()).ToRotation() - (npc.rotation - 1.57f)) < 1;
            if (npc.localAI[2] != 160 || facing)
            {
                npc.localAI[2]++;
            }
            if (npc.localAI[2] == 180)
            {
                Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 7, 2.5f, -0.8f);
                Main.PlaySound(42, (int)npc.Center.X, (int)npc.Center.Y, 206, 1f, -0.4f);
                Main.windSpeedSet = (dir.X < 0 ? -1 : 1) * (Main.expertMode ? 2 : 1);
                int num = Main.expertMode ? 7 + Main.rand.Next(6) : 4 + Main.rand.Next(4);
                for (int i = 0; i < num; i++)
                {
                    Vector2 vel = dir * 12.5f;
                    vel = vel.RotatedByRandom(MathHelper.ToRadians(90));
                    float scale = 1f - (Main.rand.NextFloat() * .6f);
                    vel = vel * scale;
                    Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("Gust"), 25, 10);

                    for (int d = 0; d < 5; d++)
                    {
                        Dust.NewDust(npc.Center - new Vector2(10, 10), 20, 20, 31, vel.X, vel.Y, 0, default(Color), 2f);
                    }
                }
            }
            if (npc.localAI[2] > 190)
            {
                npc.localAI[2] = 0;
            }

            return base.PreAI();
        }
        public override void FindFrame(int frameHeight)
        {
            int frameWidth = 42;
            if (npc.localAI[2] > 160)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 4 && npc.frame.X < frameWidth * 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.X += frameWidth;
                }

                if (npc.localAI[2] > 180)
                {
                    npc.frame.X = frameWidth * 5;
                }
            }
            else
            {
                npc.frame.X = 0;
            }
            if (npc.localAI[3] < 6)
            {
                npc.frame.Y = 0;
            }
            else if (npc.localAI[3] < 12)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.localAI[3] < 18)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else
            {
                npc.frame.Y = frameHeight * 3;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            int xFrameCount = 6;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle((int)npc.frame.X, (int)npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[npc.type]) / 2));
            spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)(npc.height / 2) - (float)(texture.Height / Main.npcFrameCount[npc.type]) / 2 + 4f + vect.Y), new Rectangle?(rect), drawColor, npc.rotation, vect, 1f, effects, 0f);
            return false;
        }
    }
}