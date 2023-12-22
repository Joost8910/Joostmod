using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent;

namespace JoostMod.PlayerLayers
{
    public class GlowEye : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var jp = drawInfo.drawPlayer.GetModPlayer<JoostPlayer>();
            return jp.glowContacts;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        public override bool IsHeadLayer => true;
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            var joostPlayer = drawPlayer.GetModPlayer<JoostPlayer>();
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Accessories/GlowingContacts_Face");

            int eyeType = joostPlayer.glowEyeType;
            if (eyeType == 2)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Accessories/GlowingContacts_Alt");
            }
            if (eyeType == 3)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Accessories/GlowingContacts_Helm");
            }
            if (eyeType == 4)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Accessories/GlowingContacts_HelmAlt");
            }
            if (eyeType == 5)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Accessories/GlowingContacts_Genji");
            }

            Rectangle frame = drawPlayer.bodyFrame;
            float rot = drawPlayer.headRotation;
            Vector2 drawPos = drawPlayer.headPosition;
            Vector2 origin = drawInfo.headVect;
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            if (joostPlayer.glowEyeNoGlow)
            {
                color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
            }

            SpriteEffects effects = SpriteEffects.None;
            if (drawPlayer.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (drawPlayer.gravDir == -1f)
            {
                effects |= SpriteEffects.FlipVertically;
            }
            DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
            data.shader = joostPlayer.glowEyeDye;
            if (eyeType > 0)
            {
                drawInfo.DrawDataCache.Add(data);
            }
            else 
            {
                for (int i = 0; i < drawInfo.DrawDataCache.Count; i++)
                {
                    DrawData drawData = drawInfo.DrawDataCache[i];
                    if (drawData.texture == TextureAssets.Players[drawInfo.skinVar, 2].Value) //Find the player's eye(pupil) texture
                    {
                        drawInfo.DrawDataCache.Insert(i + 1, data); //WOOHOO!! It works!
                        break;
                        /*
                        Main.NewText(i);
                        drawData.texture = tex;
                        if (!joostPlayer.glowEyeNoGlow)
                        {
                            drawData.color = drawInfo.drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
                        }
                        drawData.shader = joostPlayer.glowEyeDye;
                        */
                    }
                } 
            }
        }
    }
}
