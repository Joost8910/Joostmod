using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JoostMod.PlayerLayers
{
    public class OverTorso : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<JoostPlayer>().drawOverArmor;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/StoneFlesh_Body");
            Rectangle frame = drawPlayer.bodyFrame;
            float rot = drawPlayer.headRotation;
            Vector2 drawPos = drawPlayer.bodyPosition;
            Vector2 origin = drawInfo.headVect;
            Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);

            if (drawPlayer.GetModPlayer<JoostPlayer>().pinkSlimeActive)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/PinkSlimeActive_Body");
                color *= 0.5f;
            }
            if (drawPlayer.GetModPlayer<JoostPlayer>().slimeActive)
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Buffs/SlimeActive_Body");
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
            DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
            data.shader = drawInfo.cBody;
            drawInfo.DrawDataCache.Add(data);
        }
    }
}
