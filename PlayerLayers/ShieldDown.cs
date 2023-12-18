using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JoostMod.PlayerLayers
{
    public class ShieldDown : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.shield == (sbyte)EquipLoader.GetEquipSlot(JoostMod.instance, "HavelsGreatshield", EquipType.Shield);
        }
        public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.Backpacks, PlayerDrawLayers.Tails);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/HavelsGreatshield_ShieldDown");
            Rectangle frame = new Rectangle(drawPlayer.bodyFrame.X - 1, drawPlayer.bodyFrame.Y, drawPlayer.bodyFrame.Width + 6, drawPlayer.bodyFrame.Height);
            float rot = drawPlayer.bodyRotation;
            Vector2 drawPos = drawPlayer.bodyPosition;
            Vector2 origin = drawInfo.bodyVect;
            Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
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
            data.shader = drawInfo.cShield;
            drawInfo.DrawDataCache.Add(data);
        }
    }
}
