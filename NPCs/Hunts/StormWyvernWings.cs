using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class StormWyvernWings : StormWyvern
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
            NPC.damage = 50;
            NPC.defense = 34;
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
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
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
            return base.PreAI();
        }
        public override void FindFrame(int frameHeight)
        {
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
            NPC head = Main.npc[(int)NPC.ai[3]];
            if (head.velocity.Y < 0 || NPC.frame.X > 0)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.X += 156;
                }
                if (NPC.frame.X >= 780)
                {
                    NPC.frame.X = 0;
                }
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
            int xFrameCount = 5;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = new Rectangle((int)NPC.frame.X, (int)NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[NPC.type]) / 2));
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)(NPC.height / 2) - (float)(texture.Height / Main.npcFrameCount[NPC.type]) / 2 + 4f + vect.Y), new Rectangle?(rect), drawColor, NPC.rotation, vect, 1f, effects, 0f);
            return false;
        }
    }
}