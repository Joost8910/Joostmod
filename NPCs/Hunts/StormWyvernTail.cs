using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.WyvernBody);
            NPC.aiStyle = -1;
            NPC.lifeMax = 10000;
            NPC.damage = 40;
            NPC.defense = 40;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.value = 0;
            NPC.netAlways = true;
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
            rotation = NPC.rotation;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)NPC.localAI[2]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[2] = reader.ReadInt16();
        }

        public override bool PreAI()
        {
            if (Main.npc[(int)NPC.ai[1]].localAI[3] >= 24)
            {
                NPC.spriteDirection = Main.npc[(int)NPC.ai[1]].spriteDirection;
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 55, 0, 0, 0, default(Color), 0.5f);
                if (NPC.localAI[3] == 12 || NPC.localAI[3] == 24)
                {
                    SoundEngine.PlaySound(SoundID.Trackable, NPC.Center);
                }
                if (NPC.localAI[3] <= 24)
                {
                    NPC.localAI[3]++;
                }
            }
            else
            {
                NPC.localAI[3] = 0;
            }
            Vector2 dir = (NPC.rotation - 1.57f).ToRotationVector2() * -1;
            NPC.TargetClosest(false);
            bool facing = Math.Abs(NPC.DirectionFrom(NPC.targetRect.Center()).ToRotation() - (NPC.rotation - 1.57f)) < 1;
            if (NPC.localAI[2] != 160 || facing)
            {
                NPC.localAI[2]++;
            }
            if (NPC.localAI[2] == 180)
            {
                SoundEngine.PlaySound(SoundID.Item7.WithVolumeScale(2.5f).WithPitchOffset(-0.8f), NPC.Center);
                SoundEngine.PlaySound(SoundID.Trackable.WithPitchOffset(-0.4f), NPC.Center);
                Main.windSpeedSet = (dir.X < 0 ? -1 : 1) * (Main.expertMode ? 2 : 1);
                int num = Main.expertMode ? 7 + Main.rand.Next(6) : 4 + Main.rand.Next(4);
                for (int i = 0; i < num; i++)
                {
                    Vector2 vel = dir * 12.5f;
                    vel = vel.RotatedByRandom(MathHelper.ToRadians(90));
                    float scale = 1f - (Main.rand.NextFloat() * .6f);
                    vel = vel * scale;
                    Projectile.NewProjectile(NPC.Center, vel, Mod.Find<ModProjectile>("Gust").Type, 25, 10);

                    for (int d = 0; d < 5; d++)
                    {
                        Dust.NewDust(NPC.Center - new Vector2(10, 10), 20, 20, 31, vel.X, vel.Y, 0, default(Color), 2f);
                    }
                }
            }
            if (NPC.localAI[2] > 190)
            {
                NPC.localAI[2] = 0;
            }

            return base.PreAI();
        }
        public override void FindFrame(int frameHeight)
        {
            int frameWidth = 42;
            if (NPC.localAI[2] > 160)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 4 && NPC.frame.X < frameWidth * 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.X += frameWidth;
                }

                if (NPC.localAI[2] > 180)
                {
                    NPC.frame.X = frameWidth * 5;
                }
            }
            else
            {
                NPC.frame.X = 0;
            }
            if (NPC.localAI[3] < 6)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.localAI[3] < 12)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.localAI[3] < 18)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else
            {
                NPC.frame.Y = frameHeight * 3;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            int xFrameCount = 6;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = new Rectangle((int)NPC.frame.X, (int)NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[NPC.type]) / 2));
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)(NPC.height / 2) - (float)(texture.Height / Main.npcFrameCount[NPC.type]) / 2 + 4f + vect.Y), new Rectangle?(rect), drawColor, NPC.rotation, vect, 1f, effects, 0f);
            return false;
        }
    }
}