using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JoostMod.PlayerLayers
{
    public class OverArmBack : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<JoostPlayer>().DrawOverArmor();
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Skin);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/StoneFlesh_NewBody");
            Rectangle frame = drawInfo.compBackArmFrame;
            Rectangle frameS = drawInfo.compBackShoulderFrame;
            float rot = drawPlayer.bodyRotation + drawInfo.compositeBackArmRotation;
            Vector2 drawPos = drawPlayer.bodyPosition;
            Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            vector2.Y -= 2f;
            drawPos += vector2 * (float)(-(float)drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
            Vector2 compositeOffset_BackArm = new Vector2((float)(6 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : -1)), (float)(2 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically)) ? 1 : -1)));
            Vector2 origin = drawInfo.bodyVect + compositeOffset_BackArm;
            //drawPos += compositeOffset_BackArm;
            Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);

            if (drawPlayer.GetModPlayer<JoostPlayer>().pinkSlimeActive)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/PinkSlimeActive_NewBody");
                color *= 0.5f;
            }
            if (drawPlayer.GetModPlayer<JoostPlayer>().slimeActive)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/SlimeActive_NewBody");
                color *= 0.5f;
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

            if (!drawInfo.hideCompositeShoulders)
            {
                DrawData dataS = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frameS), color, drawPlayer.bodyRotation, origin, 1f, effects, 0);
                dataS.shader = drawInfo.cBody;
                drawInfo.DrawDataCache.Add(dataS);
            }
            DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
            data.shader = drawInfo.cBody;
            drawInfo.DrawDataCache.Add(data);
        }
    }
}
