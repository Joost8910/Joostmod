using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JoostMod.PlayerLayers
{
    public class OverArmFront : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<JoostPlayer>().DrawOverArmor();
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/StoneFlesh_NewBody");
            Rectangle frame = drawInfo.compFrontArmFrame;
            Rectangle frameS = drawInfo.compFrontShoulderFrame;
            float rot = drawPlayer.bodyRotation + drawInfo.compositeFrontArmRotation;
            Vector2 drawPos = drawPlayer.bodyPosition;
            Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            vector2.Y -= 2f;
            drawPos += vector2 * (float)(-(float)drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
            Vector2 origin = drawInfo.bodyVect;
            //Vector2 compositeOffset_FrontArm = new Vector2((float)(-5 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : -1)), 0f);
            //origin += compositeOffset_FrontArm;
            //drawPos += compositeOffset_FrontArm;
            Vector2 shoulderPos = drawPos + drawInfo.frontShoulderOffset;

            if (drawInfo.compFrontArmFrame.X / drawInfo.compFrontArmFrame.Width >= 7)
            {
                drawPos += new Vector2((float)((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : -1), (float)((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically)) ? 1 : -1));
            }

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

            int num = drawInfo.compShoulderOverFrontArm ? 1 : 0;
            int num2 = (!drawInfo.compShoulderOverFrontArm) ? 1 : 0;

            for (int i = 0; i < 2; i++)
            {
                if (i == num && !drawInfo.hideCompositeShoulders)
                {
                    DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + shoulderPos + origin, new Rectangle?(frameS), color, drawPlayer.bodyRotation, origin, 1f, effects, 0);
                    data.shader = drawInfo.cBody;
                    drawInfo.DrawDataCache.Add(data);
                }
                if (i == num2)
                {
                    DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
                    data.shader = drawInfo.cBody;
                    drawInfo.DrawDataCache.Add(data);
                }
            }

        }
    }
}
