using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class StormWyvernBody2 : StormWyvern
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
            npc.defense = 30;
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
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
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
            return base.PreAI();
        }
        public override void FindFrame(int frameHeight)
        {
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
            int xFrameCount = 1;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle((int)npc.frame.X, (int)npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[npc.type]) / 2));
            spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)(npc.height / 2) - (float)(texture.Height / Main.npcFrameCount[npc.type]) / 2 + 4f + vect.Y), new Rectangle?(rect), drawColor, npc.rotation, vect, 1f, effects, 0f);
            return false;
        }
    }
}